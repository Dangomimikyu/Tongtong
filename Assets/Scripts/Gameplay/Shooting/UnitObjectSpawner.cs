// note this class is for the spawning of various objects by units (both allied and enemy)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitObjectSpawner : MonoBehaviour
{
	[SerializeField]
	private GameObject m_bulletPrefab;
	[SerializeField]
	private GameObject m_shieldPrefab;

	public void SpawnBullet(GameObject tongbot, Transform firingPoint, float vel, float damage)
	{
		//Transform temp = tongbot.transform.position + firingPoint.localPosition;
		Transform bulletPos = tongbot.transform;
		GameObject bullet = Instantiate(m_bulletPrefab, tongbot.transform);
		bullet.transform.position += firingPoint.localPosition;
		bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(vel, 0.0f));

		Destroy(bullet, 10.0f); // set a hard limit for the lifetime of a bullet just in case it somehow avoids getting destroyed
	}

	public void SpawnShield(GameObject tongbot, Transform shieldPos, float duration)
	{

	}
}
