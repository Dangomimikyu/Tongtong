using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DangoMimikyu.EventManagement;

public class QuestManager : MonoBehaviour
{
    private List<Quest> m_questList = new List<Quest>();
	private Quest m_activeQuest = null;
	[SerializeField]
	private AccountInformation m_playerAccountInformation;

	~QuestManager()
	{
		EventManager.instance.StopListening(GameEvents.Gameplay_QuestEnd, CompleteQuest);
		EventManager.instance.StopListening(GameEvents.Gameplay_QuestAbandoned, AbandonedQuest);
	}

	#region Monobehaviour functions
	private void Start()
	{
		EventManager.instance.StartListening(GameEvents.Gameplay_QuestEnd, CompleteQuest);
		EventManager.instance.StartListening(GameEvents.Gameplay_QuestAbandoned, AbandonedQuest);

		// make a test quest first for demo purposes
		CreateTestQuest();
	}

	private void OnDisable()
	{
		EventManager.instance.StopListening(GameEvents.Gameplay_QuestEnd, CompleteQuest);
		EventManager.instance.StopListening(GameEvents.Gameplay_QuestAbandoned, AbandonedQuest);
	}
	#endregion

	#region Quest creation functions
	private void CreateTestQuest()
	{
		Quest newQuest = new Quest("Mvmt && atk demo", 10);
		newQuest.questManager = this;
		m_activeQuest = newQuest;
		m_questList.Add(newQuest);
	}
	#endregion

	#region Information getter functions
	public Quest GetCurrentQuest()
	{
		return m_activeQuest;
	}
	#endregion

	#region Quest payout functions
	private void CompleteQuest(EventArgumentData ead)
	{
		//m_playerAccountInformation.ReceiveRewards((Quest)ead.eventParams[0]);
		Debug.Log("quest name: " + m_activeQuest.questName);
		m_playerAccountInformation.ReceiveRewards(m_activeQuest);
	}

	public void CompleteQuest(Quest q)
	{
		Debug.Log("quest name: " + q.questName);
		m_playerAccountInformation.ReceiveRewards(q);
	}

	private void AbandonedQuest(EventArgumentData ead)
	{
		Debug.Log("Quest failed: " + m_activeQuest.questName);
		m_playerAccountInformation.ReceiveRewards(m_activeQuest);
	}
	#endregion

	#region Rewards functions
	private void TestAddRewards()
	{
		QuestRewards qr = new QuestRewards();
		qr.money = 30;
		m_activeQuest.AddReward(qr);
	}
	#endregion
}
