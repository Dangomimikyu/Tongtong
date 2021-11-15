using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryEquipButton : MonoBehaviour
{
    private Button m_button;


	#region Monobehaviour functions
	private void Awake()
	{
        m_button = GetComponent<Button>();
		m_button.onClick.AddListener(test);
	}

	void Start()
    {

    }

    void Update()
    {

    }
	#endregion

    private void test()
	{

	}
}
