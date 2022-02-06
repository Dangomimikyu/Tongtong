using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DangoMimikyu.EventManagement;
using TMPro;
using DG.Tweening;

public class CommandOutputUIHandler : MonoBehaviour
{
	[Header("Object references")]
	[SerializeField]
	private TMP_Text m_outputBox;

	#region Monobehaviour functions
	private void Start()
	{
		m_outputBox.text = "";

		EventManager.instance.StartListening(GameEvents.Gameplay_BreakCombo, BrokeCombo);
		EventManager.instance.StartListening(GameEvents.Input_CommandSuccess, CommandSuccess);
	}
	#endregion

	#region Coroutines
	private IEnumerator ClearTextAfterSeconds(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		m_outputBox.text = "";

		yield break;
	}
	#endregion

	public void ChangeOutputText(string newText)
	{
		m_outputBox.text = newText;
		StartCoroutine(ClearTextAfterSeconds(1.0f));
	}

	#region Event handling functions
	private void CommandSuccess(EventArgumentData ead)
	{
		CommandAtrributes.Commands cmd = (CommandAtrributes.Commands)ead.eventParams[0];
		string outputString = cmd.ToString();
		if (cmd == CommandAtrributes.Commands.AttackStraight)
		{
			outputString = "Attack straight";
		}
		else if (cmd == CommandAtrributes.Commands.AttackUpward)
		{
			outputString = "Attack upwards";
		}
		ChangeOutputText(outputString + "!");
	}

	private void BrokeCombo(EventArgumentData ead)
	{
		ChangeOutputText("combo broken");
	}
	#endregion
}
