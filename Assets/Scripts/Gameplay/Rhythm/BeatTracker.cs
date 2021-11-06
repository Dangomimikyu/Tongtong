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
	[Range(0f, 1f)]
	[SerializeField]
	private float m_timeElapsed = 0.0f;
	private short m_totalBeats = 0;
	#endregion

	#region Monobehaviour functions
	private void OnValidate()
	{
		m_beatDuration = 60.0f / m_beatsPerMinute;
	}

	private void Start()
	{
		InitTimingZones();

		StartCoroutine(TrackBeats());
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
		while (Time.time < m_maxTime)
		{
			m_timeElapsed = Time.time - (m_totalBeats * m_beatDuration);

			if (m_timeElapsed >= (m_beatDuration * 0.5f) && !m_outlineThisBeat)
			{
				m_outlineThisBeat = true;
				EventManager.instance.DispatchEvent(GameEvents.Gameplay_MetronomeBeat);
			}
			else if (m_timeElapsed >= m_beatDuration)
			{
				m_outlineThisBeat = false;
				EventManager.instance.DispatchEvent(GameEvents.Input_Drum, cmdInput.None);
				++m_totalBeats;
			}

			yield return null; // wait for next frame
		}
		EventManager.instance.DispatchEvent(GameEvents.Gameplay_QuestEnd, false);
		yield break;
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
