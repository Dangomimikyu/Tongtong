using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DangoMimikyu.EventManagement;

public class EnemyInformationHandler : MonoBehaviour
{
	[Header("Enemy information lists")]
	[SerializeField]
	private List<EnemyBehaviour> m_totalEnemyList;
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
	}

	#region Monobehaviour functions
	private void Start()
	{
		EventManager.instance.StartListening(GameEvents.Gameplay_MetronomeBeat, MakeDecision);
		EventManager.instance.StartListening(GameEvents.Enemy_Spawn, EnemySpawned);
		EventManager.instance.StartListening(GameEvents.Enemy_Active, EnemyActive);
	}

	private void OnDisable()
	{
		EventManager.instance.StopListening(GameEvents.Gameplay_MetronomeBeat, MakeDecision);
	}
	#endregion

	#region Event handling functions
	private void EnemySpawned(EventArgumentData ead)
	{
		Debug.Log("enemy spawned");
		EnemyBehaviour eb = (EnemyBehaviour)ead.eventParams[0];
		m_totalEnemyList.Add(eb);
	}

	private void EnemyActive(EventArgumentData ead)
	{
		Debug.Log("enemy is active");
		EnemyBehaviour eb = (EnemyBehaviour)ead.eventParams[0];
		m_activeEnemyList.Add(eb);
	}

	private void MakeDecision(EventArgumentData ead)
	{
		// get each enemy to make their own decision on what to do
		foreach (EnemyBehaviour eb in m_activeEnemyList)
		{
			Debug.Log("making decision");
			eb.MakeDecision();
		}
	}
	#endregion
}
