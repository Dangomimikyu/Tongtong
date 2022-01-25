using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditUnitDragBox : MonoBehaviour
{
	private BoxCollider2D m_boxCollider;
	#region Monobehaviour functions
	private void Awake()
	{
		m_boxCollider = GetComponent<BoxCollider2D>();
	}

	void Start()
	{
		
	}

	void Update()
	{
		
	}
    #endregion
}
