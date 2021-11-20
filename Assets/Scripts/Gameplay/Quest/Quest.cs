using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DangoMimikyu.EventManagement;

public struct QuestRewards
{
	public uint money;
	//CraftingMaterial[] materials;

}

public class Quest
{
	private string m_ownerAccountName = "";
	public string questName { private set; get; }
	private QuestManager m_questManager;
	public QuestRewards questRewards;

	#region Constructor
	public Quest()
	{
		questName = "defaultQuestName";
		questRewards.money = 1;
	}

	~Quest()
	{
		EventManager.instance.StopListening(GameEvents.Gameplay_QuestEnd, CompleteQuest);
	}
	#endregion

	#region Monobehaviour functions
	private void Start()
	{
		EventManager.instance.StartListening(GameEvents.Gameplay_QuestEnd, CompleteQuest);
	}

	private void OnDisable()
	{
		EventManager.instance.StopListening(GameEvents.Gameplay_QuestEnd, CompleteQuest);
	}
	#endregion

	#region Completion rewards
	public void CompleteQuest(EventArgumentData ead)
	{
		Debug.Log("completed quest");
	}
	#endregion
}
