using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DangoMimikyu.EventManagement;

public class EnemyInformationHandler : MonoBehaviour
{
	[Header("Enemy information lists")]
	[SerializeField]
	private List<EnemyBehaviour> m_activeEnemyList;

	[Header("Enemy behaviour settings")]
	[Tooltip("min number of beats to wait after an action")]
	public int minWaitBeats;
	[Tooltip("max number of beats to wait after an action")]
	public int maxWaitBeats;

	~EnemyInformationHandler()
	{
		EventManager.instance.StopListening(GameEvents.Gameplay_MetronomeBeat, MakeDecision);
		EventManager.instance.StopListening(GameEvents.Enemy_Spawn, EnemySpawned);
		EventManager.instance.StopListening(GameEvents.Enemy_Active, EnemyActive);
		EventManager.instance.StopListening(GameEvents.Enemy_Damaged, EnemyDamaged);
		EventManager.instance.StopListening(GameEvents.Enemy_Died, EnemyDied);
		EventManager.instance.StopListening(GameEvents.Gameplay_QuestEnd, RemoveAll);
		EventManager.instance.StopListening(GameEvents.Gameplay_QuestAbandoned, RemoveAll);
	}

	#region Monobehaviour functions
	private void Start()
	{
		EventManager.instance.StartListening(GameEvents.Gameplay_MetronomeBeat, MakeDecision);
		EventManager.instance.StartListening(GameEvents.Enemy_Spawn, EnemySpawned);
		EventManager.instance.StartListening(GameEvents.Enemy_Active, EnemyActive);
		EventManager.instance.StartListening(GameEvents.Enemy_Damaged, EnemyDamaged);
		EventManager.instance.StartListening(GameEvents.Enemy_Died, EnemyDied);
		EventManager.instance.StartListening(GameEvents.Gameplay_QuestEnd, RemoveAll);
		EventManager.instance.StartListening(GameEvents.Gameplay_QuestAbandoned, RemoveAll);
	}

	private void OnDisable()
	{
		EventManager.instance.StopListening(GameEvents.Gameplay_MetronomeBeat, MakeDecision);
		EventManager.instance.StopListening(GameEvents.Enemy_Spawn, EnemySpawned);
		EventManager.instance.StopListening(GameEvents.Enemy_Active, EnemyActive);
		EventManager.instance.StopListening(GameEvents.Enemy_Damaged, EnemyDamaged);
		EventManager.instance.StopListening(GameEvents.Enemy_Died, EnemyDied);
		EventManager.instance.StopListening(GameEvents.Gameplay_QuestEnd, RemoveAll);
		EventManager.instance.StopListening(GameEvents.Gameplay_QuestAbandoned, RemoveAll);
	}
	#endregion

	#region Event handling functions
	private void EnemySpawned(EventArgumentData ead)
	{
		//Debug.Log("enemy spawned");
		//EnemyBehaviour eb = (EnemyBehaviour)ead.eventParams[0];
		//m_totalEnemyList.Add(eb);
	}

	private void EnemyActive(EventArgumentData ead)
	{
		Debug.Log("enemy is active");
		EnemyBehaviour eb = (EnemyBehaviour)ead.eventParams[0];
		//m_activeEnemyList.Add(eb);

		if (!m_activeEnemyList.Contains(eb))
		{
			m_activeEnemyList.Add(eb);
			UpdateList();
		}
	}

	private void EnemyDamaged(EventArgumentData ead)
	{
		Debug.Log("enemy took damage");
	}

	private void EnemyDied(EventArgumentData ead)
	{
		Debug.Log("enemy died");

		// remove from the list so it won't be iterated on anymore
		m_activeEnemyList.Remove((EnemyBehaviour)ead.eventParams[0]);
	}

	private void MakeDecision(EventArgumentData ead)
	{
		// get each enemy to make their own decision on what to do
		foreach (EnemyBehaviour eb in m_activeEnemyList)
		{
			Debug.Log("making decision");
			if (eb != null)
			{
				eb.MakeDecision();
			}
		}
	}

	private void RemoveAll(EventArgumentData ead)
	{
		// used when scene change
		m_activeEnemyList.Clear();
	}
	#endregion

	#region Enemy removal
	public void UpdateList()
	{
		int totalCount = m_activeEnemyList.Count;
		for (int i = 0; i < totalCount; ++i)
		{
			if (m_activeEnemyList[i] == null)
			{
				m_activeEnemyList.Remove(m_activeEnemyList[i]);
				totalCount--;
				i--;
			}
		}
	}
	#endregion

	#region Information getters
	public int GetNumEnemies()
	{
		return m_activeEnemyList.Count; // return the number of enemies on screen right now
	}
	#endregion
}
