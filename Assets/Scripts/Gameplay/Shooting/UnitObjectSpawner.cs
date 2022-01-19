// note this class is for the spawning of various objects by units (both allied and enemy)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitObjectSpawner : MonoBehaviour
{
	[Header("External script references")]
	[SerializeField]
	private WeaponAttributes m_weaponAttributes;
	[Header("Prefab references")]
	[SerializeField]
	private GameObject m_bulletPrefab;
	[SerializeField]
	private GameObject m_shieldPrefab;

	#region Coroutines
	private IEnumerator BurstFire(GameObject tongBot, Weapon wpn)
	{
		int burstCount = 0;
		while (burstCount < wpn.burstBulletcount)
		{
			Debug.Log("firing burst shot: " + burstCount + " weapon type: " + wpn.weaponType);
			burstCount++;
			GameObject bullet = Instantiate(m_bulletPrefab, tongBot.transform);
			bullet.transform.position += wpn.firingPoint.localPosition;
			bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(wpn.bulletVel, 0.0f));

			// set whether the bullet is meant to hit enemies or players
			bullet.GetComponent<BulletInformation>().SetInfo(wpn);

			// set a hard limit for the lifetime of a bullet just in case it somehow avoids getting destroyed
			Destroy(bullet, 10.0f);
			yield return new WaitForSeconds(wpn.burstCooldownTimer);
		}

		//yield break;
	}
	#endregion

	#region Spawning functions
	public void SpawnPlayerBullet(GameObject tongBot, Weapon wpn, CommandAtrributes.Potency pot)
	{
		if (wpn == null)
			return; // weapon is null

		if (wpn.burstBulletcount > 0) // this is a burst fire weapon
		{
			Debug.Log("bursting bullet");
			StartCoroutine(BurstFire(tongBot, wpn));
		}
		else // this is a single first weapon
		{
			Debug.Log("firing single shot");
			GameObject bullet = Instantiate(m_bulletPrefab, tongBot.transform);
			bullet.transform.position += wpn.firingPoint.localPosition;
			bullet.transform.rotation = wpn.firingPoint.rotation;
			//bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(wpn.bulletVel, 0.0f));
			bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.forward);

			// set whether the bullet is meant to hit enemies or players
			bullet.GetComponent<BulletInformation>().SetInfo(wpn);

			// set a hard limit for the lifetime of a bullet just in case it somehow avoids getting destroyed
			Destroy(bullet, 10.0f);
		}
	}

	public void SpawnEnemyBullet(GameObject budbot, Weapon wpn)
	{
		if (wpn == null)
			return; // weapon is null

		GameObject bullet = Instantiate(m_bulletPrefab, budbot.transform);
		bullet.transform.position += wpn.firingPoint.localPosition;
		bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(-wpn.bulletVel, 0.0f));

		// set whether the bullet is meant to hit enemies or players
		bullet.GetComponent<BulletInformation>().SetInfo(wpn);

		// set a hard limit for the lifetime of a bullet just in case it somehow avoids getting destroyed
		Destroy(bullet, 10.0f);
	}

	public void SpawnShield(GameObject tongbot, Transform shieldPos, float duration)
	{

	}
	#endregion

	#region Utility functions
	private float GetBulletVelocity(Weapon wpn)
	{
		float vel = 0.0f;
		vel = m_weaponAttributes.GetBulletVelocity(wpn);
		return vel;
	}
	#endregion
}
