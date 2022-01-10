// note: each enemy will be responsible for their own attacking timing
// they are not bound by rhythm so they just attack at random intervals
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DangoMimikyu.EventManagement;
using DG.Tweening;

public class EnemyData
{
    public float health;
    public Weapon weapon; // enemies will only have one weapon
}

public class EnemyBehaviour : MonoBehaviour
{
    private enum EnemyDecision
	{
        Move,
        Attack,
        Defend,
        Total
    }

    public EnemyData enemyData = new EnemyData();
    private int beatWaitCounter = 4; // how many beats to wait before deciding next action
    private EnemyInformationHandler m_enemyManager;
    private UnitDataManager m_unitDataManager;
    private UnitObjectSpawner m_unitObjSpawner;
    [SerializeField]
    private HealthBarController m_healthBarController;

    #region Monobehaviour functions
    void Start()
    {
        m_enemyManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyInformationHandler>();
        m_unitObjSpawner = GameObject.FindGameObjectWithTag("UnitManager").GetComponent<UnitObjectSpawner>();
        m_unitDataManager = GameObject.FindGameObjectWithTag("UnitManager").GetComponent<UnitDataManager>();
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
            int behaviour = Random.Range(0, (int)EnemyDecision.Total);

			switch ((EnemyDecision)behaviour)
			{
				case EnemyDecision.Move:
                    Debug.Log("enemy is move");
                    Walk();
					break;
				case EnemyDecision.Attack:
                    Debug.Log("enemy is attack");
                    Attack();
					break;
				case EnemyDecision.Defend:
                    Debug.Log("enemy is defend");
                    // currently doesn't do anything as at week 9
                    Defend();
					break;
				case EnemyDecision.Total:
					break;
				default:
					break;
			}
        }
	}
    #endregion

    #region Combat functions
    private void Walk()
	{
        if (gameObject != null)
        {
            // movement is done by moving 20% of the distance between this enemy and the frontmost player unit
            UnitBehaviour frontMostUnit = m_unitDataManager.GetFrontUnit();
            Vector3 frontUnitPos = frontMostUnit.gameObject.transform.position;
            float movementDistance = 0.2f * (frontUnitPos - transform.position).magnitude;
            Debug.Log("front unit position: " + frontUnitPos + " movement distance: " + movementDistance);
            transform.DOMoveX(transform.position.x - movementDistance, 1.0f);
        }
	}

    private void Attack()
    {
        if (gameObject != null)
		{
            m_unitObjSpawner.SpawnEnemyBullet(gameObject, enemyData.weapon);
		}
    }

    private void Defend()
	{
        // set up a shield in front
	}
	#endregion

	#region Health functions
	public void TakeDamage(float dmg)
	{
        enemyData.health -= dmg;
        m_healthBarController.UpdateHealth(enemyData.health);
	}

    public void SetMaxHealth(float health)
	{
        enemyData.health = health;
        m_healthBarController.SetMaxHealth(enemyData.health);
	}
	#endregion
}
