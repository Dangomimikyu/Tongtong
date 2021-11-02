using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DangoMimikyu.EventManagement;

public class SoundManager : MonoBehaviour
{
	#region Monobehaviour functions
	void Start()
    {

    }

	private void OnEnable()
	{
		EventManager.instance.StartListening(GameEvents.Gameplay_BreakCombo, BreakCombo);
	}

	private void OnDisable()
	{
		EventManager.instance.StopListening(GameEvents.Gameplay_BreakCombo, BreakCombo);
	}
	#endregion

	#region Metronome and Drum functions
	private void BreakCombo(EventArgumentData ead)
	{
		Debug.Log("Playing break combo sound");
	}
    #endregion
}
