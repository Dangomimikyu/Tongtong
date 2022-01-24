using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitUpgradeManager : MonoBehaviour
{
	[Header("Object references")]
	[SerializeField]
	private Image unitDisplayImage;

	private UnitDataManager m_unitDataManager;

	#region Monobehaviour functions
	private void Start()
	{
		m_unitDataManager = GameObject.FindGameObjectWithTag("UnitManager").GetComponent<UnitDataManager>();
	}
	#endregion

	public void EditUnit(int index)
	{
		Debug.Log("Edit unit: " + index);

	}

	#region Post selection functions
	public void RepairUnit()
	{

	}

	public void UpgradeUnit()
	{

	}
	#endregion
}
