using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DangoMimikyu.EventManagement;

public class QuestManager : MonoBehaviour
{
	[SerializeField]
    private List<Quest> m_questList = new List<Quest>();
	private Quest m_activeQuest = null;
	[SerializeField]
	private AccountInformation m_playerAccountInformation;
	[SerializeField]
	private QuestGenerator m_questGenerator;
	//[SerializeField]
	public QuestSelectionUIHandler m_questSelectionUIHandler = null;

	~QuestManager()
	{
		EventManager.instance.StopListening(GameEvents.Gameplay_QuestEnd, CompleteQuest);
		EventManager.instance.StopListening(GameEvents.Gameplay_QuestAbandoned, AbandonedQuest);
		EventManager.instance.StopListening(GameEvents.Misc_SceneChange, AddQuestListToUI);
	}

	#region Monobehaviour functions
	private void Start()
	{
		EventManager.instance.StartListening(GameEvents.Gameplay_QuestEnd, CompleteQuest);
		EventManager.instance.StartListening(GameEvents.Gameplay_QuestAbandoned, AbandonedQuest);
		EventManager.instance.StartListening(GameEvents.Misc_SceneChange, AddQuestListToUI);

		// make a test quest first for demo purposes
		//CreateTestQuest();
		InitQuestList();
	}

	private void OnDisable()
	{
		EventManager.instance.StopListening(GameEvents.Gameplay_QuestEnd, CompleteQuest);
		EventManager.instance.StopListening(GameEvents.Gameplay_QuestAbandoned, AbandonedQuest);
		EventManager.instance.StopListening(GameEvents.Misc_SceneChange, AddQuestListToUI);
	}
	#endregion

	#region Quest creation functions
	private void CreateTestQuest()
	{
		Quest newQuest = new Quest("Mvmt && atk demo", "tutorial demo quest", 10, 5);
		newQuest.questManager = this;
		m_activeQuest = newQuest;
		m_questList.Add(newQuest);
	}

	private void InitQuestList()
	{
		m_questList.Clear(); // empty the list first
		m_questList = m_questGenerator.GenerateQuestList();
	}
	#endregion

	#region UI functions
	private void PopulateQuestUI()
	{
		//m_questSelectionUIHandler = GameObject.FindGameObjectWithTag("QuestSelectionHandler").GetComponent<QuestSelectionUIHandler>();

		if (m_questSelectionUIHandler == null)
		{
			Debug.LogError("cmon man bruh");
		}
		foreach (Quest q in m_questList)
		{
			m_questSelectionUIHandler.AddQuestEntry(q);
		}
	}
	#endregion

	#region Information getter functions
	public Quest GetCurrentQuest()
	{
		return m_activeQuest;
	}

	// [to remove] shouldn't be depending on getting the whole quest list
	public List<Quest> GetQuestList()
	{
		return m_questList;
	}

	public Quest GetQuestFromIndex(int index)
	{
		return m_questList[index];
	}
    #endregion

    #region Quest action functions
	public void StartQuest()
    {
		if (m_questSelectionUIHandler.GetSelectionIndex() == -1)
		{
			Debug.LogError("trying to start quest without having selected a quest");
			return;
		}

		// start the current quest
		m_activeQuest = m_questList[m_questSelectionUIHandler.GetSelectionIndex()];
    }

	public void AbandonQuest()
    {
		//m_activeQuest = null;
    }
    #endregion

    #region Quest payout functions
    private void CompleteQuest(EventArgumentData ead)
	{
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

	#region Quest UI functions
	private void AddQuestListToUI(EventArgumentData ead)
	{
		string currentSceneName = SceneManager.GetActiveScene().name;
		if (currentSceneName == "HomeBaseScene")
		{
			PopulateQuestUI();
		}
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
