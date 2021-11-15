using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttributes : MonoBehaviour
{
	// define the types of weapon
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

	public GameObject gun_radioPrefab;
	public GameObject gun_pistolPrefab;
	public GameObject gun_riflePrefab;
	public GameObject gun_sniperPrefab;
	public GameObject gun_rocketPrefab;
	public GameObject gun_laserPrefab;

	public GameObject bullet_radioPrefab;
	public GameObject bullet_pistolPrefab;
	public GameObject bullet_riflePrefab;
	public GameObject bullet_sniperPrefab;
	public GameObject bullet_rocketPrefab;
	public GameObject bullet_laserPrefab;

	#region Retrieval functions
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
}
