using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DangoMimikyu.EventManagement;
using TMPro;

// macros
using cmdInput = CommandAtrributes.Inputs;
using cmdPotency = CommandAtrributes.Potency;

public class BeatTracker : MonoBehaviour
{
	#region	Testing variables
	[Header("testing")]
	public TMP_Text bpm;
	public TMP_Text greentext;
	public TMP_Text yellowtext;
	public TMP_Text redtext;
	#endregion

	#region Tempo variables
	[Header("Settings")]
	[Range(60, 240)]
	[SerializeField]
	private float m_beatsPerMinute = 120.0f;
	private float m_beatDuration; // duration of each beat in seconds
	[SerializeField]
	private float m_maxTime = 300.0f;
	#endregion

	#region Rhythm bias variables
	[Header("Rhythm Biases")]
	[Range(0.0f, 1.0f)]
	[Tooltip("percentage of the beat that can be counted as 'perfect'")]
	[SerializeField]
	private float m_greenZone = 0.3f;
	private float m_greenDuration;
	[Range(0.0f, 0.5f)]
	[Tooltip("percentage of the beat that can be counted as 'okay'; with each one being on each side of the green")]
	[SerializeField]
	private float m_yellowZone = 0.25f;
	private float m_yellowDuration;
	[Range(0.0f, 1.0f)]
	[Tooltip("percentage of the beat that can be counted as 'bad' and will break combo")]
	[SerializeField]
	private float m_redZone = 0.1f;
	private float m_redDuration;
	#endregion

	#region Tracking coroutine variables
	private bool m_outlineThisBeat = false;
	private bool m_inputThisBeat = false;
	private bool m_waiting = false;
	private bool m_gamePause = false;
	private bool m_beatDelay;
	private short m_waitCount = 0;
	private IEnumerator c_track;

	[SerializeField]
	private float m_totalTimeElapsed = 0.0f;
	[Range(0f, 1f)]
	[SerializeField]
	private float m_timeElapsed = 0.0f;
	private short m_totalBeats = 0;
	#endregion

	~BeatTracker()
	{
		EventManager.instance.StopListening(GameEvents.Gameplay_QuestEnd, EndTracking);
		EventManager.instance.StopListening(GameEvents.Gameplay_QuestAbandoned, EndTracking);
		EventManager.instance.StopListening(GameEvents.Input_Drum, PlayerInputBeat);
		EventManager.instance.StopListening(GameEvents.Input_CommandSuccess, StartWait);
		EventManager.instance.StopListening(GameEvents.Input_CommandFail, CommandFail);
		EventManager.instance.StopListening(GameEvents.Menu_Pause, GamePause);
	}

	#region Monobehaviour functions
	private void OnValidate()
	{
		m_beatDuration = 60.0f / m_beatsPerMinute;
	}

	private void Start()
	{
		//m_beatDelay = PlayerPrefs.GetInt("BeatDelay") == 1;
		m_beatDelay = false;

		InitTimingZones();

		EventManager.instance.StartListening(GameEvents.Gameplay_QuestEnd, EndTracking);
		EventManager.instance.StartListening(GameEvents.Gameplay_QuestAbandoned, EndTracking);
		EventManager.instance.StartListening(GameEvents.Input_Drum, PlayerInputBeat);
		EventManager.instance.StartListening(GameEvents.Input_CommandSuccess, StartWait);
		EventManager.instance.StartListening(GameEvents.Input_CommandFail, CommandFail);
		EventManager.instance.StartListening(GameEvents.Menu_Pause, GamePause);

		c_track = TrackBeats();
		StartCoroutine(c_track);
	}

	private void OnDisable()
	{
		EventManager.instance.StopListening(GameEvents.Gameplay_QuestEnd, EndTracking);
		EventManager.instance.StopListening(GameEvents.Gameplay_QuestAbandoned, EndTracking);
		EventManager.instance.StopListening(GameEvents.Input_Drum, PlayerInputBeat);
		EventManager.instance.StopListening(GameEvents.Input_CommandSuccess, StartWait);
		EventManager.instance.StopListening(GameEvents.Input_CommandFail, CommandFail);
		EventManager.instance.StopListening(GameEvents.Menu_Pause, GamePause);
	}
	#endregion

