using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestSelectionButtonBehaviour : MonoBehaviour
{
	public int questSelectionIndex;
	private QuestSelectionUIHandler m_selectionUIhandler;
	[SerializeField]
	private TMP_Text m_nameTextBox;

	#region Monobehaviour functions
	private void Start()
	{
		m_selectionUIhandler = GameObject.FindGameObjectWithTag("QuestSelectionHandler").GetComponent<QuestSelectionUIHandler>();
	}
	#endregion

	#region Display quest information functions
	public void ChangeQuestDisplayName(string name)
	{
		m_nameTextBox.text = name;
	}

	public void DisplayInformation()
	{
		m_selectionUIhandler.ChangeQuestInfoUI(questSelectionIndex);
	}
	#endregion
}
