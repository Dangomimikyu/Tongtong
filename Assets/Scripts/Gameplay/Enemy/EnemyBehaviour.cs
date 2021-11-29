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

    ~EnemyBehaviour()
	{
        EventManager.instance.StopListening(GameEvents.Gameplay_MetronomeBeat, MakeDecision);
    }

    #region Monobehaviour functions
    void Start()
    {
        enemyData.health = 20;
        EventManager.instance.StartListening(GameEvents.Gameplay_MetronomeBeat, MakeDecision);
    }

    void Update()
    {

    }

	private void OnDisable()
	{
        EventManager.instance.StopListening(GameEvents.Gameplay_MetronomeBeat, MakeDecision);
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "ViewBound")
		{
            Debug.Log("enemy making active");
            EventManager.instance.DispatchEvent(GameEvents.Enemy_Active, this); // pass this EnemyBehaviour as the params
		}
	}
	#endregion

	#region AI decision functions
	private void MakeDecision(EventArgumentData ead)
	{

	}
    #endregion

    #region Attacking functions
    private void Attack()
    {

    }
    #endregion
}
