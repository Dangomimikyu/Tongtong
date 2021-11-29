using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
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
    [SerializeField]
    private Vector3 m_rightWeaponPos;

    private bool testspawn = false;

	#region Monobehaviour functions
	void Start()
    {
        m_spawnTimeElapsed = -5.0f; // give the player about 5 seconds
        m_spawnWaitTime = m_spawnTimingDifference;
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
            budbot.transform.position = new Vector2(randomX, randomY);
        }
    }
	#endregion
}
