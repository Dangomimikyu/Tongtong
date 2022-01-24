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
        EventManager.instance.StopListening(GameEvents.Gameplay_QuestEnd, ToggleRewardPanel);
        EventManager.instance.StopListening(GameEvents.Gameplay_QuestAbandoned, ToggleRewardPanel);
    }

    #region Monobehaviour functions
    void Start()
    {
        m_questEndCanvasGroup.alpha = 0; // turn off the quest overlay
        EventManager.instance.StartListening(GameEvents.Gameplay_QuestEnd, ToggleRewardPanel);
        EventManager.instance.StartListening(GameEvents.Gameplay_QuestAbandoned, ToggleRewardPanel);
    }

    private void OnDisable()
	{
        EventManager.instance.StopListening(GameEvents.Gameplay_QuestEnd, ToggleRewardPanel);
        EventManager.instance.StopListening(GameEvents.Gameplay_QuestAbandoned, ToggleRewardPanel);
    }
    #endregion

    #region Reward panel functions
    private void ToggleRewardPanel(EventArgumentData ead)
	{
        m_questManager = GameObject.FindGameObjectWithTag("QuestManager").GetComponent<QuestManager>();
        m_questName.text = m_questManager.GetCurrentQuest()?.questName;
        string rewards = "Money: " + m_questManager.GetCurrentQuest()?.questRewards.money + "\n";
        rewards += "Exp: " + m_questManager.GetCurrentQuest()?.questRewards.exp + "\n";
		m_questRewards.text = rewards;

		//m_questEndCanvasGroup.gameObject.SetActive(!m_questEndCanvasGroup.gameObject.activeSelf);
		m_questEndCanvasGroup.DOFade(1.0f, 1.0f).SetEase(Ease.Linear);

        //m_questEndCanvas.DOFade(1.0f, 1.0f).SetEase(Ease.InBounce);
    }
    #endregion
}
