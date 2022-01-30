
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSaveButtonBehaviour : MonoBehaviour
{
	public void Save()
	{
		FileSaveManager sm = GameObject.FindGameObjectWithTag("SaveManager").GetComponent<FileSaveManager>();
		sm.Save();
	}
}
