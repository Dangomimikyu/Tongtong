using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DangoMimikyu.EventManagement;
using DG.Tweening;

public class QuestEndUIHandler : MonoBehaviour
{
    [Header("Object references")]
    [SerializeField]
    private CanvasGroup m_questEndCanvasGroup;

    ~QuestEndUIHandler()
	{
        EventManager.instance.StopListening(GameEvents.Gameplay_QuestEnd, ToggleRewardPanel);
	}

    #region Monobehaviour functions
    void Start()
    {
        m_questEndCanvasGroup.alpha = 0; // turn off the quest overlay
        EventManager.instance.StartListening(GameEvents.Gameplay_QuestEnd, ToggleRewardPanel);
    }

	private void OnDisable()
	{
        EventManager.instance.StopListening(GameEvents.Gameplay_QuestEnd, ToggleRewardPanel);
    }
	#endregion

	#region Reward panel functions
	private void ToggleRewardPanel(EventArgumentData ead)
	{
        //m_questEndCanvasGroup.gameObject.SetActive(!m_questEndCanvasGroup.gameObject.activeSelf);
        m_questEndCanvasGroup.DOFade(1.0f, 1.0f).SetEase(Ease.Linear);

        //m_questEndCanvas.DOFade(1.0f, 1.0f).SetEase(Ease.InBounce);
    }
    #endregion
}
