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
	private UnitDataManager m_unitDataManager;

	[Header("unit specifications")]
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
		EventManager.instance.StartListening(GameEvents.Misc_SceneChange, SpawnUnits);
		m_unitDataManager = GetComponent<UnitDataManager>();
	}

	void Update()
	{

	}
	#endregion

	#region Spawning functions
	private void SpawnUnits(EventArgumentData ead)
	{
		// check the currently active scene
		string currentSceneName = (string)ead.eventParams[0];

		// don't need to spawn anything if it's the other scenes
		if (currentSceneName == "HomeBaseScene")
		{
			// spawn at home base locations
			for (int i = 0; i < m_unitDataManager.activeUnits.Count; ++i)
			{
				GameObject unit = Instantiate(m_unitPrefab, transform);
				//GameObject unit = Instantiate(m_unitPrefab, m_baseSpawnLocations[i], Quaternion.identity);
				unit.transform.position		= m_baseSpawnLocations[i];
				unit.transform.rotation		= Quaternion.identity;
				unit.transform.localScale	= m_baseScaleSize;

				UnitData ud = unit.GetComponent<UnitBehaviour>().unitData;
                SpawnWeapon(unit, ud.leftWeapon, ud.rightWeapon);
                Debug.Log("spawned at: " + unit.transform.position);
			}
		}
		else if (currentSceneName == "ExpeditionScene")
		{
			// spawn at battle positions
		}
	}

	private void SpawnWeapon(GameObject parent, Weapon left, Weapon right)
	{
		if (left != null)
		{
			GameObject leftWeaponPrefab = m_weaponAttributes.GetWeaponPrefab(left);
			GameObject leftWeapon = Instantiate(leftWeaponPrefab, parent.transform.position + m_leftWeaponPos, Quaternion.identity, parent.transform);
			leftWeapon.transform.localScale = Vector3.one;
			
			if (left.twoHanded)
				return; // if the left hand one is two handed, no need to do anything for the right hand
		}
        else
        {
			Debug.Log("left hand weapon was null");
        }

		if (right != null)
		{
			GameObject rightWeaponPrefab = m_weaponAttributes.GetWeaponPrefab(right);
			GameObject rightWeapon = Instantiate(rightWeaponPrefab, parent.transform.position + m_rightWeaponPos, Quaternion.identity, parent.transform);
			rightWeapon.transform.localScale = Vector3.one;
		}
		else
        {
			Debug.Log("right hand weapon was null");
        }
	}
	#endregion
}
