using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DangoMimikyu.EventManagement;
using System;

public struct QuestRewards
{
	#region Operation overloads
	public static QuestRewards operator +(QuestRewards self, QuestRewards other)
	{
		self.money += other.money;
		return self;
	}

	public static QuestRewards operator -(QuestRewards self, QuestRewards other)
	{
		self.money -= other.money;
		return self;
	}
	#endregion

	public uint money;
	//CraftingMaterial[] materials;
}

public class Quest
{
	private string m_ownerAccountName = "";
	public string questName { private set; get; }
	public QuestRewards questRewards;
	public QuestManager questManager;
	public bool active;

	#region Constructor
	public Quest()
	{
		questName = "defaultQuestName";
		questRewards.money = 1;
	}

	public Quest(string name = "defaultQuestName", uint moneyReward = 1)
	{
		questName = name;
		questRewards.money = moneyReward;
	}

	~Quest()
	{
	}
	#endregion

	#region reward functions
	public void AddReward(QuestRewards qr)
	{
		questRewards += qr;
	}

	//public QuestRewards GetRewards()
	//{
	//	return null;
	//}
	#endregion

	#region Completion rewards
	public void CompleteQuest(EventArgumentData ead)
	{
		if (active)
		{
			Debug.Log("completed quest");
			//questManager.CompleteQuest(this);
		}
	}
	#endregion
}
