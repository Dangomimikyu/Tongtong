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

	[SerializeField]
	private AudioSource m_source;

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

		//m_source = GetComponent<AudioSource>();
	}

	private void OnDisable()
	{
		EventManager.instance.StopListening(GameEvents.Gameplay_ComboFever, ComboSound);
		EventManager.instance.StopListening(GameEvents.Gameplay_BreakCombo, ComboSound);
		EventManager.instance.StopListening(GameEvents.Input_Drum, DrumSound);
		EventManager.instance.StartListening(GameEvents.Gameplay_MetronomeBeat, MetronomeSound);
	}
	#endregion

	#region Metronome and Drum functions
	private void ComboSound(EventArgumentData ead)
	{
		var fever = ead.eventName == GameEvents.Gameplay_ComboFever ? true : false;
		if (fever)
		{
			Debug.Log("Playing combo fever sound");
		}
		else
		{
			Debug.Log("Playing break combo sound");
		}
	}

	private void MetronomeSound(EventArgumentData ead)
	{
		//if (m_source == null)
		//	Debug.Log("bruh wtfh");
		m_source.PlayOneShot(m_metronome);
		//GetComponent<AudioSource>().PlayOneShot(m_metronome);
	}

	private void DrumSound(EventArgumentData ead)
	{
		CommandAtrributes.Inputs input = (CommandAtrributes.Inputs)ead.eventParams[0];
		switch (input)
		{
			case CommandAtrributes.Inputs.Walk:
				m_source.PlayOneShot(m_walkDrum);
				break;
			case CommandAtrributes.Inputs.Attack:
				m_source.PlayOneShot(m_attackDrum);
				break;
			case CommandAtrributes.Inputs.Defend:
				m_source.PlayOneShot(m_defendDrum);
				break;
			case CommandAtrributes.Inputs.Magic:
				m_source.PlayOneShot(m_magicDrum);
				break;
		}
	}
    #endregion
}
