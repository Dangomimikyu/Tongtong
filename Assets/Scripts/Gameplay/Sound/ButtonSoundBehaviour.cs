using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSoundBehaviour : MonoBehaviour
{
	private SoundManager m_soundManager;
	private Button m_selfButton;

	#region Monobehaviour functions
	private void Start()
	{
		m_soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
		m_selfButton = GetComponent<Button>();
		m_selfButton.onClick.AddListener(PlayButtonClickSound);
	}
	#endregion

	private void PlayButtonClickSound()
	{
		m_soundManager.UIButtonSound();
	}
}
