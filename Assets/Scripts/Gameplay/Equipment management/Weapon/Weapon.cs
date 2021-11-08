// note: weapon in this context is defined as "something equip-able that's directly used in combat" so this includes shields
using WeaponType = WeaponAttributes.WeaponType;

public class Weapon
{
	public WeaponType weaponType;
	public float damage;
	public bool twoHanded;

	Weapon()
	{
		weaponType = WeaponType.None;
		damage = 1.0f;
	}

	public void Shoot()
	{

	}
}
