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
        playerLevel = GameObject.FindGameObjectWithTag("PlayerMangaer").GetComponent<AccountInformation>().GetPlayerLevel();

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
        Quest returnQuest = new Quest();

        // roll for special quest - more rewards
        if (Random.Range(0, 100) == 0)
        {
            returnQuest.special = true;
        }

        return returnQuest;
    }
}
