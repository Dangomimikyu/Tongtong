using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DangoMimikyu.EventManagement;
using UnityEngine.SceneManagement;

public class UnitSpawner : MonoBehaviour
{
	[Header("External script references")]
	[SerializeField]
	private WeaponAttributes m_weaponAttributes;
	[SerializeField]
	private UnitDataManager m_unitDataManager;

	[Header("Unit specifications")]
	[SerializeField]
	private GameObject m_unitPrefab;
	[SerializeField]
	private Vector3 m_leftWeaponPos;
	[SerializeField]
	private Vector3 m_rightWeaponPos;

	[Header("Spawning specifications")]
	[SerializeField]
	private List<Vector3> m_baseSpawnLocations;
	[SerializeField]
	private Vector3 m_baseScaleSize;
	[SerializeField]
	private List<Vector3> m_fieldSpawnLocations;
	[SerializeField]
	private Vector3 m_fieldScaleSize;

	#region Monobehaviour functions
	void Start()
	{
		EventManager.instance.StartListening(GameEvents.Gameplay_UpdateUnits, SpawnUnits);
		EventManager.instance.StartListening(GameEvents.Misc_SceneChange, SpawnUnits);
	}
	#endregion

	#region Spawning functions
	private void SpawnUnits(EventArgumentData ead)
	{
		// check the currently active scene
		string currentSceneName = SceneManager.GetActiveScene().name;
		Debug.Log("current scene name: " + currentSceneName);

		// destroy the current UnitBehaviour GameObjects
		DestroyUnits();

		// don't need to spawn anything if it's the other scenes
		if (currentSceneName == "HomeBaseScene")
		{
			// spawn at home base locations
			Debug.Log("count: " + m_unitDataManager.activeUnitData.Count);

			for (int i = 0; i < m_unitDataManager.activeUnitData.Count; ++i)
			{

				GameObject unit = Instantiate(m_unitPrefab, transform);
				unit.transform.position = m_baseSpawnLocations[i];
				unit.transform.rotation = Quaternion.identity;
				unit.transform.localScale = m_baseScaleSize;
				unit.GetComponent<UnitBehaviour>().unitData = m_unitDataManager.activeUnitData[i];
				UnitBehaviour newBehaviour = unit.GetComponent<UnitBehaviour>();
				SpawnWeapon(unit, newBehaviour.unitData.leftWeapon, newBehaviour.unitData.rightWeapon);

				m_unitDataManager.activeUnits.Add(newBehaviour);
				EventManager.instance.DispatchEvent(GameEvents.Unit_Spawn, newBehaviour);
				Debug.Log("spawned at: " + unit.transform.position);
			}
		}
		else if (currentSceneName == "ExpeditionScene")
		{
			// spawn at battle positions
			for (int i = 0; i < m_unitDataManager.activeUnitData.Count; ++i)
			{

				GameObject unit = Instantiate(m_unitPrefab, transform);
				unit.transform.position = m_fieldSpawnLocations[i];
				unit.transform.rotation = Quaternion.identity;
				unit.transform.localScale = m_baseScaleSize;
				unit.GetComponent<UnitBehaviour>().unitData = m_unitDataManager.activeUnitData[i];
				UnitBehaviour newBehaviour = unit.GetComponent<UnitBehaviour>();
				SpawnWeapon(unit, newBehaviour.unitData.leftWeapon, newBehaviour.unitData.rightWeapon);

				m_unitDataManager.activeUnits.Add(newBehaviour);
				EventManager.instance.DispatchEvent(GameEvents.Unit_Spawn, newBehaviour);
				Debug.Log("spawned at: " + unit.transform.position);
			}
		}
	}

	private void SpawnWeapon(GameObject parent, Weapon left, Weapon right)
	{
		if (left != null)
		{
			GameObject leftWeaponPrefab = m_weaponAttributes.GetWeaponPrefab(left);
			//GameObject leftWeapon = Instantiate(leftWeaponPrefab, parent.transform.position + m_leftWeaponPos, Quaternion.identity, parent.transform);
			GameObject leftWeapon = Instantiate(leftWeaponPrefab, parent.transform.position, Quaternion.identity, parent.transform);
			leftWeapon.transform.localScale = Vector3.one;
			if (left.twoHanded)
			{
				leftWeapon.transform.localPosition = m_rightWeaponPos;
				return; // if the left hand one is two handed, no need to do anything for the right hand
			}
			else
			{
				leftWeapon.transform.localPosition = m_leftWeaponPos;
			}
		}
        else
        {
			Debug.Log("left hand weapon was null");
        }

		if (right != null)
		{
			GameObject rightWeaponPrefab = m_weaponAttributes.GetWeaponPrefab(right);
			GameObject rightWeapon = Instantiate(rightWeaponPrefab, parent.transform.position + m_rightWeaponPos, Quaternion.identity, parent.transform);
			rightWeapon.transform.localPosition = m_rightWeaponPos;
			rightWeapon.transform.localScale = Vector3.one;
		}
		else
        {
			Debug.Log("right hand weapon was null");
        }
	}
	#endregion

	#region Destroy functions
	private void DestroyUnits()
	{
		foreach (UnitBehaviour ub in m_unitDataManager.activeUnits)
		{
			Destroy(ub.gameObject);
		}
	}
	#endregion
}
