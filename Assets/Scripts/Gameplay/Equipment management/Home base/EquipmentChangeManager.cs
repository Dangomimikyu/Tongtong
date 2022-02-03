using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentChangeManager : MonoBehaviour
{
	[Header("Object references")]
	[SerializeField]
	private Canvas m_equipmentChangeCanvas;
	[SerializeField]
	private Image m_leftWeaponImage;
	[SerializeField]
	private Image m_rightWeaponImage;
	[SerializeField]
	private UnitData m_currentSelectedUnit = null;

	private WeaponTemplate m_currentSelectedWeapon = null;
	private WeaponAttributes m_weaponAttributes = null;
	private UnitDataManager m_unitDataManager = null;

	#region Monobehaviour functions
	private void Start()
	{
		m_equipmentChangeCanvas.gameObject.SetActive(false);
		m_weaponAttributes = GameObject.FindGameObjectWithTag("WeaponAttributes").GetComponent<WeaponAttributes>();
		m_unitDataManager = GameObject.FindGameObjectWithTag("UnitManager").GetComponent<UnitDataManager>();
	}
	#endregion

	#region Selection functions
	public void EditUnit(UnitData ud)
	{
		if (ud != null)
		{
			m_currentSelectedUnit = ud;
			UpdateWeaponImages();
		}
		else
		{
			m_currentSelectedUnit = null;
		}
	}
	#endregion

	#region UI functions
	public void UpdateWeaponImages()
	{
		m_leftWeaponImage.sprite = m_weaponAttributes.GetWeaponSprite(m_currentSelectedUnit.leftWeapon.weaponType);
		if (m_currentSelectedUnit.leftWeapon.twoHanded)
		{
			m_rightWeaponImage.sprite = m_leftWeaponImage.sprite;
		}
		else
		{
			m_rightWeaponImage.sprite = m_weaponAttributes.GetWeaponSprite(m_currentSelectedUnit.rightWeapon.weaponType);
		}
	}

	public void ToggleDisplayCanvas(bool enable)
	{
		if (m_currentSelectedUnit != null)
		{
			m_equipmentChangeCanvas.gameObject.SetActive(enable);
		}
	}
	#endregion

	#region Change weapon functions
	public void ChangeCurrentSelectedWeapon(WeaponTemplate wt)
	{
		m_currentSelectedWeapon = wt;
	}

	// this is meant for UI buttons to call
	public void ChangeWeapon(bool left)
	{
		if (m_weaponAttributes.GetIsTwohanded(m_currentSelectedWeapon.weaponType))
		{
			// change sprites
			m_leftWeaponImage.sprite = m_currentSelectedWeapon.weaponSprite;
			m_rightWeaponImage.sprite = m_currentSelectedWeapon.weaponSprite;

			// update unit data
			m_unitDataManager.UpdateUnitWeapon(m_currentSelectedUnit, m_currentSelectedWeapon.weaponType, true);
		}
		else
		{
			if (left)
			{
				// change sprites
				m_leftWeaponImage.sprite = m_currentSelectedWeapon.weaponSprite;

				// update unit data
				m_unitDataManager.UpdateUnitWeapon(m_currentSelectedUnit, m_currentSelectedWeapon.weaponType, true);
			}
			else
			{
				m_rightWeaponImage.sprite = m_currentSelectedWeapon.weaponSprite;
				m_unitDataManager.UpdateUnitWeapon(m_currentSelectedUnit, m_currentSelectedWeapon.weaponType, false);
			}
		}
	}
	#endregion
}
