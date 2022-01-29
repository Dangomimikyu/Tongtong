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

	private UnitDataManager m_unitDataManager;
	private UnitData m_currentData;
	private AccountInformation m_playerAccount;

	#region Monobehaviour functions
	private void Start()
	{
		m_unitDataManager = GameObject.FindGameObjectWithTag("UnitManager").GetComponent<UnitDataManager>();
	}
	#endregion

	public void EditUnit(int index)
	{
		Debug.Log("Edit unit: " + index);

		m_currentData = m_unitDataManager.activeUnitData[index];
		m_unitDisplayImage.sprite = m_tongbot;
		m_healthBarFill.SetMaxHealth(m_currentData.maxHealth);
		m_healthBarFill.UpdateHealth(m_currentData.currentHealth);
		Debug.Log("curr health " + m_currentData.currentHealth);
	}

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

		if (m_currentData.unitLevel == 3)
        {

        }
		else if (m_currentData.unitLevel == 5)
        {

        }
	}
	#endregion
}
