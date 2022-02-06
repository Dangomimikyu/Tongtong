using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DangoMimikyu.EventManagement;
using DG.Tweening;
using TMPro;

public class QuestEndUIHandler : MonoBehaviour
{
    [Header("Object references")]
    [SerializeField]
    private CanvasGroup m_questEndCanvasGroup;
    [SerializeField]
    private TMP_Text m_questName;
    [SerializeField]
    private TMP_Text m_questRewards;

    private QuestManager m_questManager;

    ~QuestEndUIHandler()
	{
        EventManager.instance.StopListening(GameEvents.Gameplay_QuestEnd, ToggleRewardPanelSuccess);
        EventManager.instance.StopListening(GameEvents.Gameplay_QuestAbandoned, ToggleRewardPanelFailed);
    }

    #region Monobehaviour functions
    void Start()
    {
        m_questEndCanvasGroup.gameObject.SetActive(false);
        m_questEndCanvasGroup.alpha = 0; // turn off the quest overlay
        EventManager.instance.StartListening(GameEvents.Gameplay_QuestEnd, ToggleRewardPanelSuccess);
        EventManager.instance.StartListening(GameEvents.Gameplay_QuestAbandoned, ToggleRewardPanelFailed);
    }

    private void OnDisable()
	{
        EventManager.instance.StopListening(GameEvents.Gameplay_QuestEnd, ToggleRewardPanelSuccess);
        EventManager.instance.StopListening(GameEvents.Gameplay_QuestAbandoned, ToggleRewardPanelFailed);
    }
    #endregion

    #region Reward panel functions
    private void ToggleRewardPanelSuccess(EventArgumentData ead)
	{
        m_questEndCanvasGroup.gameObject.SetActive(true);

        m_questManager = GameObject.FindGameObjectWithTag("QuestManager").GetComponent<QuestManager>();
        m_questName.text = m_questManager.GetCurrentQuest()?.questName;
        string rewards = "Money: " + m_questManager.GetCurrentQuest()?.questRewards.money + "\n";
        rewards += "Exp: " + m_questManager.GetCurrentQuest()?.questRewards.exp + "\n";
		m_questRewards.text = rewards;

		m_questEndCanvasGroup.DOFade(1.0f, 1.0f).SetEase(Ease.Linear);
    }

    private void ToggleRewardPanelFailed(EventArgumentData ead)
	{
        m_questEndCanvasGroup.gameObject.SetActive(true);

        m_questManager = GameObject.FindGameObjectWithTag("QuestManager").GetComponent<QuestManager>();
        m_questName.text = m_questManager.GetCurrentQuest()?.questName;

        string questFailMessage = "Quest failed";
        m_questRewards.text = questFailMessage;

        m_questEndCanvasGroup.DOFade(1.0f, 1.0f).SetEase(Ease.Linear);
    }
    #endregion
}
