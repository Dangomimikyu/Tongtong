// note: each enemy will be responsible for their own attacking timing
// they are not bound by rhythm so they just attack at random intervals
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DangoMimikyu.EventManagement;

public class EnemyData
{
    public float health;
    public Weapon weapon; // enemies will only have one weapon
}

public class EnemyBehaviour : MonoBehaviour
{
    public EnemyData enemyData = new EnemyData();
    private int beatWaitCounter = 4; // how many beats to wait before deciding next action
    private EnemyInformationHandler m_enemyManager;

    private UnitObjectSpawner m_unitObjSpawner;

    #region Monobehaviour functions
    void Start()
    {
        enemyData.health = 20;
        m_enemyManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyInformationHandler>();
        m_unitObjSpawner = GameObject.FindGameObjectWithTag("UnitManager").GetComponent<UnitObjectSpawner>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "EnemyActiveTrigger")
		{
			Debug.Log("enemy making active");
			EventManager.instance.DispatchEvent(GameEvents.Enemy_Active, this); // pass this EnemyBehaviour as the params
		}
	}
	#endregion

	#region AI decision functions
	public void MakeDecision()
	{
        if (--beatWaitCounter <= 0)
        {
            // reset the beat wait counter
            beatWaitCounter = Random.Range(m_enemyManager.minWaitBeats, m_enemyManager.maxWaitBeats);

            Attack();
        }
	}
    #endregion

    #region Attacking functions
    private void Attack()
    {
        m_unitObjSpawner.SpawnEnemyBullet(gameObject, enemyData.weapon);
    }
    #endregion
}
