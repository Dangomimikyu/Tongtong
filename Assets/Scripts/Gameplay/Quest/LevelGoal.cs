using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DangoMimikyu.EventManagement;

public class LevelGoal : MonoBehaviour
{
	#region Monobehaviour functions
	void Start()
    {
    }

    void Update()
    {

    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// end the current quest by dispatching event
		EventManager.instance.DispatchEvent(GameEvents.Gameplay_QuestEnd);
	}
	#endregion
}
