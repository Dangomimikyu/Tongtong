using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttributes : MonoBehaviour
{
	#region Data type definitions
	// define the types of weapon
	[System.Serializable]
	public enum WeaponType
	{
		None,
		Pistol,
		Rifle,
		Sniper,
		Rocket,
		Lazer,
	}

	[System.Serializable]
	public struct BulletData
	{
		[Tooltip("Bullet's initial velocity")]
		public float velocity;
		[Tooltip("Bullet damage")]
		public float damage;
		[Tooltip("Cooldown duration for weapons that fire in burst shots")]
		public float burstCooldown; // for weapons like rifle that shoot in 3 bullet bursts
		[Tooltip("Number of shots to first in one burst shot")]
		public int burstBulletCount;
		[Tooltip("Cooldown duration between shots, counted in metronome beats")]
		public int ShootCooldown;
	}

	// define the types of shield
	[System.Serializable]
	public enum ShieldType
	{
		None,
		Buckler,
		Kite,
		Tower
	}

	[System.Serializable]
	public struct ShieldData
	{
		[Tooltip("Shield level")]
		public int level;
		[Tooltip("Shield health")]
		public float health;
		[Tooltip("Despawn time")]
		public float lifetimeDur;
		[Tooltip("Vertical Scaling")]
		public Vector3 vertScaling;
	}
	#endregion

	#region All weapon information
	private List<GameObject> gunPrefabList = new List<GameObject>();
	private List<GameObject> bulletPrefabList = new List<GameObject>();
	private List<GameObject> shieldPrefabList = new List<GameObject>();

	private List<BulletData> m_bulletStatsList = new List<BulletData>();

	[Header("Weapon info")]
	[SerializeField]
	private List<WeaponTemplate> m_weaponInformation;
	[SerializeField]
	private List<WeaponType> m_twoHandedList;
	[SerializeField]
	private List<ShieldTemplate> m_shieldInformation;
	#endregion

	#region Monobehaviour functions
	private void Start()
	{
		for (int i = 0; i < m_weaponInformation.Count; ++i)
		{
			gunPrefabList.Add(m_weaponInformation[i].weaponPrefab);
			bulletPrefabList.Add(m_weaponInformation[i].bulletPrefab);
			m_bulletStatsList.Add(m_weaponInformation[i].bulletData);
		}

		for (int i = 0; i < m_shieldInformation.Count; ++i)
        {
			shieldPrefabList.Add(m_shieldInformation[i].shieldPrefab);
        }

		Debug.Log("finished starting");
	}
    #endregion

    #region Retrieval functions
		#region Prefab
		public GameObject GetWeaponPrefab(Weapon weapon)
		{
			switch (weapon.weaponType)
			{
				default:
				case WeaponType.None:
					Debug.LogWarning("unable to get a weapon prefab because this weapontype is none");
					return null;
				case WeaponType.Pistol:
				case WeaponType.Rifle:
				case WeaponType.Sniper:
				case WeaponType.Rocket:
				case WeaponType.Lazer:
				return gunPrefabList[(int)weapon.weaponType - 1];
			}
		}

		public GameObject GetBulletPrefab(Weapon weapon)
		{
			switch (weapon.weaponType)
			{
				default:
				case WeaponType.None:
					Debug.LogWarning("unable to get a bullet prefab because this weapontype is none");
					return null;
				case WeaponType.Pistol:
				case WeaponType.Rifle:
				case WeaponType.Sniper:
				case WeaponType.Rocket:
				case WeaponType.Lazer:
				return bulletPrefabList[(int)weapon.weaponType - 1];
			}
		}

		public GameObject GetShieldPrefab(ShieldData shieldData)
		{
			if (shieldData.level > 0)
			{
				return shieldPrefabList[shieldData.level - 1];
			}
			else
			{
				Debug.LogError("invalid shield level");
				return null;
			}
		}
		#endregion

		#region Bullet values
		public float GetBulletVelocity(Weapon wpn)
		{
			return m_bulletStatsList[(int)wpn.weaponType].velocity;
		}

		public float GetBulletDamage(Weapon wpn)
		{
			return m_bulletStatsList[(int)wpn.weaponType].damage;
		}

		public BulletData GetBulletData(WeaponType weaponType)
		{
			return m_bulletStatsList[(int)weaponType];
		}
		#endregion

		#region Two handed functions
		public bool GetIsTwohanded(WeaponType wpnType)
		{
			return m_twoHandedList.Contains(wpnType);
		}
		#endregion

		#region Weapon Data getters
		public WeaponTemplate GetWeaponData(int weaponType)
		{
			return m_weaponInformation[Mathf.Max(0, weaponType - 1)];
		}

		public WeaponTemplate GetWeaponData(WeaponType wpnType)
		{
			return m_weaponInformation[(int)wpnType]; // don't need the -1 because WeaponType will be a valid value
		}

		public ShieldTemplate GetShieldData(int currentLevel)
		{
			return m_shieldInformation[currentLevel]; // don't need to -1 because this is called by the upgrader which already has the number incremented
		}
		#endregion
	#endregion

	#region Save and Load
	public FileSaveManager.WeaponShopSave GetSaveWeaponData()
	{
		FileSaveManager.WeaponShopSave returnSave = new FileSaveManager.WeaponShopSave();
		returnSave.weaponUnlockedList = new List<bool>();
		foreach (WeaponTemplate wt in m_weaponInformation)
		{
			returnSave.weaponUnlockedList.Add(wt.shopPurchased);
		}
		return returnSave;
	}

	public void UpdatePurchaseStatus(FileSaveManager.SaveObject so)
	{
		List<bool> boughtList = so.weaponShopSaves.weaponUnlockedList;

		// this function is meant to be called when loading the save file
		for (int i = 0; i < m_weaponInformation.Count; ++i)
		{
			m_weaponInformation[i].shopPurchased = boughtList[i];
		}
	}
	#endregion
}
