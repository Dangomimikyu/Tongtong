using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DangoMimikyu.EventManagement;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance { private set; get; }
    private List<Quest> m_questList;
	private Quest m_activeQuest = null;
	private AccountInformation m_playerAccountInformation;

	~QuestManager()
	{
		//EventManager.instance.StopListening(GameEvents.Gameplay_QuestEnd, CompleteQuest);
	}

	#region Monobehaviour functions
	private void Awake()
	{
		if (!instance)
		{
			Debug.Log("created this instance of QuestManager");
			instance = this;
		}
		else
		{
			Debug.LogWarning("Existing QuestManager already exist but you're trying to make a new one. Will destroy the old one");
			Destroy(instance);
			instance = this;
		}

		m_playerAccountInformation = new AccountInformation();
	}

	private void Start()
	{
		//EventManager.instance.StartListening(GameEvents.Gameplay_QuestEnd, CompleteQuest);
		// make a test quest first for demo purposes
		CreateQuest();
	}
	#endregion

	#region Quest creation functions
	private void CreateQuest()
	{
		Quest newQuest = new Quest();
		newQuest.questRewards.money = 10;
		m_questList.Add(newQuest);
	}
	#endregion

	#region Quest payout functions
	private void CompleteQuest(EventArgumentData ead)
	{
		m_playerAccountInformation.ReceiveRewards((Quest)ead.eventParams[0]);
	}

	public void CompleteQuest(Quest q)
	{
		m_playerAccountInformation.ReceiveRewards(q);
	}
	#endregion
}
