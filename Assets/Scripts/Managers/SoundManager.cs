using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DangoMimikyu.EventManagement;

public class SoundManager : MonoBehaviour
{
	#region Monobehaviour functions
	private void Start()
	{
		EventManager.instance.StartListening(GameEvents.Gameplay_ComboFever, ComboSound);
		EventManager.instance.StartListening(GameEvents.Gameplay_BreakCombo, ComboSound);

		EventManager.instance.StartListening(GameEvents.Input_Drum, DrumSound);
	}

	private void OnDisable()
	{
		EventManager.instance.StopListening(GameEvents.Gameplay_ComboFever, ComboSound);
		EventManager.instance.StopListening(GameEvents.Gameplay_BreakCombo, ComboSound);

		EventManager.instance.StopListening(GameEvents.Input_Drum, DrumSound);
	}
	#endregion

	#region Metronome and Drum functions
	private void ComboSound(EventArgumentData ead)
	{
		var fever = ead.eventName == GameEvents.Gameplay_ComboFever ? true : false;
		if (fever)
		{
			Debug.Log("Playing combo fever sound");
		}
		else
		{
			Debug.Log("Playing break combo sound");
		}
	}

	private void DrumSound(EventArgumentData ead)
	{
		CommandAtrributes.Inputs input = (CommandAtrributes.Inputs)ead.eventParams[0];
		switch (input)
		{
			case CommandAtrributes.Inputs.Walk:
				break;
			case CommandAtrributes.Inputs.Attack:
				break;
			case CommandAtrributes.Inputs.Defend:
				break;
			case CommandAtrributes.Inputs.Magic:
				break;
		}
	}
    #endregion
}
