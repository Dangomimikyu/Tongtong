using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class HomeBaseUIManager : MonoBehaviour
{
    [Header("UI references")]
    [SerializeField]
    private TMP_Text m_moneyText;
    [SerializeField]
    private Canvas m_questSelectionCanvas;
    [SerializeField]
    private List<InventoryEquipButton> m_inventoryButtons;

    [Header("Manager references")]
    [SerializeField]
    private UnitUpgradeManager m_upgradeManager;
    [SerializeField]
    private EquipmentChangeManager m_equipmentManager;


	#region Monobehaviour functions
	private void Start()
    {
        ToggleEquipmentPopup(false);
        UpdateMoney();
        m_questSelectionCanvas.gameObject.SetActive(false);
    }
    #endregion

    #region UI functions
    public void ToggleEquipmentPopup(bool enable)
	{
        m_equipmentManager.ToggleDisplayCanvas(enable);
	}

    public void ToggleQuestSelectionUI()
	{
        m_questSelectionCanvas.gameObject.SetActive(!m_questSelectionCanvas.gameObject.activeInHierarchy);
	}

    public void UpdateMoney()
    {
        m_moneyText.text = "Money:" + GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<AccountInformation>().money.ToString();
    }
	#endregion
}
