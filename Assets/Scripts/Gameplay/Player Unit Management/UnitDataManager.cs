using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DangoMimikyu.EventManagement;

[System.Serializable]
public class UnitData
{
	public uint id;
	public int unitLevel = 1;
	public float currentHealth = 30.0f;
	public float maxHealth = 30.0f;
	public WeaponAttributes.ShieldData shieldData = new WeaponAttributes.ShieldData();
	[SerializeField]
	public Weapon leftWeapon = null;
	public Weapon rightWeapon = null;
}

public class UnitDataManager : MonoBehaviour
{
	[Header("Object references")]
	public List<UnitBehaviour> activeUnits = new List<UnitBehaviour>();		// list of active units
	public List<UnitData> activeUnitData = new List<UnitData>();			// list of active units' data

	[SerializeField]
	private string m_homeBaseSceneName;
	[SerializeField]
	private string m_expeditionSceneName;

	public string scene;

	~UnitDataManager()
	{
		EventManager.instance.StopListening(GameEvents.Gameplay_UpdateUnits, SceneChangeHandler);
		EventManager.instance.StopListening(GameEvents.Unit_Died, RemoveFromList);
	}

	#region Monobehaviour functions
	void Start()
	{
		// listen to events
		EventManager.instance.StartListening(GameEvents.Gameplay_UpdateUnits, SceneChangeHandler);
		EventManager.instance.StartListening(GameEvents.Unit_Died, RemoveFromList);
	}

	private void OnDisable()
	{
		EventManager.instance.StopListening(GameEvents.Gameplay_UpdateUnits, SceneChangeHandler);
		EventManager.instance.StopListening(GameEvents.Unit_Died, RemoveFromList);
	}
	#endregion

	#region Modify active units list
	private void SceneChangeHandler(EventArgumentData ead)
	{
		string currentSceneName = SceneManager.GetActiveScene().name;
		Debug.Log("current scene: " + currentSceneName);


		if (currentSceneName == "MainMenuScene")
		{
			// clear the current list
			ClearActiveList();
			// add the list of unit behaviour to currently active (this is done to update the list on what weapon each unit has and their position in line
			List<UnitData> ud = (List<UnitData>)ead.eventParams[0];
			Debug.Log("called mainmenu, count: " + ud.Count);
			for (int i = 0; i < ud.Count; ++i)
			{
				// equip a weapon for testing
				activeUnitData.Add(ud[i]);
			}
		}
		else if (currentSceneName == m_homeBaseSceneName)
		{
			// add the list of unit behaviour to currently active (this is done to update the list on what weapon each unit has and their position in line
			List<UnitBehaviour> ub = (List<UnitBehaviour>)ead.eventParams[0];
			Debug.Log("called home, count: " + ub.Count);
			for (int i = 0; i < ub.Count; ++i)
			{
				activeUnits.Add(ub[i]);
			}
		}
	}

	public void UpdateUnitList(FileSaveManager.SaveObject so)
	{
		WeaponAttributes wa = GameObject.FindGameObjectWithTag("WeaponAttributes").GetComponent<WeaponAttributes>();

		ClearActiveList();
		activeUnitData.Clear();
		List<UnitData> tempUDList = new List<UnitData>();

		foreach (FileSaveManager.UnitDataSave uds in so.unitdataSaves)
		{
			UnitData tempUD = new UnitData();

			// left weapon
			Weapon wLeft = new Weapon(uds.leftWeapon, true);
			wLeft.twoHanded = wa.GetIsTwohanded(uds.leftWeapon);
			tempUD.leftWeapon = wLeft;

			// right weapon
			if (!wLeft.twoHanded)
			{
				Weapon wRight = new Weapon(uds.rightWeapon, true);
				tempUD.rightWeapon = wRight;
			}

			// shield
			tempUD.shieldData = wa.GetShieldData(uds.shieldLevel).shieldData;

			// health
			tempUD.maxHealth = uds.maxHealth;
			tempUD.currentHealth = uds.currentHealth;

			tempUDList.Add(tempUD);
		}

		Debug.Log("tempUDlist count: " + tempUDList.Count);

		foreach (UnitData ud in tempUDList)
		{
			activeUnitData.Add(ud);
		}
	}

	public void UpdateUnitWeapon(UnitData ud, WeaponAttributes.WeaponType wt, bool left)
	{
		// if the current weapon is a two-haded weapon, set both to pistol
		if (ud.leftWeapon.twoHanded)
		{
			ud.leftWeapon = new Weapon(WeaponAttributes.WeaponType.Pistol, true);
			ud.rightWeapon = new Weapon(WeaponAttributes.WeaponType.Pistol, true);
		}

		Weapon newWeapon = new Weapon(wt, true);
		if (left)
		{
			ud.leftWeapon = newWeapon;

			if (newWeapon.twoHanded)
			{
				ud.rightWeapon = null;
			}
		}
		else
		{
			ud.rightWeapon = newWeapon;
		}

		// refresh the scene units
		GetComponent<UnitSpawner>().RefreshHomeUnits();

		// change weapon sprites
		EquipmentChangeManager ecm = GameObject.FindGameObjectWithTag("EquipmentChangeManager").GetComponent<EquipmentChangeManager>();
		ecm.UpdateWeaponImages();
	}

	public void ClearActiveList()
	{
		for (int i = 0; i < activeUnits.Count; ++i)
		{
			//activeUnits[i] = null;
			Destroy(activeUnits[i].gameObject);
		}
		activeUnits.Clear();
	}

	private void RemoveFromList(EventArgumentData ead)
	{
		activeUnits.Remove((UnitBehaviour)ead.eventParams[0]);

		// trigger lose event if all units have been killed
		if (activeUnits.Count <= 0) // no more player units on the field
		{
			EventManager.instance.DispatchEvent(GameEvents.Gameplay_QuestAbandoned);
		}
	}

	public UnitBehaviour GetFrontUnit()
	{
		return activeUnits[activeUnits.Count - 1];
	}
	#endregion
}
