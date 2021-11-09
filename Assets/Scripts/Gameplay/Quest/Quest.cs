using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct QuestRewards
{
	uint money;
	//CraftingMaterial[] materials;

}

public class Quest : MonoBehaviour
{
	private string m_ownerAccountName = "";
	public string questName { private set; get; }
	public QuestRewards questRewards;

	#region Monobehaviour functions
	private void Start()
	{

	}
	#endregion
}
