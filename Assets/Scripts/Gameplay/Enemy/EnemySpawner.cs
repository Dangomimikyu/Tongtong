using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DangoMimikyu.EventManagement;

public class EnemySpawner : MonoBehaviour
{
	[Header("External script references")]
	[SerializeField]
	private WeaponAttributes m_weaponAttributes;
	[SerializeField]
	private EnemyInformationHandler m_enemyInfoHandler;

	[Header("Spawning variables")]
	[Tooltip("number of waves of enemies")]
	public int enemyWaves;
	[Tooltip("average timing between each enemy wave")]
	[SerializeField]
	private int m_spawnTimingDifference;
	[Tooltip("number of ticks of allowance for timing differences between each wave")]
	[SerializeField]
	private int m_spawnTimingVariance;
	[SerializeField]
	private BoxCollider2D m_spawnRegion;
	[Tooltip("min number of enemies that can be spawned in one wave")]
	[SerializeField]
	private int m_minEnemies = 1;
	[Tooltip("max number of enemies that can be spawned in one wave")]
	[SerializeField]
	private int m_maxEnemiesWave;
	[Tooltip("max number of enemies that can be on screen")]
	[SerializeField]
	private int m_maxEnemiesScreen;

	[Header("Enemy specifications")]
	[SerializeField]
	private GameObject m_enemyPrefab; // budbot
	[SerializeField]
	private Vector3 m_leftWeaponPos;

	private float m_spawnTimeElapsed;       // amount of time elapsed since last spawn [to remove]
	private float m_spawnWaitTime;          // total how long to wait before next spawn [to remove]
	private int m_spawnTickElapsed = 0;
	private int m_spawnTotalWaitTick = 0;
	private float m_currentWaveCount = 0;   // count of what wave the player is on right now

	private bool testspawn = false;

	~EnemySpawner()
	{
		EventManager.instance.StopListening(GameEvents.Gameplay_MetronomeBeat, MetronomeTick);
	}

	#region Monobehaviour functions
	void Start()
	{
		//m_spawnTimeElapsed = -5.0f; // give the player about 5 seconds
		//m_spawnTimeElapsed = 0.0f;
		//m_spawnWaitTime = m_spawnTimingDifference;
		m_spawnTotalWaitTick = m_spawnTimingDifference;
		m_spawnTotalWaitTick += Random.Range(-m_spawnTimingVariance, m_spawnTimingVariance);

		// find weapon attributes
		m_weaponAttributes = GameObject.FindGameObjectWithTag("WeaponAttributes").GetComponent<WeaponAttributes>();
		m_enemyInfoHandler = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyInformationHandler>();

		EventManager.instance.StartListening(GameEvents.Gameplay_MetronomeBeat, MetronomeTick);
	}

	void Update()
	{
  //      m_spawnTimeElapsed += Time.deltaTime;
  //      if (m_spawnTimeElapsed > m_spawnWaitTime && !testspawn)
		//{
  //          testspawn = true;
  //          m_spawnTimeElapsed = 0.0f;
  //          m_currentWaveCount++;
  //          //SpawnEnemies();
  //          TestSpawn();
		//}
	}

	private void OnDisable()
	{
		EventManager.instance.StopListening(GameEvents.Gameplay_MetronomeBeat, MetronomeTick);
	}
	#endregion

	#region Event response functions
	private void MetronomeTick(EventArgumentData ead)
	{
		if (++m_spawnTickElapsed > m_spawnTotalWaitTick)
		{
			m_spawnTickElapsed = 0;
			++m_currentWaveCount;
			m_spawnTotalWaitTick = m_spawnTimingDifference;
			m_spawnTotalWaitTick += Random.Range(-m_spawnTimingVariance, m_spawnTimingVariance);
			TestSpawn();
		}
	}
	#endregion

	#region Spawning functions
	private void SpawnEnemies()
	{
		int numEnemies = Random.Range(m_minEnemies, m_maxEnemiesWave);

		Vector2 spawnPosMin = m_spawnRegion.transform.position - (m_spawnRegion.transform.localScale * 0.5f);
		Vector2 spawnPosMax = m_spawnRegion.transform.position + (m_spawnRegion.transform.localScale * 0.5f);

		for (int i = 0; i < numEnemies; ++i)
		{
			float randomX = Random.Range(spawnPosMin.x, spawnPosMax.x);
			float randomY = Random.Range(spawnPosMin.y, spawnPosMax.y);

			GameObject budbot = Instantiate(m_enemyPrefab);
			budbot.transform.position = new Vector2(randomX, randomY);
			//budbot.GetComponent<EnemyBehaviour>().enemyData

			EventManager.instance.DispatchEvent(GameEvents.Enemy_Spawn, budbot.GetComponent<EnemyBehaviour>());
		}
	}

	private void TestSpawn()
	{
		//Debug.Log("spawning test enemies");
		if (m_enemyInfoHandler.GetNumEnemies() > m_maxEnemiesScreen)
		{
			m_currentWaveCount--; // reduce the wave count since didn't spawn anything
			return; // don't spawn any more if there are too many enemies on screen right now
		}

		int numEnemies = Random.Range(m_minEnemies, m_maxEnemiesWave);
		Vector2 spawnPosMin = m_spawnRegion.transform.position - (m_spawnRegion.transform.localScale * 0.5f);
		Vector2 spawnPosMax = m_spawnRegion.transform.position + (m_spawnRegion.transform.localScale * 0.5f);

		for (int i = 0; i < numEnemies; ++i)
		{

			float randomX = Random.Range(spawnPosMin.x, spawnPosMax.x);
			//float randomY = Random.Range(spawnPosMin.y, spawnPosMax.y);

			GameObject budbot = Instantiate(m_enemyPrefab);
			EnemyBehaviour eb = budbot.GetComponent<EnemyBehaviour>();
			eb.SetMaxHealth(40.0f);
			eb.enemyData.weapon = new Weapon(WeaponAttributes.WeaponType.Pistol, false);
			//eb.enemyData.weapon.weaponType = WeaponAttributes.WeaponType.Pistol;
			SpawnWeapon(budbot, ref eb.enemyData.weapon);
			budbot.transform.position = new Vector2(randomX, m_spawnRegion.transform.position.y);
		}

		testspawn = false;
	}

	private void SpawnWeapon(GameObject parent, ref Weapon weapon)
	{
		if (weapon != null)
		{
			GameObject weaponPrefab = m_weaponAttributes.GetWeaponPrefab(weapon);
			GameObject wpn = Instantiate(weaponPrefab, parent.transform.position, Quaternion.identity, parent.transform);
			weapon.firingPoint = weaponPrefab.GetComponent<WeaponInformation>().weaponFiringPoint;
			wpn.transform.localScale = Vector3.one;
			//wpn.transform.localRotation.Set(0.0f, 180.0f, 0.0f, 1.0f);
			wpn.transform.localRotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
			wpn.transform.localPosition = m_leftWeaponPos; // enemies will only have one weapon
		}
		else
		{
			Debug.Log("enemy weapon was null");
		}
	}
	#endregion
}
