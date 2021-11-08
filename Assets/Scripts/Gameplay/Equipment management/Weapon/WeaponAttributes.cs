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
}
