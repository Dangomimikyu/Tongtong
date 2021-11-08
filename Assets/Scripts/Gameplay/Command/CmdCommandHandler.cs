using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DangoMimikyu.EventManagement;

// macros
using cmdCommand = CommandAtrributes.Commands;
using cmdInput = CommandAtrributes.Inputs;
using cmdPotency = CommandAtrributes.Potency;

public class CmdCommandHandler : MonoBehaviour
{
	private Dictionary<cmdInput[], cmdCommand> m_commandDictionary = new Dictionary<cmdInput[], cmdCommand>(new CommandDictionary());

	#region Monobehaviour functions
	void Start()
	{
		InitDictionary();
		EventManager.instance.StartListening(GameEvents.Input_CommandComplete, ProcessCommand);
	}

	private void OnDisable()
	{
		EventManager.instance.StopListening(GameEvents.Input_CommandComplete, ProcessCommand);
	}
	#endregion

	#region Init functions
	private void InitDictionary()
	{
		cmdInput walk = cmdInput.Walk; cmdInput atk = cmdInput.Attack;
		cmdInput def = cmdInput.Defend; cmdInput mag = cmdInput.Magic;

		m_commandDictionary.Add(new cmdInput[] { walk, walk, walk, atk }, cmdCommand.Forward);                  // forward
		m_commandDictionary.Add(new cmdInput[] { walk, walk, walk, def }, cmdCommand.Retreat);                  // retreat
		m_commandDictionary.Add(new cmdInput[] { atk, atk, walk, atk }, cmdCommand.AttackStraight);             // attack straight
		m_commandDictionary.Add(new cmdInput[] { atk, walk, atk, atk }, cmdCommand.AttackUpward);				// attack upward
		m_commandDictionary.Add(new cmdInput[] { def, atk, def, atk }, cmdCommand.Defend);						// defend against physical
		m_commandDictionary.Add(new cmdInput[] { def, def, mag, atk }, cmdCommand.Focus);                       // focus to make next attack stronger
		m_commandDictionary.Add(new cmdInput[] { mag, mag, mag, def, mag, def, atk, mag }, cmdCommand.Pray);    // pray for miracle
	}
	#endregion

	#region Command processing functions
	private void ProcessCommand(EventArgumentData ead)
	{
		// param order: cmdCommand command, cmdPotency potency
		cmdInput[] inputs = (cmdInput[])ead.eventParams[0];
		cmdPotency potency = (cmdPotency)ead.eventParams[1];

		if (m_commandDictionary.TryGetValue(inputs, out cmdCommand command))
		{
			Debug.Log("command yes");
			EventManager.instance.DispatchEvent(GameEvents.Input_CommandSuccess, command, potency);
		}
		else
		{
			Debug.Log("command no");
			EventManager.instance.DispatchEvent(GameEvents.Input_CommandFail, command, potency);
		}
	}
	#endregion
}
