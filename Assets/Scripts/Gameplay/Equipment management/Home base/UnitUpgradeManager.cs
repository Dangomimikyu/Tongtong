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
	private HealthBarController m_healthBarFill;
	[SerializeField]
	private EquipmentChangeManager m_equipmentManager;

	public bool unitCurrentlySelected = false; // flag for whether a unit is currently being selected or not

	private WeaponAttributes m_weaponAttributes;
	private UnitDataManager m_unitDataManager;
	private UnitData m_currentData;
	private AccountInformation m_playerAccount;
	private int currentUnitIndex;

	#region Monobehaviour functions
	private void Start()
	{
		m_unitDataManager = GameObject.FindGameObjectWithTag("UnitManager").GetComponent<UnitDataManager>();
		m_weaponAttributes = GameObject.FindGameObjectWithTag("WeaponAttributes").GetComponent<WeaponAttributes>();

		ReturnUnit(); // init the upgrade UI
	}
	#endregion

	#region Button functions
	// button selection for units
	public void EditUnit(int index)
	{
		Debug.Log("Edit unit: " + index);
		currentUnitIndex = index;
		m_currentData = m_unitDataManager.activeUnitData[index];
		unitCurrentlySelected = true;
		m_equipmentManager.EditUnit(m_currentData);

		m_unitDisplayImage.gameObject.SetActive(true);
		m_unitDisplayImage.sprite = m_tongbot;
		m_healthBarFill.SetMaxHealth(m_currentData.maxHealth);
		m_healthBarFill.UpdateHealth(m_currentData.currentHealth);
		Debug.Log("curr health " + m_currentData.currentHealth);
	}

	public void ReturnUnit()
	{
		m_currentData = null;
		m_equipmentManager.EditUnit(m_currentData); // will send null
		unitCurrentlySelected = false;
		m_unitDisplayImage.gameObject.SetActive(false);
		m_healthBarFill.SetMaxHealth(1.0f);
		m_healthBarFill.UpdateHealth(0.0f);
	}
	#endregion

	#region Post selection functions
	public void RepairUnit()
	{
		if (m_currentData.currentHealth == m_currentData.maxHealth)
        {
			Debug.Log("trying to repair while at full health");
			return;
        }

		// update local instance of player information
		m_playerAccount = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<AccountInformation>();

        if (m_playerAccount.money < 10)
        {
			Debug.Log("not enough money to repair");
			return;
        }

		// minus money
		m_playerAccount.money -= 10;

		// set unit health to full
		m_currentData.currentHealth = m_currentData.maxHealth;

		// update fill
		m_healthBarFill.UpdateHealth(m_currentData.maxHealth);
    }

	public void UpgradeUnit()
	{
		// update local instance of player information
		m_playerAccount = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<AccountInformation>();

		if (m_playerAccount.money < 30)
        {
			Debug.Log("not enough money to upgrade");
			return;
        }

		m_playerAccount.money -= 30;

		// each upgrade makes the unit 1.1x stronger
		m_currentData.maxHealth *= 1.1f;

		++m_currentData.unitLevel;

		if (m_currentData.unitLevel == 3 || m_currentData.unitLevel == 5)
        {
			m_weaponAttributes.GetShieldData(m_currentData.unitLevel / 2); // make use of int division (3 / 2 == 1)
        }
	}

	public void ChangeUnitWeapon()
	{

	}
	#endregion
}
