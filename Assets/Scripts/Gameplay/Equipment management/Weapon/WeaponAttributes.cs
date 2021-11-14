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
	public GameObject gun_RiflePrefab;
	public GameObject gun_SniperPrefab;
	public GameObject gun_RocketPrefab;
	public GameObject gun_LazerPrefab;

	public GameObject bullet_radioPrefab;
	public GameObject bullet_pistolPrefab;
	public GameObject bullet_RiflePrefab;
	public GameObject bullet_SniperPrefab;
	public GameObject bullet_RocketPrefab;
	public GameObject bullet_LazerPrefab;

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
				return gun_RiflePrefab;
			case WeaponType.Sniper:
				return gun_SniperPrefab;
			case WeaponType.Rocket:
				return gun_RocketPrefab;
			case WeaponType.Lazer:
				return gun_LazerPrefab;
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
				return bullet_RiflePrefab;
			case WeaponType.Sniper:
				return bullet_SniperPrefab;
			case WeaponType.Rocket:
				return bullet_RocketPrefab;
			case WeaponType.Lazer:
				return bullet_LazerPrefab;
		}
	}
	#endregion
}
