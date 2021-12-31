using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DangoMimikyu.EventManagement;

public class PauseMenuHandler : MonoBehaviour
{
	[Header("Object references")]
	[SerializeField]
	private Canvas m_beatUICanvas;
	[SerializeField]
	private Canvas m_questEndCanvas;
	[SerializeField]
	private Canvas m_feedbackCanvas;
	[SerializeField]
	private Canvas m_pauseCanvas;

	private bool m_currentPauseState = false;

	#region Monobehaviour functions
	void Start()
	{
		m_pauseCanvas.gameObject.SetActive(false);
	}
	#endregion

	#region Event handling functions
	private void TogglePauseMenu(bool enable)
	{
		m_pauseCanvas.gameObject.SetActive(enable);
	}
	#endregion

	public void TogglePause()
	{
		m_currentPauseState = !m_currentPauseState;

		TogglePauseMenu(m_currentPauseState);
		EventManager.instance.DispatchEvent(GameEvents.Menu_Pause, m_currentPauseState);
	}
}
