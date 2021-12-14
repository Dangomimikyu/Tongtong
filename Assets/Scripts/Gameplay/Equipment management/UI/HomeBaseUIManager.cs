using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HomeBaseUIManager : MonoBehaviour
{
    [Header("Object references")]
    [SerializeField]
    private Canvas m_popupCanvas;

	#region Monobehaviour functions
	private void Start()
    {
        ToggleEquipmentPopup(false);
    }

	private void Update()
	{

	}
    #endregion

    #region Visibility functions
    public void ToggleEquipmentPopup(bool enable)
	{
        m_popupCanvas.gameObject.SetActive(enable);
	}
    #endregion

    /* drag and drop functions
	private void Update()
    {
        if (m_dragged)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.Translate(mousePos);
        }
    }

    private void OnMouseDown()
    {
        m_dragged = true;
        m_boxCollider.isTrigger = true;
    }

    private void OnMouseUp()
    {
        m_dragged = false;
        m_boxCollider.isTrigger = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "UnitEditingUIBox")
        {
            m_inBox = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "UnitEditingUIBox")
        {
            m_inBox = false;
        }
    }
    */
}
