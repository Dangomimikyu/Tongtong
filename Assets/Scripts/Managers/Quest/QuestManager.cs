using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance { private set; get; }
    private List<Quest> m_questList;

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
	}
	#endregion

	#region Quest creation functions
	private void CreateQuest()
	{
		Quest newQuest = new Quest();

	}
	#endregion
}
