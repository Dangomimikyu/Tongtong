using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DangoMimikyu.EventManagement;

// macros
using cmdCommand = CommandAtrributes.Commands;
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
	private bool m_feverStatus = false;

	~ComboTracker()
	{
		EventManager.instance.StopListening(GameEvents.Input_CommandSuccess, ModifyCombo);
		EventManager.instance.StopListening(GameEvents.Input_CommandFail, ModifyCombo);
	}

	#region Monobehaviour functions
	private void Start()
	{
		EventManager.instance.StartListening(GameEvents.Input_CommandSuccess, ModifyCombo);
		EventManager.instance.StartListening(GameEvents.Input_CommandFail, ModifyCombo);
	}

	private void OnDisable()
	{
		EventManager.instance.StopListening(GameEvents.Input_CommandSuccess, ModifyCombo);
		EventManager.instance.StopListening(GameEvents.Input_CommandFail, ModifyCombo);
	}
	#endregion

	#region Combo handling functions
	private void ModifyCombo(EventArgumentData ead)
	{
		// param order: cmdCommand command, cmdPotency potency
		bool success = ead.eventName == GameEvents.Input_CommandSuccess ? true : false;
		if (success)
		{
			CommandData newInput = new CommandData();
			newInput.command = (cmdCommand)ead.eventParams[0];
			newInput.potency = (cmdPotency)ead.eventParams[1];
			m_cmdHistory.Add(newInput);
			m_comboCount++;
			Debug.Log("combo count: " + m_comboCount);
			if (m_comboCount >= minFeverCombo && m_feverStatus == false)
			{
				m_feverStatus = true;
				EventManager.instance.DispatchEvent(GameEvents.Gameplay_ComboFever);
			}
		}
		else
		{
			m_highestCombo = m_comboCount > m_highestCombo ? m_comboCount : m_highestCombo;
			bool brokeFever = m_feverStatus;
			m_comboCount = 0;
			if (brokeFever)
			{
				EventManager.instance.DispatchEvent(GameEvents.Gameplay_BreakCombo);
				m_feverStatus = false;
			}
		}
	}
	#endregion
}