	#region Init functions
	private void InitTimingZones()
	{
		// the purpose of this function is just the validate that the timing zones add up to 1 in total
		// if the number is not exactly 1, it will send a console error
		float totalTime = m_greenZone + (m_yellowZone * 2.0f) + (m_redZone * 2.0f);
		m_beatDuration = 60.0f / m_beatsPerMinute;
		if (totalTime == 1.0f)
		{
			m_greenDuration = m_beatDuration * m_greenZone;
			m_yellowDuration = m_beatDuration * m_yellowZone;
			m_redDuration = m_beatDuration * m_redZone;

			bpm.text = m_beatsPerMinute.ToString();
			greentext.text = m_greenDuration.ToString();
			yellowtext.text = m_yellowDuration.ToString();
			redtext.text = m_redDuration.ToString();
			return;
		}
		else
		{
			Debug.LogError("timing zones do not add up to 1. totaltime: " + totalTime);
		}
	}
	#endregion

	#region Coroutines
	private IEnumerator TrackBeats()
	{
		while (Time.timeSinceLevelLoad < m_maxTime)
		{
			while (m_gamePause)
			{
				yield return null;
			}

			m_totalTimeElapsed += Time.deltaTime;
			//m_timeElapsed = Time.timeSinceLevelLoad - (m_totalBeats * m_beatDuration);
			m_timeElapsed = m_totalTimeElapsed - (m_totalBeats * m_beatDuration);
			if (m_timeElapsed >= (m_beatDuration * 0.5f) && !m_outlineThisBeat)
			{
				m_outlineThisBeat = true;
				Debug.Log("metronome beat now");
				EventManager.instance.DispatchEvent(GameEvents.Gameplay_MetronomeBeat, m_beatDuration);
				if (m_waiting)
					EventManager.instance.DispatchEvent(GameEvents.Input_Drum, cmdInput.None);
			}
			else if (m_timeElapsed >= m_beatDuration)
			{
				if (m_inputThisBeat)
				{
					EventManager.instance.DispatchEvent(GameEvents.Input_Drum, cmdInput.BeatEnd);
				}
				else
				{
					if (m_beatDelay)
					{
						if (!m_waiting)
						{
							EventManager.instance.DispatchEvent(GameEvents.Input_Drum, cmdInput.None);
						}
						else
						{
							if (++m_waitCount >= 4)
							{
								m_waiting = false;
								m_waitCount = 0;
							}
						}
					}
				}
				m_outlineThisBeat = false;
				m_inputThisBeat = false;
				++m_totalBeats;
			}

			yield return null; // wait for next frame
		}
		yield break;
	}
	#endregion

	#region Event handling functions
	private void EndTracking(EventArgumentData ead)
	{
		Debug.Log("stopping tracking");
		StopCoroutine(c_track);
	}

	private void PlayerInputBeat(EventArgumentData ead)
	{
		if ((cmdInput)ead.eventParams[0] != cmdInput.None && (cmdInput)ead.eventParams[0] != cmdInput.BeatEnd) // received this event from PlayerInputHandler
		{
			m_inputThisBeat = true;
		}
	}

	private void StartWait(EventArgumentData ead)
	{
		m_waiting = true;
		m_waitCount = 0;
	}

	private void CommandFail(EventArgumentData ead)
	{
		m_waiting = false;
		m_waitCount = 0;
	}

	private void GamePause(EventArgumentData ead)
	{
		Debug.Log("game pause call");
		if ((bool)ead.eventParams[0] == true) // pause the game
		{
			m_gamePause = true;
		}
		else // unpause the game
		{
			m_gamePause = false;
		}
	}
	#endregion

	#region Potency functions
	public void ProcessPotency(ref cmdPotency potency)
	{
		//Debug.Log("potency time elapsed: " + m_timeElapsed);
		if (m_timeElapsed < m_redDuration || m_timeElapsed > (m_beatDuration - m_redDuration)) // red zone
		{
			Debug.Log("potency: low");
			potency = cmdPotency.Low;
		}
		else if (m_timeElapsed < m_yellowDuration || m_timeElapsed > (m_beatDuration - m_redDuration - m_yellowDuration)) // yellow zone
		{
			Debug.Log("potency: mid");
			potency = potency == cmdPotency.High ? potency : cmdPotency.Medium;
		}
		else if (m_timeElapsed <= (m_redDuration + m_yellowDuration + m_greenDuration)) // green zone
		{
			Debug.Log("potency: high");
		}
		else
		{
			Debug.LogWarning("unexpected potency timing");
		}
	}
	#endregion
}
