// note:
using UnityEngine;
using WeaponType = WeaponAttributes.WeaponType;
using BulletData = WeaponAttributes.BulletData;

public class Weapon
{
	public Transform firingPoint;		// Transform to spawn bullets
	public WeaponType weaponType;       // define the type of weapon
	public float bulletVel;				// starting speed of bullet
	public float damage;				// damage of each bullet
	public float burstCooldownTimer;	// time between each burst shot
	public int burstBulletcount;		// number of bullets to shoot in a burst
	public int shootCooldown;			// number of metronome beats to wait before being able to shoot again
	public bool twoHanded;              // define whether this is a twohanded weapon or not
	public bool isPlayer;               // define whether this weapon will hurt player units or enemy units

	#region Constructors
	public Weapon()
	{
		weaponType = WeaponType.None;
	}

	public Weapon(WeaponType type, bool isPlayer)
	{
		WeaponAttributes wpnAttributes = GameObject.FindGameObjectWithTag("WeaponAttributes").GetComponent<WeaponAttributes>();
		BulletData bd = wpnAttributes.GetBulletData(type);

		// set variables (firingPoint will be set during SpawnWeapon)
		weaponType = type;
		bulletVel = bd.velocity;
		damage = bd.damage;
		burstCooldownTimer = bd.burstCooldown;
		burstBulletcount = bd.burstBulletCount;
		shootCooldown = bd.ShootCooldown;
		this.isPlayer = isPlayer;
		twoHanded = wpnAttributes.GetIsTwohanded(type);
	}
	#endregion
}
