using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUnitBehaviour : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D m_boxCollider;
    private bool m_inBox = false;
    private bool m_dragged = false;

    private void Awake()
    {
        m_boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
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
}
