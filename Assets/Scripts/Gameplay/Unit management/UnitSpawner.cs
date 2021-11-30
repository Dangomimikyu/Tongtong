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

	~UnitSpawner()
	{
		EventManager.instance.StopListening(GameEvents.Gameplay_UpdateUnits, SpawnUnits);
		EventManager.instance.StopListening(GameEvents.Misc_SceneChange, SpawnUnits);
	}

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

		// destroy the UnitBehaviour GameObjects from the previous scene
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
				SpawnWeapon(unit, ref newBehaviour.unitData.leftWeapon, ref newBehaviour.unitData.rightWeapon);

				m_unitDataManager.activeUnits.Add(newBehaviour);
				EventManager.instance.DispatchEvent(GameEvents.Unit_Spawn, newBehaviour);
				Debug.Log("spawned at (home): " + unit.transform.position);
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
				unit.transform.localScale = m_fieldScaleSize;
				unit.GetComponent<UnitBehaviour>().unitData = m_unitDataManager.activeUnitData[i];
				UnitBehaviour newBehaviour = unit.GetComponent<UnitBehaviour>();
				SpawnWeapon(unit, ref newBehaviour.unitData.leftWeapon, ref newBehaviour.unitData.rightWeapon);

				m_unitDataManager.activeUnits.Add(newBehaviour);
				EventManager.instance.DispatchEvent(GameEvents.Unit_Spawn, newBehaviour);
				Debug.Log("spawned at (exp): " + unit.transform.position);
			}
		}
	}

	private void SpawnWeapon(GameObject parent, ref Weapon left, ref Weapon right)
	{
		if (left != null)
		{
			GameObject leftWeaponPrefab = m_weaponAttributes.GetWeaponPrefab(left);
			GameObject leftWeapon = Instantiate(leftWeaponPrefab, parent.transform.position, Quaternion.identity, parent.transform);
			left.firingPoint = leftWeaponPrefab.GetComponent<WeaponInformation>().weaponFiringPoint;
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
			right.firingPoint = rightWeaponPrefab.GetComponent<WeaponInformation>().weaponFiringPoint;
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
		if (m_unitDataManager.activeUnits.Count == 0)
			return; // no need to continue further if theres nothing to destroy

		List<UnitBehaviour> ublist = new List<UnitBehaviour>();
		//List<UnitBehaviour> currentActive = m_unitDataManager.activeUnits;
		//foreach (UnitBehaviour ub in currentActive)
		//{
		//	//Destroy(ub.gameObject);
		//	m_unitDataManager.activeUnits.Remove(ub);
		//	ublist.Add(ub);
		//}

		int count = m_unitDataManager.activeUnits.Count;
		for (int i = 0; i < count; ++i)
		{
			ublist.Add(m_unitDataManager.activeUnits[0]);
			m_unitDataManager.activeUnits.RemoveAt(0);
		}

		foreach (UnitBehaviour ub in ublist)
		{
			Destroy(ub.gameObject);
		}
	}
	#endregion
}
