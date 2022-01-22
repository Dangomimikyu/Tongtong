using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGenerator : MonoBehaviour
{
    private int playerLevel;

    public List<Quest> GenerateQuestList()
    {
        // number of quests available is determined by the player level
        List<Quest> questList = new List<Quest>();
        playerLevel = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<AccountInformation>().GetPlayerLevel();

        int numberOfQuests = Mathf.Min(playerLevel, 10);

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

        Quest returnQuest = new Quest("testName", "testDescription", moneyEarned, expEarned, difficulty);
        returnQuest.PrintQuest();
        return returnQuest;
    }
}
