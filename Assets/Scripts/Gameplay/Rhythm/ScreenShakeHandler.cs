using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DangoMimikyu.EventManagement;

// macros
using CmdInput = CommandAtrributes.Inputs;

public class ScreenShakeHandler : MonoBehaviour
{
	public CinemachineImpulseSource m_impulseSource;

	~ScreenShakeHandler()
	{
		EventManager.instance.StopListening(GameEvents.Unit_Died, UnitDiedShake);
	}

	#region Monobehaviour functions
	private void Start()
	{
		//m_impulseSource = GetComponent<CinemachineImpulseSource>();

		EventManager.instance.StartListening(GameEvents.Unit_Died, UnitDiedShake);
	}

	private void OnDisable()
	{
		EventManager.instance.StopListening(GameEvents.Unit_Died, UnitDiedShake);
	}
	#endregion

	#region player input shaking
	private void UnitDiedShake(EventArgumentData ead)
	{
		m_impulseSource.GenerateImpulse();
	}
	#endregion
}
