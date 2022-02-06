// note: this was for the previous form of beat UI that looks like Crypt of the Necrodancer
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DangoMimikyu.EventManagement;

public class BeatTickBehaviour : MonoBehaviour
{
	private RectTransform m_rectTransform;
	#region Monobehaviour functions
	void Start()
    {
		m_rectTransform = GetComponent<RectTransform>();
    }
	#endregion

	#region Movement functions
	public void Move(float beatDuration, bool RtoL)
	{
		m_rectTransform = GetComponent<RectTransform>();
		if (!RtoL)
		{
			m_rectTransform.anchoredPosition = new Vector2(850.0f, m_rectTransform.anchoredPosition.y);
		}
			m_rectTransform.DOAnchorPosX(0, beatDuration).SetEase(Ease.Linear).OnComplete(() => { Destroy(gameObject); }); // translate the beat tick then destroy the canvas when it's done
	}
	#endregion
}
