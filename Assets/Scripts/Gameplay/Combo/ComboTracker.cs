using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DangoMimikyu.EventManagement;

// macros
using cmdCommand = CommandAtrributes.Commands;
using cmdInput = CommandAtrributes.Inputs;
using cmdPotency = CommandAtrributes.Potency;

public class ComboTracker : MonoBehaviour
{
	protected class CommandData
	{
		public cmdCommand command;
		public cmdPotency potency;
	}

	public short minFeverCombo = 3;
	private List<CommandData> m_cmdHistory = new List<CommandData>();
	private short m_comboCount = 0;
	private short m_highestCombo = 0;

	#region Monobehaviour functions
	private void OnEnable()
	{
		EventManager.instance.StartListening(GameEvents.Input_CommandComplete, ModifyCombo);
	}

	private void OnDisable()
	{
		EventManager.instance.StopListening(GameEvents.Input_CommandComplete, ModifyCombo);
	}
	#endregion

	#region Combo handling functions
	private void ModifyCombo(EventArgumentData ead)
	{
		// param order: bool success, cmdCommand command, cmdPotency potency
		bool success = (bool)ead.eventParams[0];
		if (success)
		{
			CommandData newInput = new CommandData();
			newInput.command = (cmdCommand)ead.eventParams[1];
			newInput.potency = (cmdPotency)ead.eventParams[2];
			m_cmdHistory.Add(newInput);
			EventManager.instance.DispatchEvent(GameEvents.Input_CommandSuccess, ead.eventParams[1], ead.eventParams[2]);
		}
		else
		{
			m_highestCombo = m_comboCount > m_highestCombo ? m_comboCount : m_highestCombo;
			bool playsound = m_comboCount > minFeverCombo ? true : false;
			m_comboCount = 0;
			EventManager.instance.DispatchEvent(GameEvents.Input_CommandFail, ead.eventParams[1], ead.eventParams[2]);
			if (playsound)
				EventManager.instance.DispatchEvent(GameEvents.Gameplay_BreakCombo);
		}
	}
	#endregion
}
