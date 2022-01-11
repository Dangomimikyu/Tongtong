using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DangoMimikyu.EventManagement;

public class PauseMenuHandler : MonoBehaviour
{
	[Header("Object references")]
	[SerializeField]
	private Canvas m_permenantPauseUI;
	[SerializeField]
	private Canvas m_mainPauseCanvas;
	[SerializeField]
	private Canvas m_commandCanvas;

	private bool m_currentPauseState = false;

	#region Monobehaviour functions
	void Start()
	{
		m_permenantPauseUI.gameObject.SetActive(false);
		m_mainPauseCanvas.gameObject.SetActive(false);
		m_commandCanvas.gameObject.SetActive(false);
	}
	#endregion

	#region Event handling functions
	public void TogglePause()
	{
		m_currentPauseState = !m_currentPauseState;

		ToggleMainPauseMenu(m_currentPauseState);
		EventManager.instance.DispatchEvent(GameEvents.Menu_Pause, m_currentPauseState);
	}
	#endregion

	#region UI enabling functions
	private void ToggleMainPauseMenu(bool enable)
	{
		m_permenantPauseUI.gameObject.SetActive(enable);
		m_mainPauseCanvas.gameObject.SetActive(enable);
		m_commandCanvas.gameObject.SetActive(false);
	}

	private void ToggleCommandUI()
	{
		if (m_commandCanvas.gameObject.activeInHierarchy)
		{
			m_commandCanvas.gameObject.SetActive(false);
			m_mainPauseCanvas.gameObject.SetActive(true);
		}
		else
		{
			m_commandCanvas.gameObject.SetActive(true);
			m_mainPauseCanvas.gameObject.SetActive(false);
		}
	}
	#endregion

	#region Button Functions
	public void ToggleCmdMenu()
	{
		// called by the "Command list" button in pause menu
		ToggleCommandUI();
	}

	public void AbandonQuest()
	{
		// called by the "Abandon quest" button in pause menu
		Button btn;

	}
	#endregion
}
