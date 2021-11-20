// note: weapon in this context is defined as "something equip-able that's directly used in combat" so this includes shields
using UnityEngine;
using WeaponType = WeaponAttributes.WeaponType;

public class Weapon
{
	public Transform firingPoint;
	public WeaponType weaponType;
	public float damage;
	public bool twoHanded;

	public Weapon()
	{
		weaponType = WeaponType.None;
		damage = 0.0f;
	}

	public void Shoot()
	{

	}
}
