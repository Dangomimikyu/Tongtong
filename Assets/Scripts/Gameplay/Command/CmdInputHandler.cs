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
	private Dictionary<cmdInput[], cmdCommand> m_commandDictionary = new Dictionary<cmdInput[], cmdCommand>(new CommandDictionary());
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
		InitDictionary();
	}
	private void OnEnable()
	{
		EventManager.instance.StartListening(GameEvents.Input_Drum, InputBeat);
	}

	private void OnDisable()
	{
		EventManager.instance.StopListening(GameEvents.Input_Drum, InputBeat);
	}
	#endregion

	#region Init functions
	private void InitDictionary()
	{
		cmdInput walk = cmdInput.Walk; cmdInput atk = cmdInput.Attack;
		cmdInput def = cmdInput.Defend; cmdInput mag = cmdInput.Magic;

		m_commandDictionary.Add(new cmdInput[] { walk, walk, walk, atk }, cmdCommand.Forward);                  // forward
		m_commandDictionary.Add(new cmdInput[] { walk, walk, walk, def }, cmdCommand.Retreat);                  // retreat
		m_commandDictionary.Add(new cmdInput[] { atk, atk, walk, atk }, cmdCommand.Attack);                     // attack
		m_commandDictionary.Add(new cmdInput[] { def, atk, def, atk }, cmdCommand.DefendPhysical);              // defend against physical
		m_commandDictionary.Add(new cmdInput[] { def, mag, mag, def }, cmdCommand.DefendMagical);               // defend against magic
		m_commandDictionary.Add(new cmdInput[] { def, def, mag, atk }, cmdCommand.Focus);                       // focus to make next attack stronger
		m_commandDictionary.Add(new cmdInput[] { mag, mag, mag, def, mag, def, atk, mag }, cmdCommand.Pray);    // pray for miracle
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
				}
				else if (m_currentBeat == 4)
				{
					// send the completed command to be processed
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

		// play the wump wump sound only if there is a combo
	}
	#endregion
}
