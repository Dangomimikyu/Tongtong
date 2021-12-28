// overseeing of the all the unit's UI, but individual unit's UI is controlled by it's own UnitBehaviour
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DangoMimikyu.EventManagement;

// macros
using cmdCommand = CommandAtrributes.Commands;
using cmdInput = CommandAtrributes.Inputs;
using cmdPotency = CommandAtrributes.Potency;

public class UnitHeadUIController : MonoBehaviour
{
	[Header("Sprite references")]
	[SerializeField]
	private Sprite m_walkIcon;
	[SerializeField]
	private Sprite m_attackIcon;
	[SerializeField]
	private Sprite m_shieldIcon;
	[SerializeField]
	private Sprite m_focusIcon;
	[SerializeField]
	private Sprite m_prayIcon;

	[Header("Active units")]
	[SerializeField]
	private List<UnitBehaviour> m_playerUnits;
	private UnitDataManager m_unitDataManager;

	~UnitHeadUIController()
	{
		EventManager.instance.StopListening(GameEvents.Input_CommandSuccess, ChangeUnitCommandUI);
		EventManager.instance.StopListening(GameEvents.Unit_Spawn, UnitSpawned);
		EventManager.instance.StopListening(GameEvents.Input_CommandFail, FailedCommandUI);
	}

	#region Monobehaviour functions
	private void Start()
	{
		m_unitDataManager = GameObject.FindGameObjectWithTag("UnitManager").GetComponent<UnitDataManager>();
		EventManager.instance.StartListening(GameEvents.Input_CommandSuccess, ChangeUnitCommandUI);
		EventManager.instance.StartListening(GameEvents.Unit_Spawn, UnitSpawned);
		EventManager.instance.StartListening(GameEvents.Input_CommandFail, FailedCommandUI);
	}

	private void OnEnable()
	{
		//EventManager.instance.StartListening(GameEvents.Input_CommandSuccess, ChangeUnitCommandUI);
		//EventManager.instance.StartListening(GameEvents.Unit_Spawn, UnitSpawned);
		//EventManager.instance.StartListening(GameEvents.Input_CommandFail, FailedCommandUI);
	}

	private void OnDisable()
	{
		EventManager.instance.StopListening(GameEvents.Input_CommandSuccess, ChangeUnitCommandUI);
		EventManager.instance.StopListening(GameEvents.Unit_Spawn, UnitSpawned);
		EventManager.instance.StopListening(GameEvents.Input_CommandFail, FailedCommandUI);
	}

	#endregion

	#region Event response functions
	private void UnitSpawned(EventArgumentData ead)
	{
		m_playerUnits = m_unitDataManager.activeUnits;
	}

	private void ChangeUnitCommandUI(EventArgumentData ead)
	{
		Debug.Log("call here snowman");
		cmdCommand cmd = (cmdCommand)ead.eventParams[0];
		foreach (UnitBehaviour u in m_playerUnits)
		{
			switch (cmd)
			{
				case cmdCommand.Forward:
					u.UpdateHeadUI(m_walkIcon);
					break;
				case cmdCommand.Retreat:
					u.UpdateHeadUI(m_walkIcon);
					break;
				case cmdCommand.AttackStraight:
				case cmdCommand.AttackUpward:
					u.UpdateHeadUI(m_attackIcon);
					break;
				case cmdCommand.Defend:
					u.UpdateHeadUI(m_shieldIcon);
					break;
				case cmdCommand.Focus:
					u.UpdateHeadUI(m_focusIcon);
					break;
				case cmdCommand.Pray:
					u.UpdateHeadUI(m_prayIcon);
					break;
				default:
					Debug.LogError("Unexpected army command");
					break;
			}
		}
	}

	private void FailedCommandUI(EventArgumentData ead)
	{
		Debug.Log("call here snowman");
		foreach (UnitBehaviour u in m_playerUnits)
		{
			u.UpdateHeadUI(null); // set all the unit's head UI to be inactive
		}
	}
	#endregion
}
