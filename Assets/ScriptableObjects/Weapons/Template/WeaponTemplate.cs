using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "WeaponTemplate", menuName = "Scriptable Weapon")]
public class WeaponTemplate : ScriptableObject
{
	[Tooltip("Weapon Prefab")]
	public GameObject weaponPrefab;
	[Tooltip("Bullet Prefab")]
	public GameObject bulletPrefab;
	[Tooltip("Whether this weapon has been purchased from the shop")]
	public bool shopPurchased;
	[Tooltip("bullet information for this weapon")]
	public WeaponAttributes.BulletData bulletData;
}
