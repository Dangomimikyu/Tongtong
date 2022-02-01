using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using DangoMimikyu.EventManagement;

public class FileSaveManager : MonoBehaviour
{
	#region Save structs
	public struct SaveObject
	{
		// player account information
		public int playerMoney;
		public int playerLevel;
		public float currentExp;
		public float neededExp;

		// unit information
		public List<UnitDataSave> unitdataSaves;

		// weapon purchase information
		public WeaponShopSave weaponShopSaves;
    }

	[System.Serializable]
	public struct UnitDataSave
	{
		public int level;
		public int shieldLevel;
		public float currentHealth;
		public float maxHealth;
		public WeaponAttributes.WeaponType leftWeapon;
		public WeaponAttributes.WeaponType rightWeapon;
	}

	[System.Serializable]
	public struct WeaponShopSave
	{
		public List<bool> weaponUnlockedList;
	}
	#endregion

	[Header("Object references")]
	[SerializeField]
	private AccountInformation m_accountInfo;
	[SerializeField]
	private UnitDataManager m_dataManager;
	[SerializeField]
	private WeaponAttributes m_weaponAttributes;

	private string m_savePath;
	private int m_saveStateCount = 0;

	#region Monobehaviour functions
	private void Start()
	{
		EventManager.instance.StartListening(GameEvents.Misc_SaveReady, SaveReady);

		m_savePath = Application.dataPath + "/SaveFiles/";

		if (!Directory.Exists(m_savePath))
			Directory.CreateDirectory(m_savePath);
	}

	private void OnApplicationQuit()
	{
		// save one more time when the game is closed
		Save();
	}
	#endregion

	#region Event handling functions
	private void SaveReady(EventArgumentData ead)
	{
		SaveObject currentSave = Load();

		// update account info
		m_accountInfo.UpdateInfo(currentSave);

		// update unit data
		m_dataManager.UpdateUnitList(currentSave);

		// update weapon purchase status
		m_weaponAttributes.UpdatePurchaseStatus(currentSave);
	}
	#endregion

	#region External call functions
	public void Save()
	{
		SaveObject saveObject = new SaveObject();
		// player account data
		saveObject.playerMoney = m_accountInfo.money;
		saveObject.playerLevel = m_accountInfo.GetPlayerLevel();
		saveObject.currentExp = m_accountInfo.GetCurrentExp();
		saveObject.neededExp = m_accountInfo.GetNeededExp();

		//saveObject.unitDataList = m_dataManager.activeUnitData;

		// player unit data
		List<UnitDataSave> tempSaveList = new List<UnitDataSave>();
		foreach (UnitData ud in m_dataManager.activeUnitData)
        {
			UnitDataSave tempUDsave = new UnitDataSave();
			tempUDsave.level = ud.unitLevel;
			tempUDsave.shieldLevel = ud.shieldData.level;
			tempUDsave.currentHealth = ud.currentHealth;
			tempUDsave.maxHealth = ud.maxHealth;
			tempUDsave.leftWeapon = ud.leftWeapon.weaponType;
			tempUDsave.rightWeapon = ud.leftWeapon.twoHanded ? WeaponAttributes.WeaponType.None : ud.rightWeapon.weaponType;
			tempSaveList.Add(tempUDsave);
		}
		saveObject.unitdataSaves = tempSaveList;

		// weapon purchase data
		saveObject.weaponShopSaves = m_weaponAttributes.GetSaveWeaponData();

		SaveToFile(JsonConvert.SerializeObject(saveObject));
	}

	public SaveObject Load()
	{
		string json = LoadFromFile();
		return JsonConvert.DeserializeObject<SaveObject>(json);
	}
	#endregion

	#region Local saving functions
	private void SaveToFile(string json)
	{
		File.WriteAllText(m_savePath + "save_" + ++m_saveStateCount + ".txt", json);
	}

	private string LoadFromFile()
	{
		DirectoryInfo dirInfo = new DirectoryInfo(m_savePath);
		FileInfo[] allSaveFiles = dirInfo.GetFiles("*.txt");
		FileInfo latestSave = null;
		m_saveStateCount = allSaveFiles.Length;

		// get the latest save file
		foreach (FileInfo fi in allSaveFiles)
		{
			if (latestSave == null)
				latestSave = fi;

			latestSave = fi.LastWriteTime > latestSave.LastWriteTime ? fi : latestSave;
		}

		Debug.Log("file read name: " + latestSave.Name);

		return latestSave == null ? null : File.ReadAllText(latestSave.FullName);
	}
	#endregion
}
