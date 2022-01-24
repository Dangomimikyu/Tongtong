using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitUpgradeManager : MonoBehaviour
{
	[Header("Sprite references")]
	[SerializeField]
	private Sprite m_tongbot;

	[Header("Object references")]
	[SerializeField]
	private Image m_unitDisplayImage;
	[SerializeField]
	private Image m_healthBarFill;

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
		// minus money
		// set unit health to full
		// update fill
	}

	public void UpgradeUnit()
	{

	}
	#endregion
}
