using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DangoMimikyu.EventManagement;
using System;

public struct QuestRewards
{
	public int money;
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
	public int questDifficulty = 1;

	#region Constructor
	public Quest()
	{
		questName = "defaultQuestName";
		questDescription = "This quest has no description";
		questDifficulty = 1;
		questRewards.money = 1;
		questRewards.exp = 5;
	}

	public Quest(string name = "defaultQuestName", string description = "defaultDescription", int moneyReward = 1, int expReward = 5, int difficulty = 1)
	{
		questName = name;
		questDescription = description;
		questDifficulty = difficulty;
		questRewards.money = moneyReward;
		questRewards.exp = expReward;
	}

	~Quest()
	{
	}
	#endregion

	#region test functions
	public void PrintQuest()
	{
		Debug.Log("name: " + questName + " desc: " + questDescription + " mon: " + questRewards.money + "exp: " + questRewards.exp + " diff: " + questDifficulty);
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
