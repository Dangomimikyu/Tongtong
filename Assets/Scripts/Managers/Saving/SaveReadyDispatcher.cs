using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DangoMimikyu.EventManagement;

public class SaveReadyDispatcher : MonoBehaviour
{
	// this class' whole purpose is to tell relevent classes that the save file is ready to be read
	// this prevents FileSaveManager starting later than others and causing error

	private void Start()
	{
		EventManager.instance.DispatchEvent(GameEvents.Misc_SaveReady);

		// change scene after setting save values
		SceneController sc = GameObject.FindGameObjectWithTag("SceneController").GetComponent<SceneController>();
		sc.LoadSceneString("HomeBaseScene");
	}
}
