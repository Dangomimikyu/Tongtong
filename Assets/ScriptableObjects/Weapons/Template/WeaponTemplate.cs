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
	[Tooltip("Price of this weapon in the shop")]
	public int shopPrice;
	[Tooltip("bullet information for this weapon")]
	public WeaponAttributes.BulletData bulletData;
	[Tooltip("Weapon sprite")]
	public Sprite weaponSprite;
	[Tooltip("Weapon type")]
	public WeaponAttributes.WeaponType weaponType;
}
