// note: this class is meant to identify when an enemy spawns on top of another, making it hard for the player to hit it
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnChecker : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "EnemyUnit")
		{
			Debug.Log("despawning this unit");
			//EnemyInformationHandler eih = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyInformationHandler>();

			//Debug.LogError("removed removed");
			//eih.RemoveFromList(collision.gameObject.GetComponent<EnemyBehaviour>());
			Destroy(collision.gameObject);
		}
	}
}
