using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DangoMimikyu.EventManagement;

public class EnemySpawner : MonoBehaviour
{
    [Header("External script references")]
    [SerializeField]
    private WeaponAttributes m_weaponAttributes;

    [Header("Spawning variables")]
    public int enemyWaves;
    [Tooltip("average timing between each enemy wave")]
    [SerializeField]
    private float m_spawnTimingDifference;
    [Tooltip("allowance for timing differences between each wave")]
    [SerializeField]
    private float m_spawnTimingVariance;
    [SerializeField]
    private BoxCollider2D m_spawnRegion;
    [Tooltip("min number of enemies that can be spawned in one wave")]
    [SerializeField]
    private int m_minEnemies = 1;
    [Tooltip("max number of enemies that can be spawned in one wave")]
    [SerializeField]
    private int m_maxEnemies;
    private float m_spawnTimeElapsed;   // amount of time elapsed since last spawn
    private float m_spawnWaitTime;   // how long to wait before next spawn

    [Header("Enemy specifications")]
    [SerializeField]
    private GameObject m_enemyPrefab; // budbot
    [SerializeField]
    private Vector3 m_leftWeaponPos;

    private bool testspawn = false;

	#region Monobehaviour functions
	void Start()
    {
        m_spawnTimeElapsed = -5.0f; // give the player about 5 seconds
        m_spawnWaitTime = m_spawnTimingDifference;

        // find weapon attributes
        m_weaponAttributes = GameObject.FindGameObjectWithTag("WeaponAttributes").GetComponent<WeaponAttributes>();
    }

    void Update()
    {
        m_spawnTimeElapsed += Time.deltaTime;
        if (m_spawnTimeElapsed > m_spawnWaitTime && !testspawn)
		{
            testspawn = true;
            //m_spawnTimeElapsed = 0.0f;
            //SpawnEnemies();
            TestSpawn();
		}
    }
	#endregion

	#region Spawning functions
    private void SpawnEnemies()
	{
        int numEnemies = Random.Range(m_minEnemies, m_maxEnemies);

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
        Debug.Log("testing enemy spawn");
        int numEnemies = Random.Range(m_minEnemies, m_maxEnemies);

        Vector2 spawnPosMin = m_spawnRegion.transform.position - (m_spawnRegion.transform.localScale * 0.5f);
        Vector2 spawnPosMax = m_spawnRegion.transform.position + (m_spawnRegion.transform.localScale * 0.5f);

        for (int i = 0; i < numEnemies; ++i)
        {
            float randomX = Random.Range(spawnPosMin.x, spawnPosMax.x);
            float randomY = Random.Range(spawnPosMin.y, spawnPosMax.y);

            GameObject budbot = Instantiate(m_enemyPrefab);
            EnemyBehaviour eb = budbot.GetComponent<EnemyBehaviour>();
            eb.enemyData.health = 20;
            eb.enemyData.weapon = new Weapon(WeaponAttributes.WeaponType.Pistol, false);
            //eb.enemyData.weapon.weaponType = WeaponAttributes.WeaponType.Pistol;
            SpawnWeapon(budbot, ref eb.enemyData.weapon);
            budbot.transform.position = new Vector2(randomX, randomY);
        }
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
