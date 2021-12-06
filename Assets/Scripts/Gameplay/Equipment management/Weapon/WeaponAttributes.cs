using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttributes : MonoBehaviour
{
	#region Data type definitions
	// define the types of weapon
	[System.Serializable]
	public enum WeaponType
	{
		None,
		Radio, // specifically for the team anchor
		Pistol,
		Rifle,
		Sniper,
		Rocket,
		Lazer,
	}

	[System.Serializable]
	public struct BulletData
	{
		[Tooltip("Bullet's initial velocity")]
		public float velocity;
		[Tooltip("Bullet damage")]
		public float damage;
		[Tooltip("Cooldown duration for weapons that fire in burst shots")]
		public float burstCooldown; // for weapons like rifle that shoot in 3 bullet bursts
		[Tooltip("Number of shots to first in one burst shot")]
		public int burstBulletCount;
		[Tooltip("Cooldown duration between shots, counted in metronome beats")]
		public int ShootCooldown;
	}
	#endregion

	#region Editor information
	[Header("Gun prefabs")]
	[SerializeField]
	GameObject gun_radioPrefab;
	[SerializeField]
	GameObject gun_pistolPrefab;
	[SerializeField]
	GameObject gun_riflePrefab;
	[SerializeField]
	GameObject gun_sniperPrefab;
	[SerializeField]
	GameObject gun_rocketPrefab;
	[SerializeField]
	GameObject gun_laserPrefab;

	[Header("Bullet prefabs")]
	[SerializeField]
	GameObject bullet_radioPrefab;
	[SerializeField]
	GameObject bullet_pistolPrefab;
	[SerializeField]
	GameObject bullet_riflePrefab;
	[SerializeField]
	GameObject bullet_sniperPrefab;
	[SerializeField]
	GameObject bullet_rocketPrefab;
	[SerializeField]
	GameObject bullet_laserPrefab;

	[Header("Weapon stats")]
	[SerializeField]
	private List<BulletData> m_bulletStatsList;
	[SerializeField]
	private List<WeaponType> m_twoHandedList;
	#endregion

	#region Retrieval functions
		#region Prefab
		public GameObject GetWeaponPrefab(Weapon weapon)
		{
			switch (weapon.weaponType)
			{
				default:
				case WeaponType.None:
					Debug.LogWarning("unable to get a weapon prefab because this weapontype is none");
					return null;
				case WeaponType.Radio:
					return gun_radioPrefab;
				case WeaponType.Pistol:
					return gun_pistolPrefab;
				case WeaponType.Rifle:
					return gun_riflePrefab;
				case WeaponType.Sniper:
					return gun_sniperPrefab;
				case WeaponType.Rocket:
					return gun_rocketPrefab;
				case WeaponType.Lazer:
					return gun_laserPrefab;
			}
		}

		public GameObject GetBulletPrefab(Weapon weapon)
		{
			switch (weapon.weaponType)
			{
				default:
				case WeaponType.None:
					Debug.LogWarning("unable to get a bullet prefab because this weapontype is none");
					return null;
				case WeaponType.Radio:
					return bullet_radioPrefab;
				case WeaponType.Pistol:
					return bullet_pistolPrefab;
				case WeaponType.Rifle:
					return bullet_riflePrefab;
				case WeaponType.Sniper:
					return bullet_sniperPrefab;
				case WeaponType.Rocket:
					return bullet_rocketPrefab;
				case WeaponType.Lazer:
					return bullet_laserPrefab;
			}
		}
	#endregion

		#region Bullet values
		public float GetBulletVelocity(Weapon wpn)
		{
			return m_bulletStatsList[(int)wpn.weaponType].velocity;
		}

		public float GetBulletDamage(Weapon wpn)
		{
			return m_bulletStatsList[(int)wpn.weaponType].damage;
		}

		public BulletData GetBulletData(WeaponType weaponType)
		{
			return m_bulletStatsList[(int)weaponType];
		}
		#endregion

		#region Two handed functions
		public bool GetIsTwohanded(WeaponType wpnType)
		{
			return m_twoHandedList.Contains(wpnType);
		}
		#endregion
	#endregion
}
