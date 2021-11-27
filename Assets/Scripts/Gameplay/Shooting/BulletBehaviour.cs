using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
	#region Monobehaviour functions
	private void OnTriggerEnter2D(Collider2D collision)
	{
		// collided with something
		if (collision.gameObject.tag == "EnemyUnit")
		{
			// destroy this object then deal damage to enemy
			Destroy(gameObject);
		}
		else if (collision.gameObject.tag == "ViewBound")
		{
			// destroy this object since it went beyond the screen view
			Destroy(gameObject);
		}
	}
	#endregion
}
