using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DangoMimikyu.EventManagement;

// macros
using cmdCommand = CommandAtrributes.Commands;
using cmdInput = CommandAtrributes.Inputs;
using cmdPotency = CommandAtrributes.Potency;

public class BeatTracker : MonoBehaviour
{
	#region External script references
	[Header("Script references")]
	[SerializeField]
	private BeatUIHandler m_beatUIHandler;
	#endregion

	#region Tempo variables
	[Header("Tempo Settings")]
	[Range(60, 240)]
	[SerializeField]
	private float m_beatsPerMinute = 120.0f;
	[SerializeField]
	private float m_beatDuration; // duration of each beat in seconds
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
	private float m_redZone = 0.2f;
	private float m_redDuration;
	#endregion

	#region Tracking coroutine variables
	private Coroutine c_track = null;
	private bool m_inputThisBeat = false; // true if the player has cast an input during this beat
	private bool m_outlineThisBeat = false;
	[Range(0f, 1f)]
	[SerializeField]
	private float m_timeElapsed = 0.0f;
	private short currentBeat = 0;
	private short totalBeats = 0; // set a fail condition if this goes above 9999
	#endregion

	#region CommandAttributes variables
	private Dictionary<cmdInput[], cmdCommand> m_commandDictionary = new Dictionary<cmdInput[], cmdCommand>(new CommandDictionary());
	private List<cmdCommand> m_cmdBacklog = new List<cmdCommand>();
	private cmdInput[] m_inputs = new cmdInput[4];
	private cmdPotency m_potency;
	#endregion

	#region Monobehaviour functions
	void Start()
    {

    }

    void Update()
    {

    }

	private void OnEnable()
	{

	}
	#endregion

	#region Input functions
	private void InputBeat(cmdInput input = cmdInput.None)
	{
		//currentBeat = (short)((currentBeat + 1) % 4);

		// determine the potency of this action based on the timing; no action needed for green zone or if there was no input
		if (input != cmdInput.None)
		{
			ProcessPotency();
		}

		// process the input or command
		switch (input)
		{
			case cmdInput.None:
				if (!m_inputThisBeat)
				{
					// only reset the command array if the player missed this beat
					ResetInputs();
				}
				else if (currentBeat == 4)
				{
					// send the completed command to be processed
					//ProcessCommand(m_inputs);
					EventManager.instance.DispatchEvent(GameEvents.Input_CommandComplete, m_inputs, m_potency);
				}
				break;
			case cmdInput.Walk:
			case cmdInput.Attack:
			case cmdInput.Defend:
			case cmdInput.Magic:
				if (m_inputThisBeat) // check for double input on this beat
				{
					ResetInputs();
					return;
				}

				for (int i = 0; i < m_inputs.Length; ++i)
				{
					if (m_inputs[i] == cmdInput.None && !m_inputThisBeat)
					{
						m_inputs[i] = input;
						m_inputThisBeat = true;
					}
				}
				++currentBeat;

				string outputstring = "";
				for (int i = 0; i < 4; ++i)
				{
					outputstring += " " + m_inputs[i];
				}
				Debug.Log("input array:" + outputstring + " currentBeat: " + currentBeat);
				break;
			case cmdInput.test:
				Debug.Log("bruhrba");
				break;
			default:
				Debug.LogError("Unexpected player input");
				break;
		}
	}

	private void ResetInputs()
	{
		Debug.Log("resetting inputs");
		for (int i = 0; i < 4; ++i)
		{
			m_inputs.SetValue(cmdInput.None, i);
		}
		currentBeat = 0;
		m_potency = cmdPotency.High;

		// play the wump wump sound
	}
	#endregion

	#region Potency functions
	private void ProcessPotency()
	{
		Debug.Log("potency time elapsed: " + m_timeElapsed);
		if (m_timeElapsed < m_redDuration || m_timeElapsed > (m_beatDuration - m_redDuration)) // red zone
		{
			Debug.Log("low potency");
			m_potency = cmdPotency.Low;
		}
		else if (m_timeElapsed < m_yellowDuration || m_timeElapsed > (m_beatDuration - m_redDuration - m_yellowDuration)) // yellow zone
		{
			Debug.Log("mid potency");
			m_potency = m_potency == cmdPotency.High ? m_potency : cmdPotency.Medium;
		}
		else if (m_timeElapsed <= (m_redDuration + m_yellowDuration + m_greenDuration)) // green zone
		{
			Debug.Log("high potency");
		}
		else
		{
			Debug.Log("unexpected potency timing");
		}

		if (m_timeElapsed > (m_greenDuration + m_yellowDuration)) // red zone
		{
			m_potency = cmdPotency.Low;
		}
		else if (m_timeElapsed > m_greenDuration) // yellow zone
		{
			m_potency = m_potency == cmdPotency.High ? m_potency : cmdPotency.Medium;
		}
	}
	#endregion
}
