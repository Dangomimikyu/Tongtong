using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DangoMimikyu.EventManagement;
using System;

public struct QuestRewards
{
	public uint money;
	public int exp;

	#region Operation overloads
	public static QuestRewards operator +(QuestRewards self, QuestRewards other)
	{
		self.money += other.money;
		self.exp += other.exp;
		return self;
	}

	public static QuestRewards operator -(QuestRewards self, QuestRewards other)
	{
		self.money -= other.money;
		self.exp -= other.exp;
		return self;
	}
	#endregion
}

public class Quest
{
	public string questName { private set; get; }
	public QuestRewards questRewards;
	public string questDescription;
	public QuestManager questManager;
	public bool active = false;
	public bool special = false; // reserved for quests that randomly have super good rewards

	#region Constructor
	public Quest()
	{
		questName = "defaultQuestName";
		questDescription = "This quest has no description";
		questRewards.money = 1;
		questRewards.exp = 5;
	}

	public Quest(string name = "defaultQuestName", string description = "defaultDescription", uint moneyReward = 1, int expReward = 5)
	{
		questName = name;
		questDescription = description;
		questRewards.money = moneyReward;
		questRewards.exp = expReward;
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
