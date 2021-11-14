using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DangoMimikyu.EventManagement;

public class UnitData
{
	public uint id;
	public float health = 10.0f;
	public Weapon leftWeapon = null;
	public Weapon rightWeapon = null;
}

public class UnitDataManager : MonoBehaviour
{
	[Header("Object references")]
	public List<UnitBehaviour> activeUnits;      // list of active units

	[SerializeField]
	private string m_homeBaseSceneName;
	[SerializeField]
	private string m_expeditionSceneName;

	public string scene;

	#region Monobehaviour functions
	void Start()
	{
		// listen to events
		EventManager.instance.StartListening(GameEvents.Gameplay_UpdateUnits, SceneChangeHandler);

		// init the active units to have 3 people to begin with
		for (int i = 0; i < 3; ++i)
		{
			UnitBehaviour tempub = new UnitBehaviour();
			activeUnits.Add(tempub);
		}
	}

	void Update()
	{

	}
	#endregion

	#region Modify active units list
	private void SceneChangeHandler(EventArgumentData ead)
	{
		string currentSceneName = SceneManager.GetActiveScene().name;
		Debug.Log("current scene: " + currentSceneName);

		if (currentSceneName == m_expeditionSceneName)
		{
			// should spawn units by dispatching event
			// at this point units should already be equipped with a weapon, if no weapon then they will be unable to attack and will just stand there
		}
		else if (currentSceneName == m_homeBaseSceneName)
		{
			// clear the current list
			ClearActiveList();
			// add the list of unit behaviour to currently active (this is done to update the list on what weapon each unit has and their position in line
			List<UnitBehaviour> ub = (List<UnitBehaviour>)ead.eventParams[0];
			for (int i = 0; i < ub.Count; ++i)
			{
				// equip a weapon for testing
				Weapon wpn = new Weapon();
				wpn.weaponType = WeaponAttributes.WeaponType.Pistol;
				ModifyWeapons(ub[i], wpn, true);
				activeUnits.Add(ub[i]);
			}
		}
	}

	private void ClearActiveList()
	{
		for (int i = 0; i < activeUnits.Count; ++i)
		{
			activeUnits[i] = null;
		}
	}
	#endregion

	#region Equipment functions
	private void ModifyWeapons(EventArgumentData ead)
	{

	}

	private void ModifyWeapons(UnitBehaviour ub, Weapon w, bool left)
	{
		// todo: move the currently held weapon (if any) into the inventory
		if (left)
		{
			ub.unitData.leftWeapon = w;
		}
		else
		{
			ub.unitData.rightWeapon = w;
		}
	}

	private void ModifyArmour(EventArgumentData ead)
	{

	}
	#endregion
}
