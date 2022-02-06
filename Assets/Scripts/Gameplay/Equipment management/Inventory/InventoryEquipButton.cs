using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryEquipButton : MonoBehaviour
{
    [Header("Object references")]
    [SerializeField]
    private TMP_Text m_weaponText;
    [SerializeField]
    private WeaponTemplate m_weaponInfo;
    [SerializeField]
    private Image m_lockOverlay;
    [SerializeField]
    private HomeBaseUIManager m_mainUIManager;
    [SerializeField]
    private EquipmentChangeManager m_equipManager;

	#region Monobehaviour functions
	void Start()
    {
        m_weaponText.text = "";
        UpdateLockOverlay();
        UpdateTextInfo();
    }

    void Update()
    {

    }
	#endregion

	#region UI functions
	private void UpdateTextInfo()
	{
        string output = "Dmg: " + m_weaponInfo.bulletData.damage + "\n";

        if (m_weaponInfo.bulletData.burstBulletCount > 0)
		{
            output += "burst: yes\n";
            output += "shots: " + m_weaponInfo.bulletData.burstBulletCount;
		}
        else
		{
            output += "burst: no";
		}

        if (!m_weaponInfo.shopPurchased)
		{
            output += "\nprice: " + m_weaponInfo.shopPrice;
		}

        Debug.Log("output: " + output);

        m_weaponText.text = output;
	}

    private void UpdateLockOverlay()
	{
        if (m_weaponInfo.shopPurchased)
		{
            m_lockOverlay.gameObject.SetActive(false);
		}
	}
	#endregion

	#region Button click functions
    public void SelectInventoryItem()
	{
        if (!m_weaponInfo.shopPurchased)
		{
            // check if player has enough money and if so then edit WeaponTemplate info and remove the lock overlay
            AccountInformation ai = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<AccountInformation>();
            if (ai.money >= m_weaponInfo.shopPrice)
			{
                Debug.Log("purchased item");
                ai.money -= m_weaponInfo.shopPrice;
                m_weaponInfo.shopPurchased = true;
                UpdateLockOverlay();
                m_mainUIManager.UpdateMoney();
			}
        }
        else // player already owns this item
		{
            // update the currently selected WeaponTemplate in EquipmentChangeManager
            m_equipManager.ChangeCurrentSelectedWeapon(m_weaponInfo);

            // bring up equipment change screen
            m_mainUIManager.ToggleEquipmentPopup(true);
        }
    }
	#endregion
}
