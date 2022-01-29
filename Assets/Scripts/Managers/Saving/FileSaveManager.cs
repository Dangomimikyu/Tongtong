using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileSaveManager : MonoBehaviour
{
	[Header("Object references")]
	[SerializeField]
	private AccountInformation m_accountInfo;
	[SerializeField]
	private UnitDataManager m_dataManager;

	private string m_savePath = Application.dataPath + "/SaveFiles/";

	#region Monobehaviour functions
	private void Awake()
	{
		if (!Directory.Exists(m_savePath))
			Directory.CreateDirectory(m_savePath);
	}

	private void Start()
	{

	}
	#endregion

	public void Save()
	{

	}

	public string Load()
	{
		DirectoryInfo dirInfo = new DirectoryInfo(m_savePath);
		FileInfo[] allSaveFiles = dirInfo.GetFiles("*.txt");
		FileInfo latestSave = null;

		// get the latest save file
		foreach (FileInfo fi in allSaveFiles)
		{
			if (latestSave == null)
				latestSave = fi;

			latestSave = fi.LastWriteTime > latestSave.LastWriteTime ? fi : latestSave;
		}

		return latestSave == null ? null : File.ReadAllText(latestSave.FullName);
	}
}
