using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

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


		public List<UnitData> unitDataList;
	}

	public struct UnitDataSave
	{
		public int level;
		public float currentHealth;
		public float maxHealth;
	}

	public struct WeaponShopSave
	{
		public bool pistolUnlocked;
		public bool rifleUnlocked;
		public bool sniperlUnlocked;
		public bool rocketUnlocked;
		public bool LaserUnlocked;
	}
	#endregion

	[Header("Object references")]
	[SerializeField]
	private AccountInformation m_accountInfo;
	[SerializeField]
	private UnitDataManager m_dataManager;

	private string m_savePath;
	private int m_saveStateCount = 0;

	#region Monobehaviour functions
	private void Start()
	{
		m_savePath = Application.dataPath + "/SaveFiles/";

		if (!Directory.Exists(m_savePath))
			Directory.CreateDirectory(m_savePath);
	}
	#endregion

	#region External call functions
	public void Save()
	{
		SaveObject saveObject = new SaveObject();
		saveObject.playerMoney = m_accountInfo.money;
		saveObject.playerLevel = m_accountInfo.GetPlayerLevel();
		saveObject.currentExp = m_accountInfo.GetCurrentExp();
		saveObject.neededExp = m_accountInfo.GetNeededExp();
		saveObject.unitDataList = m_dataManager.activeUnitData;
		SaveToFile(JsonConvert.SerializeObject(saveObject));
	}

	public void Load()
	{

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

		return latestSave == null ? null : File.ReadAllText(latestSave.FullName);
	}
	#endregion
}
