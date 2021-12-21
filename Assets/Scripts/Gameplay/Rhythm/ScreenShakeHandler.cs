using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DangoMimikyu.EventManagement;

public class ScreenShakeHandler : MonoBehaviour
{
	private CinemachineImpulseSource m_impulseSource;

	~ScreenShakeHandler()
	{
		EventManager.instance.StopListening(GameEvents.Input_Drum, BeatInputShake);
		EventManager.instance.StopListening(GameEvents.Input_CommandFail, CommandFailShake);
	}

	#region Monobehaviour functions
	private void Start()
	{
		m_impulseSource = GetComponent<CinemachineImpulseSource>();

		EventManager.instance.StartListening(GameEvents.Input_Drum, BeatInputShake);
		EventManager.instance.StartListening(GameEvents.Input_CommandFail, CommandFailShake);
	}

	private void OnDisable()
	{
		EventManager.instance.StopListening(GameEvents.Input_Drum, BeatInputShake);
		EventManager.instance.StopListening(GameEvents.Input_CommandFail, CommandFailShake);
	}
	#endregion

	#region player input shaking
	private void BeatInputShake(EventArgumentData ead)
	{
		if ((CommandAtrributes.Inputs)ead.eventParams[0] == CommandAtrributes.Inputs.None)
		{
			return; // don't bother continuing if there was no input
		}

		Debug.Log("input shake");
		m_impulseSource.m_ImpulseDefinition.m_AmplitudeGain = 10.4f;
		m_impulseSource.m_ImpulseDefinition.m_FrequencyGain = 10.4f;
		m_impulseSource.GenerateImpulse();
	}

	private void CommandFailShake(EventArgumentData ead)
	{
		Debug.Log("command fail shake");
		m_impulseSource.m_ImpulseDefinition.m_AmplitudeGain = 11.0f;
		m_impulseSource.m_ImpulseDefinition.m_FrequencyGain = 11.0f;
		m_impulseSource.GenerateImpulse();
	}
	#endregion
}
