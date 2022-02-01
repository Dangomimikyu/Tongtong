using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HomeBaseUIManager : MonoBehaviour
{
    [System.Serializable]
    public struct InventoryEquipmentButton
    {
        public Image weaponBackground;
        public Image lockOverlay;
    }


    [Header("Object references")]
    [SerializeField]
    private Canvas m_popupCanvas;
    [SerializeField]
    private Canvas m_questSelectionCanvas;
    [SerializeField]
    private List<InventoryEquipmentButton> m_inventoryButtons;

	#region Monobehaviour functions
	private void Start()
    {
        ToggleEquipmentPopup(false);
        m_questSelectionCanvas.gameObject.SetActive(false);
    }
    #endregion

    #region Visibility functions
    public void ToggleEquipmentPopup(bool enable)
	{
        m_popupCanvas.gameObject.SetActive(enable);
	}

    public void ToggleQuestSelectionUI()
	{
        m_questSelectionCanvas.gameObject.SetActive(!m_questSelectionCanvas.gameObject.activeInHierarchy);
	}
	#endregion

	#region Inventory UI buttons
    public void UpdateInventoryUI()
	{

	}
	#endregion
}
