using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DangoMimikyu.EventManagement;

public class BulletBehaviour : MonoBehaviour
{
	private BulletInformation m_bulletInfo;

	#region Monobehaviour functions
	private void Start()
	{
		m_bulletInfo = GetComponent<BulletInformation>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{

		if (collision.gameObject.tag == "EnemyUnit" && m_bulletInfo.friendly) // player bullet hit enemy
		{
			// destroy this object then deal damage to enemy
			Debug.Log("hit enemy");
			EnemyBehaviour eb = collision.gameObject.GetComponent<EnemyBehaviour>();
			//eb.enemyData.health -= m_bulletInfo.damage;
			eb.TakeDamage(m_bulletInfo.damage);
			if (eb.enemyData.health <= 0.0f)
			{
				EventManager.instance.DispatchEvent(GameEvents.Enemy_Died, eb);
				Destroy(eb.gameObject); // temp for now
			}
			else
			{
				Debug.Log("enemy health: " + eb.enemyData.health);
				EventManager.instance.DispatchEvent(GameEvents.Enemy_Damaged, eb);
			}

			Destroy(gameObject); // destroy the bullet
		}
		else if (collision.gameObject.tag == "PlayerUnit" && !m_bulletInfo.friendly) // enemy bullet hit player
		{
			Debug.Log("hit player");
			UnitBehaviour ub = collision.gameObject.GetComponent<UnitBehaviour>();
			//ub.unitData.health -= m_bulletInfo.damage;
			ub.TakeDamage(m_bulletInfo.damage);
			if (ub.unitData.health <= 0.0f)
			{
				EventManager.instance.DispatchEvent(GameEvents.Unit_Died, ub);
				Destroy(ub.gameObject);
			}
			else
			{
				Debug.Log("player health: " + ub.unitData.health);
				// send event to update the player unit's health
				EventManager.instance.DispatchEvent(GameEvents.Unit_Damaged, ub);
			}

			Destroy(gameObject);
		}
		else if (collision.gameObject.tag == "ViewBound")
		{
			// destroy this object since it went beyond the screen view
			Debug.Log("bullet hit viewbound");
			Destroy(gameObject);
		}
	}
	#endregion
}
