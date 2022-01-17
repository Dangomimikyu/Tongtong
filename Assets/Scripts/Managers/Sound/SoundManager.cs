using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using DangoMimikyu.EventManagement;

public class SoundManager : MonoBehaviour
{
	[Header("UI sounds")]
	[SerializeField]
	private AudioClip m_menuButton;

	[Header("Music sounds")]
	[SerializeField]
	private AudioClip m_mainMenuBGM;
	[SerializeField]
	private AudioClip m_homeBaseBGM;
	[SerializeField]
	private AudioClip m_expeditionBGM;
	[SerializeField]
	private AudioClip m_winBGM;
	[SerializeField]
	private AudioClip m_loseBGM;

	[Header("UI sounds")]
	[SerializeField]
	private AudioClip m_menuSelection;

	[Header("Home base sounds")]
	[SerializeField]
	private AudioClip m_equipItem;
	[SerializeField]
	private AudioClip m_upgradeUnit;

	[Header("Expedition sounds")]
	[SerializeField]
	private AudioClip m_metronome;
	[SerializeField]
	private AudioClip m_feverGet;
	[SerializeField]
	private AudioClip m_feverLost;
	[SerializeField]
	private AudioClip m_walkDrum;
	[SerializeField]
	private AudioClip m_attackDrum;
	[SerializeField]
	private AudioClip m_defendDrum;
	[SerializeField]
	private AudioClip m_magicDrum;

	[Header("Audio sources")]
	[SerializeField]
	private AudioSource m_musicSource;
	[SerializeField]
	private AudioSource m_sfxSource;
	[SerializeField]
	private AudioSource m_UISource;

	~SoundManager()
	{
		EventManager.instance.StopListening(GameEvents.Gameplay_ComboFever, ComboSound);
		EventManager.instance.StopListening(GameEvents.Gameplay_BreakCombo, ComboSound);
		EventManager.instance.StopListening(GameEvents.Input_Drum, DrumSound);
		EventManager.instance.StartListening(GameEvents.Gameplay_MetronomeBeat, MetronomeSound);
	}

	#region Monobehaviour functions
	private void Start()
	{
		EventManager.instance.StartListening(GameEvents.Gameplay_ComboFever, ComboSound);
		EventManager.instance.StartListening(GameEvents.Gameplay_BreakCombo, ComboSound);
		EventManager.instance.StartListening(GameEvents.Input_Drum, DrumSound);
		EventManager.instance.StartListening(GameEvents.Gameplay_MetronomeBeat, MetronomeSound);
	}

	private void OnDisable()
	{
		EventManager.instance.StopListening(GameEvents.Gameplay_ComboFever, ComboSound);
		EventManager.instance.StopListening(GameEvents.Gameplay_BreakCombo, ComboSound);
		EventManager.instance.StopListening(GameEvents.Input_Drum, DrumSound);
		EventManager.instance.StartListening(GameEvents.Gameplay_MetronomeBeat, MetronomeSound);
	}
	#endregion

	#region Sound playing functions
	private void ComboSound(EventArgumentData ead)
	{
		var fever = ead.eventName == GameEvents.Gameplay_ComboFever ? true : false;
		if (fever)
		{
			Debug.Log("Playing combo fever sound");
			m_sfxSource.PlayOneShot(m_feverGet);
		}
		else
		{
			Debug.Log("Playing break combo sound");
			m_sfxSource.PlayOneShot(m_feverLost);
		}
	}

	private void MetronomeSound(EventArgumentData ead)
	{
		m_sfxSource.PlayOneShot(m_metronome);
	}

	private void DrumSound(EventArgumentData ead)
	{
		CommandAtrributes.Inputs input = (CommandAtrributes.Inputs)ead.eventParams[0];
		switch (input)
		{
			case CommandAtrributes.Inputs.Walk:
				m_sfxSource.PlayOneShot(m_walkDrum);
				break;
			case CommandAtrributes.Inputs.Attack:
				m_sfxSource.PlayOneShot(m_attackDrum);
				break;
			case CommandAtrributes.Inputs.Defend:
				m_sfxSource.PlayOneShot(m_defendDrum);
				break;
			case CommandAtrributes.Inputs.Magic:
				m_sfxSource.PlayOneShot(m_magicDrum);
				break;
		}
	}
    #endregion
}
