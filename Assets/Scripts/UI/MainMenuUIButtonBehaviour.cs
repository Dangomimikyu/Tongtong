using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class MainMenuUIButtonBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[Header("Object references")]
	[SerializeField]
	private Image m_leftArrow = null;
	[SerializeField]
	private Image m_rightArrow = null;

	[Header("Animation settings")]
	[SerializeField]
	private Vector3 m_offset;

	#region Monobehaviour functions
	public void OnPointerEnter(PointerEventData eventData)
	{
	}

	public void OnPointerExit(PointerEventData eventData)
	{
	}
	#endregion

	#region Coroutines
	public IEnumerator ArrowOut()
	{
		while (true)
		{

		}

		yield break;
	}

	public IEnumerator ArrowIn()
	{
		yield break;
	}
	#endregion
}
