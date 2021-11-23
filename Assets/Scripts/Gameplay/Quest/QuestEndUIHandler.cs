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
    private Image m_rewardPanel;

    ~QuestEndUIHandler()
	{
        EventManager.instance.StopListening(GameEvents.Gameplay_QuestEnd, ToggleRewardPanel);
	}

    #region Monobehaviour functions
    void Start()
    {
        m_rewardPanel.gameObject.SetActive(false); // turn off the quest overlay
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
        m_rewardPanel.gameObject.SetActive(!m_rewardPanel.gameObject.activeSelf);
        m_rewardPanel.DOFade(1.0f, 1.0f).SetEase(Ease.InBounce);
	}
    #endregion
}
