using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class MainMenuUIButtonBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField]
	private Image m_leftArrow = null;
	[SerializeField]
	private Image m_rightArrow = null;

	#region Monobehaviour functions
	public void OnPointerEnter(PointerEventData eventData)
	{
	}

	public void OnPointerExit(PointerEventData eventData)
	{
	}
	#endregion

	#region Coroutines

	#endregion
}
