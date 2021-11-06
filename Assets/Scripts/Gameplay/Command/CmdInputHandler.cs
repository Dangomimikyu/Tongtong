using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DangoMimikyu.EventManagement;

// macros
using cmdCommand = CommandAtrributes.Commands;
using cmdInput = CommandAtrributes.Inputs;
using cmdPotency = CommandAtrributes.Potency;

public class CmdInputHandler : MonoBehaviour
{
	#region External script references
	[Header("Script references")]
	[SerializeField]
	private BeatTracker m_beatTracker;
	#endregion

	#region CommandAttributes variables
	private cmdInput[] m_inputs = new cmdInput[4];
	private cmdPotency m_potency; // keeps track of current potency
	#endregion

	#region Tracking coroutine variables
	private Coroutine c_track = null;
	private bool m_inputThisBeat = false; // true if the player has cast an input during this beat
	private short m_currentBeat = 0;
	#endregion

	#region Monobehaviour functions
	private void Start()
	{
		EventManager.instance.StartListening(GameEvents.Input_Drum, InputBeat);
	}
	private void OnEnable()
	{
		//EventManager.instance.StartListening(GameEvents.Input_Drum, InputBeat);
	}

	private void OnDisable()
	{
		EventManager.instance.StopListening(GameEvents.Input_Drum, InputBeat);
	}
	#endregion

	#region Input functions
	private void InputBeat(EventArgumentData ead)
	{
		cmdInput input = (cmdInput)ead.eventParams[0];

		// determine the potency of this action based on the timing; no action needed for green zone or if there was no input
		if (input != cmdInput.None)
		{
			m_beatTracker.ProcessPotency(ref m_potency);
		}

		// process the input or command
		switch (input)
		{
			case cmdInput.None:
				if (!m_inputThisBeat)
				{
					// only reset the command array if the player missed this beat
					ResetInputs();
					EventManager.instance.DispatchEvent(GameEvents.Input_CommandFail);
				}
				else if (m_currentBeat == 4)
				{
					// send the completed command to be processed
					Debug.Log("command complete");
					EventManager.instance.DispatchEvent(GameEvents.Input_CommandComplete, m_inputs, m_potency);
					ResetInputs();
				}
				m_inputThisBeat = false;
				break;
			case cmdInput.Walk:
			case cmdInput.Attack:
			case cmdInput.Defend:
			case cmdInput.Magic:
				if (m_inputThisBeat) // check for double input on this beat
				{
					Debug.Log("double input");
					ResetInputs();
					EventManager.instance.DispatchEvent(GameEvents.Input_CommandFail);
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
				++m_currentBeat;

				string outputstring = "";
				for (int i = 0; i < 4; ++i)
				{
					outputstring += " " + m_inputs[i];
				}
				Debug.Log("input array:" + outputstring + " currentBeat: " + m_currentBeat);
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
		m_currentBeat = 0;
		m_potency = cmdPotency.High;
	}
	#endregion
}
