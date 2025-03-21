using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestSelectionUIHandler : MonoBehaviour
{
	[Header("External script references")]
	[SerializeField]
	private QuestManager m_questManager;

	[Header("Object references")]
	[SerializeField]
	private GameObject m_selectionUIPrefab;
	[SerializeField]
	private TMP_Text m_questNameOutputBox;
	[SerializeField]
	private TMP_Text m_descriptionOutputBox;
	[SerializeField]
	private GameObject m_questLayoutGroup;

	private int m_questNumberCount = 0;
	private int m_currentSelectedIndex = -1;
	private Quest m_currentSelectedQuest = null;

	#region Monobehaviour functions
	private void Start()
	{
		m_questNameOutputBox.text = "";
		m_descriptionOutputBox.text = "";
		Debug.Log("lesgo baby");

		m_questManager = GameObject.FindGameObjectWithTag("QuestManager").GetComponent<QuestManager>();
		m_questManager.m_questSelectionUIHandler = this;
		m_questManager.AddQuestListToUI();
	}
	#endregion

	#region Quest info functions
	public void AddQuestEntry(Quest q)
	{
		GameObject newQuestButton = Instantiate(m_selectionUIPrefab, m_questLayoutGroup.transform);
		newQuestButton.GetComponent<QuestSelectionButtonBehaviour>().questSelectionIndex = m_questNumberCount;
		newQuestButton.GetComponent<QuestSelectionButtonBehaviour>().ChangeQuestDisplayName(q.questName);
		++m_questNumberCount;
	}

	public void ChangeQuestInfoUI(int index)
	{
		Quest selection = m_questManager.GetQuestFromIndex(index);
		m_currentSelectedQuest = selection;
		m_currentSelectedIndex = index;

		m_questNameOutputBox.text = selection.questName;
		string description = selection.questDescription + "\n\n";
		description += "money: " + selection.questRewards.money + "\n";
		description += "exp: " + selection.questRewards.exp + "\n";
		m_descriptionOutputBox.text = description;
	}

	public void StartQuest()
	{
		if (m_currentSelectedQuest != null)
		{
			m_questManager.StartQuest();
			SceneController sc = GameObject.FindGameObjectWithTag("SceneController").GetComponent<SceneController>();
			sc.LoadSceneString("ExpeditionScene");
		}
	}
	#endregion

	#region Value getter functions
	public int GetSelectionIndex()
	{
		return m_currentSelectedIndex;
	}
	#endregion
}
