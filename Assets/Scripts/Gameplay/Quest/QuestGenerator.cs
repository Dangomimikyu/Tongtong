using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct QuestDescriptions
{
	public string name;
	public string description;

	public QuestDescriptions(string n, string d)
	{
		name = n;
		description = d;
	}
}

public class QuestGenerator : MonoBehaviour
{
	private int playerLevel;
	private QuestDescriptions[] m_questDescriptions =
		{
			new QuestDescriptions("Enemy subjugation", "Defeat enemies and meet at the rendezvous point at the end."),
			new QuestDescriptions("Point retrieval", "Reach the beacon and recapture from enemies"),
		}; // due to time constraint, theres only one type of mission so this is actually just for flair

	public List<Quest> GenerateQuestList()
	{
		// number of quests available is determined by the player level
		List<Quest> questList = new List<Quest>();
		playerLevel = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<AccountInformation>().GetPlayerLevel();

		int numberOfQuests = Mathf.Min(playerLevel + 2, 5);

		for (int i = 0; i < numberOfQuests; ++i)
		{
			Quest q = GenerateQuest();
			questList.Add(q);
		}

		return questList;
	}

	private Quest GenerateQuest()
	{
		int moneyEarned = Random.Range(10, 100);
		int expEarned = Random.Range(10, 25);
		int difficulty = Random.Range(1, 5);
		bool spec = Random.Range(0, 100) == 0;

		if (spec)
		{
			moneyEarned *= 3;
			expEarned *= 3;
			difficulty = 5;
		}

		QuestDescriptions qdTemp = m_questDescriptions[Random.Range(0, m_questDescriptions.Length)];

		Quest returnQuest = new Quest(qdTemp.name, qdTemp.description, moneyEarned, expEarned, difficulty);
		returnQuest.PrintQuest();
		return returnQuest;
	}
}
