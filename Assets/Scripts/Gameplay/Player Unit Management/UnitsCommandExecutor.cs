using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DangoMimikyu.EventManagement;

// macros
using cmdCommand = CommandAtrributes.Commands;
using cmdInput = CommandAtrributes.Inputs;
using cmdPotency = CommandAtrributes.Potency;

public class UnitsCommandExecutor : MonoBehaviour
{
	[SerializeField]
	private List<UnitBehaviour> m_playerUnits;
	private UnitDataManager m_unitDataManager;

	~UnitsCommandExecutor()
	{
		EventManager.instance.StopListening(GameEvents.Input_CommandSuccess, ExecuteCommand);
		EventManager.instance.StopListening(GameEvents.Unit_Spawn, UpdateUnitsList);
		EventManager.instance.StopListening(GameEvents.Unit_Died, UpdateUnitsList);
	}

	#region Monobehaviour functions
	private void Start()
	{
		m_unitDataManager = GameObject.FindGameObjectWithTag("UnitManager").GetComponent<UnitDataManager>();
		EventManager.instance.StartListening(GameEvents.Input_CommandSuccess, ExecuteCommand);
		EventManager.instance.StartListening(GameEvents.Unit_Spawn, UpdateUnitsList);
		EventManager.instance.StartListening(GameEvents.Unit_Died, UpdateUnitsList);
	}

	private void OnEnable()
	{
		//EventManager.instance.StartListening(GameEvents.Input_CommandSuccess, ExecuteCommand);
		//EventManager.instance.StartListening(GameEvents.Unit_Spawn, UnitSpawned);
	}

	private void OnDisable()
	{
		EventManager.instance.StopListening(GameEvents.Input_CommandSuccess, ExecuteCommand);
		EventManager.instance.StopListening(GameEvents.Unit_Spawn, UpdateUnitsList);
		EventManager.instance.StopListening(GameEvents.Unit_Died, UpdateUnitsList);
	}
	#endregion

	#region Unit handling functions
	private void UpdateUnitsList(EventArgumentData ead)
	{
		m_playerUnits = m_unitDataManager.activeUnits;
	}
	#endregion

	#region Command execution functions
	private void ExecuteCommand(EventArgumentData ead)
	{
		Debug.Log("executing command");
		cmdCommand cmd = (cmdCommand)ead.eventParams[0];
		cmdPotency potency = (cmdPotency)ead.eventParams[1];
		foreach (UnitBehaviour u in m_playerUnits)
		{
			switch (cmd)
			{
				case cmdCommand.Forward:
					u.MoveForward(potency);
					break;
				case cmdCommand.Retreat:
					u.Retreat(potency);
					break;
				case cmdCommand.AttackStraight:
					u.AttackStraight(potency);
					break;
				case cmdCommand.AttackUpward:
					u.AttackUpward(potency);
					break;
				case cmdCommand.Defend:
					u.Defend(potency);
					break;
				case cmdCommand.Focus:
					u.Focus(potency);
					break;
				case cmdCommand.Pray:
					u.Pray(potency);
					break;
				default:
					Debug.LogError("Unexpected army command");
					break;
			}
		}
	}
	#endregion
}
