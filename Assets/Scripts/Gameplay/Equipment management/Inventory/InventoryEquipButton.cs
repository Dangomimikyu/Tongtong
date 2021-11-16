using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class InventoryEquipButton : MonoBehaviour
{
    private Button m_button;
    private PlayerInputs m_playerInputs;

	#region Monobehaviour functions
	private void Awake()
	{
        m_playerInputs = new PlayerInputs();
        m_button = GetComponent<Button>();
		m_button.onClick.AddListener(Select);
	}

	void Start()
    {

    }

    void Update()
    {

    }
	#endregion

	private void Select()
    {
        // note: will be called by the UI button manager
        // change UI to be the one over the characters
    }
}
