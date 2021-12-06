using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletInformation : MonoBehaviour
{
	// meant to be used as an information class with the bullet's information
	public float damage;
	public bool friendly;

	#region Var setting functions
	public void SetInfo(Weapon wpn)
	{
		damage = wpn.damage;
		friendly = wpn.isPlayer;
	}
	#endregion
}
