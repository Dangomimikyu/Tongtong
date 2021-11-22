using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DangoMimikyu.EventManagement;

public class LevelGoal : MonoBehaviour
{
	private bool m_questEnded = false;
	#region Monobehaviour functions
	//private void OnCollisionEnter2D(Collision2D collision)
	//{
	//	Debug.Log("quest end;");
	//	EventManager.instance.DispatchEvent(GameEvents.Gameplay_QuestEnd);
	//}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (m_questEnded)
			return; // quest only needs to be ended once

		Debug.Log("quest end trigger");
		m_questEnded = true;
		EventManager.instance.DispatchEvent(GameEvents.Gameplay_QuestEnd);
	}
	#endregion
}
