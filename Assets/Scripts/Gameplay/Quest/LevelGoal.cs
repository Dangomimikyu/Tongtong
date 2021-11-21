using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DangoMimikyu.EventManagement;

public class LevelGoal : MonoBehaviour
{
	#region Monobehaviour functions
	//private void OnCollisionEnter2D(Collision2D collision)
	//{
	//	Debug.Log("quest end;");
	//	EventManager.instance.DispatchEvent(GameEvents.Gameplay_QuestEnd);
	//}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log("quest end trigger");
		EventManager.instance.DispatchEvent(GameEvents.Gameplay_QuestEnd);
	}
	#endregion
}
