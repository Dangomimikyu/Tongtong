using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
		// collided with something
		if (collision.gameObject.tag == "EnemyUnit")
		{
			// destroy this object then deal damage to enemy
			Debug.Log("hit enemy");
			EnemyBehaviour eb = collision.gameObject.GetComponent<EnemyBehaviour>();
			eb.enemyData.health -= m_bulletInfo.damage;
			if (eb.enemyData.health <= 0.0f)
			{
				Destroy(eb.gameObject); // temp for now
			}
			else
			{
				Debug.Log("enemy health: " + eb.enemyData.health);
			}

			Destroy(gameObject); // destroy the bullet
		}
		else if (collision.gameObject.tag == "ViewBound")
		{
			// destroy this object since it went beyond the screen view
			Debug.Log("hit out of camera");
			Destroy(gameObject);
		}
	}
	#endregion
}
