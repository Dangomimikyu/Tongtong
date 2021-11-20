using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DangoMimikyu.EventManagement;

public class TestAddUnit : MonoBehaviour
{
    void Start()
    {
		//List<UnitBehaviour> ublist = new List<UnitBehaviour>();
		List<UnitData> udlist = new List<UnitData>();
		for (int i = 0; i < 3; ++i)
		{
			//UnitBehaviour tempub = new UnitBehaviour();
			//Weapon wLeft = new Weapon();
			//wLeft.weaponType = WeaponAttributes.WeaponType.Pistol;
			//Weapon wRight = new Weapon();
			//wRight.weaponType = WeaponAttributes.WeaponType.Pistol;
			//tempub.unitData.leftWeapon = wLeft;
			//tempub.unitData.rightWeapon = wRight;
			//ublist.Add(tempub);

			UnitData tempud = new UnitData();
			Weapon wLeft = new Weapon();
			wLeft.weaponType = WeaponAttributes.WeaponType.Pistol;
			//wLeft.twoHanded = true;
			Weapon wRight = new Weapon();
			wRight.weaponType = WeaponAttributes.WeaponType.Rifle;
			//wRight.twoHanded = true;
			tempud.leftWeapon = wLeft;
			tempud.rightWeapon = wRight;
			udlist.Add(tempud);
		}
		Debug.Log("dispatching event");
		EventManager.instance.DispatchEvent(GameEvents.Gameplay_UpdateUnits, udlist);
	}
}
