using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DangoMimikyu.EventManagement;

public class TestAddUnit : MonoBehaviour
{
	[SerializeField]
	private ShieldTemplate m_shieldInformation;

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
			Weapon wLeft = new Weapon(WeaponAttributes.WeaponType.Pistol, true);
			//wLeft.weaponType = WeaponAttributes.WeaponType.Pistol;
			//wLeft.twoHanded = true;
			Weapon wRight = new Weapon(WeaponAttributes.WeaponType.Rifle, true);
			//wRight.weaponType = WeaponAttributes.WeaponType.Rifle;
			//wRight.twoHanded = true;
			tempud.leftWeapon = wLeft;
			tempud.rightWeapon = wRight;
			tempud.shieldData = m_shieldInformation.shieldData;
			tempud.currentHealth = tempud.maxHealth / 2;
			udlist.Add(tempud);
		}
		Debug.Log("dispatching event");
		EventManager.instance.DispatchEvent(GameEvents.Gameplay_UpdateUnits, udlist);

		// change scene after updating units
		SceneController sc = GameObject.FindGameObjectWithTag("SceneController").GetComponent<SceneController>();
		sc.LoadSceneString("HomeBaseScene");
	}
}
