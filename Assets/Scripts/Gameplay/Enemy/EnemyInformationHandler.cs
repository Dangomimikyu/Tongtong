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
	}

	#region Monobehaviour functions
	private void Start()
	{
		EventManager.instance.StartListening(GameEvents.Gameplay_MetronomeBeat, MakeDecision);
		EventManager.instance.StartListening(GameEvents.Enemy_Spawn, EnemySpawned);
		EventManager.instance.StartListening(GameEvents.Enemy_Active, EnemyActive);
		EventManager.instance.StartListening(GameEvents.Enemy_Damaged, EnemyDamaged);
		EventManager.instance.StartListening(GameEvents.Enemy_Died, EnemyDied);
	}

	private void OnDisable()
	{
		EventManager.instance.StopListening(GameEvents.Gameplay_MetronomeBeat, MakeDecision);
		EventManager.instance.StopListening(GameEvents.Enemy_Spawn, EnemySpawned);
		EventManager.instance.StopListening(GameEvents.Enemy_Active, EnemyActive);
		EventManager.instance.StopListening(GameEvents.Enemy_Damaged, EnemyDamaged);
		EventManager.instance.StopListening(GameEvents.Enemy_Died, EnemyDied);
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
		m_activeEnemyList.Add(eb);
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
			eb.MakeDecision();
		}
	}
	#endregion
}
