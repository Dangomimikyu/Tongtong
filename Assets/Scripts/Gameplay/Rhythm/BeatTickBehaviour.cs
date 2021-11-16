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

    void Update()
    {

    }
	#endregion

	#region Movement functions
	public void Move(float beatDuration)
	{
		m_rectTransform = GetComponent<RectTransform>();
		m_rectTransform.DOAnchorPosX(850.0f, beatDuration).SetEase(Ease.Linear).OnComplete(() => { Destroy(gameObject); }); // translate the beat tick then destroy the canvas when it's done
	}
	#endregion
}
