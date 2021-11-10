using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct QuestRewards
{
	public uint money;
	//CraftingMaterial[] materials;

}

public class Quest : MonoBehaviour
{
	private string m_ownerAccountName = "";
	public string questName { private set; get; }
	public QuestRewards questRewards;

	#region Constructor
	Quest()
	{
		questName = "defaultQuestName";
		questRewards.money = 1;
	}
	#endregion

	#region Monobehaviour functions
	private void Start()
	{

	}
	#endregion

	#region Completion rewards
	public void CompleteQuest()
	{

	}
	#endregion
}
