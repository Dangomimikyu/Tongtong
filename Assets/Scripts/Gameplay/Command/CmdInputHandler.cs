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
	[SerializeField]
	private BeatUIHandler m_beatUIHandler;
	#endregion

	#region CommandAttributes variables
	private cmdInput[] m_inputs = new cmdInput[4];
	private cmdPotency m_potency; // keeps track of current potency
	#endregion

	#region input verification variables
	private bool m_waiting = false;
	private bool m_justEndWaiting = false; // needed to tell the checker that it just finished waiting and not to preemptively fail the player
	[SerializeField]
	private bool m_inputThisBeat = false; // true if the player has cast an input during this beat
	private short m_currentBeat = 0;
	private bool m_beatDelay;
	#endregion

	~CmdInputHandler()
	{
		EventManager.instance.StopListening(GameEvents.Input_Drum, InputBeat);
		EventManager.instance.StopListening(GameEvents.Input_CommandSuccess, WaitPostCommand);
		EventManager.instance.StopListening(GameEvents.Input_CommandFail, ResetWait);
	}

	#region Monobehaviour functions
	private void Start()
	{
		//m_beatDelay = PlayerPrefs.GetInt("BeatDelay") == 1;
		m_beatDelay = false;

		EventManager.instance.StartListening(GameEvents.Input_Drum, InputBeat);
		EventManager.instance.StartListening(GameEvents.Input_CommandSuccess, WaitPostCommand);
		EventManager.instance.StartListening(GameEvents.Input_CommandFail, ResetWait);
	}

	private void OnDisable()
	{
		EventManager.instance.StopListening(GameEvents.Input_Drum, InputBeat);
		EventManager.instance.StopListening(GameEvents.Input_CommandSuccess, WaitPostCommand);
		EventManager.instance.StopListening(GameEvents.Input_CommandFail, ResetWait);
	}
	#endregion

	#region Input functions
	private void InputBeat(EventArgumentData ead)
	{
		cmdInput input = (cmdInput)ead.eventParams[0];

		if (m_waiting)
		{
			if (input != cmdInput.None)
			{
				EventManager.instance.DispatchEvent(GameEvents.Input_CommandFail);
				m_waiting = false;
				return;
			}

			if (++m_currentBeat >= 4) // finished waiting
			{
				m_currentBeat = 0;
				m_waiting = false;
				m_inputThisBeat = false;
				//m_justEndWaiting = true;

				// call this function again after resetting
				EventArgumentData temp = ead;
				temp.eventParams[0] = cmdInput.BeatEnd;
				//InputBeat(temp);

				m_beatUIHandler.CompleteWait();
				EventManager.instance.DispatchEvent(GameEvents.Unit_FinishWaiting);
			}
		}
		else
		{
			// determine the potency of this action based on the timing; no action needed for green zone or if there was no input
			if (input != cmdInput.None && input != cmdInput.BeatEnd)
			{
				m_beatTracker.ProcessPotency(ref m_potency);
			}

			// process the input or command
			switch (input)
			{
				case cmdInput.None:
					//if (!m_inputThisBeat) // [to remove]
					//{
					// only reset the command array if the player missed this beat
					ResetInputs();
					EventManager.instance.DispatchEvent(GameEvents.Input_CommandFail);
					//}
					m_inputThisBeat = false;
					break;
				case cmdInput.BeatEnd:
					if (m_currentBeat == 4)
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
					//if (m_inputThisBeat) // check for double input on this beat
					//{
					//	Debug.Log("double input");
					//	ResetInputs();
					//	EventManager.instance.DispatchEvent(GameEvents.Input_CommandFail);
					//	return;
					//}

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
	}

	private void ResetInputs()
	{
		//Debug.Log("resetting inputs");
		for (int i = 0; i < 4; ++i)
		{
			m_inputs.SetValue(cmdInput.None, i);
		}
		m_currentBeat = 0;
		m_potency = cmdPotency.High;
	}
	#endregion

	#region Wait timer event handlers
	private void WaitPostCommand(EventArgumentData ead)
	{
		m_waiting = m_beatDelay;
        m_currentBeat = 0;

        m_beatUIHandler.WaitPostCommand();
	}

	private void ResetWait(EventArgumentData ead)
	{
		m_waiting = false;
		m_currentBeat = 0;

		m_beatUIHandler.FailedCommand();
	}
	#endregion
}
