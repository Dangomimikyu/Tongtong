using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DangoMimikyu.EventManagement;

public class LevelGoal : MonoBehaviour
{
	private bool m_questEnded = false;

	#region Monobehaviour functions
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (m_questEnded || collision.gameObject.tag != "PlayerUnit")
			return; // quest only needs to be ended once

		Debug.Log("quest end trigger");
		m_questEnded = true;
		EventManager.instance.DispatchEvent(GameEvents.Gameplay_QuestEnd, true);
	}
	#endregion
}
