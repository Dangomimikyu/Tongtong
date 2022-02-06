// note: the player settings work by updating player pref when a specific setting is updated and the value of that setting will be taken from the associated UI element.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class PlayerSettings : MonoBehaviour
{
	[Header("External object references")]
	[SerializeField]
	private AudioMixer m_musicMixer;
	[SerializeField]
	private AudioMixer m_sfxMixer;
	[SerializeField]
	private AudioMixer m_uiMixer;

	[Header("UI object references")]
	[SerializeField]
	private Slider m_musicVolSlider;
	[SerializeField]
	private Slider m_sfxVolSlider;
	[SerializeField]
	private Slider m_uiVolSlider;
	[SerializeField]
	private Toggle m_fullscreenToggle;
	[SerializeField]
	private Toggle m_delayEnabled;
	[SerializeField]
	private TMP_Dropdown m_resolutionDropdown;
	[SerializeField]
	private TMP_Dropdown m_qualityDropdown;

	private Resolution[] m_localResolutions;

	#region Monobehaviour functions
	private void Start()
	{
		//InitResolutionList();
		InitFullscreen();
		InitSliders();
		InitQuality();
		//InitResolution();
	}
	#endregion

	#region Init functions
	private void InitResolutionList()
	{
		m_localResolutions = Screen.resolutions;
		m_resolutionDropdown.ClearOptions();
		List<string> resOptions = new List<string>();

		int tempResolutionIndex = 0;
		//foreach (Resolution r in m_localResolutions)
		for (int i = 0; i < m_localResolutions.Length; ++i)
		{
			string op = m_localResolutions[i].width + " x " + m_localResolutions[i].height;
			resOptions.Add(op);

			if (m_localResolutions[i].width == Screen.currentResolution.width && m_localResolutions[i].height == Screen.currentResolution.height)
			{
				tempResolutionIndex = i;
			}
		}

		m_resolutionDropdown.AddOptions(resOptions);
		m_resolutionDropdown.value = tempResolutionIndex;
		m_resolutionDropdown.RefreshShownValue();
	}

	private void InitSliders()
	{
		m_musicVolSlider.value = PlayerPrefs.GetFloat("MusicVol", 0.5f);
		m_sfxVolSlider.value = PlayerPrefs.GetFloat("SFXVol", 0.5f);
		m_uiVolSlider.value = PlayerPrefs.GetFloat("UIVol", 0.5f);
	}

	private void InitFullscreen()
	{
		//Screen.fullScreen = PlayerPrefs.GetInt("Fullscreen", 0) == 1;
		Screen.fullScreen = true; // removing resolutions and fullscreen in order to finish polishing faster
	}

	private void InitResolution()
	{
		int width = PlayerPrefs.GetInt("ResolutionWidth", 1280);
		int height = PlayerPrefs.GetInt("ResolutionHeight", 720);
		Screen.SetResolution(width, height, Screen.fullScreen);
	}

	private void InitQuality()
	{
		QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Quality"));
	}
	#endregion

	#region Specific option setters
	public void SetMusicVol(float vol)
	{
		Debug.Log("setting music volume to: " + vol);
		PlayerPrefs.SetFloat("MusicVol", vol);

		m_musicMixer.SetFloat("MusicVolume", Mathf.Log10(vol) * 20);
	}

	public void SetSfxVol(float vol)
	{
		Debug.Log("setting SFX volume to: " + vol);
		PlayerPrefs.SetFloat("SFXVol", vol);

		m_sfxMixer.SetFloat("SfxVolume", Mathf.Log10(vol) * 20);
	}

	public void SetUiVol(float vol)
	{
		Debug.Log("setting UI volume to: " + vol);
		PlayerPrefs.SetFloat("UIVol", vol);

		m_uiMixer.SetFloat("UiVolume", Mathf.Log10(vol) * 20);
	}

	public void SetFullscreen(bool fs)
	{
		Debug.Log("setting fullscreen to: " + fs);
		PlayerPrefs.SetInt("Fullscreen", fs ? 1 : 0);

		Screen.fullScreen = fs;
	}

	public void SetResolution(int resIndex)
	{
		Resolution newResolution = m_localResolutions[resIndex];
		PlayerPrefs.SetInt("ResolutionWidth", newResolution.width);
		PlayerPrefs.SetInt("ResolutionHeight", newResolution.height);
		Debug.Log("width: " + PlayerPrefs.GetInt("ResolutionWidth", 1280) + " height: " + PlayerPrefs.GetInt("ResolutionHeight", 720));
		Screen.SetResolution(newResolution.width, newResolution.height, Screen.fullScreen);
	}

	public void SetQuality(int qualityIndex)
	{
		Debug.Log("changing quality level to: " + qualityIndex);
		PlayerPrefs.SetInt("Quality", qualityIndex);
		QualitySettings.SetQualityLevel(qualityIndex);
	}

	public void SetBeatDelay(bool bd)
	{
		Debug.Log("setting beat delay to: " + bd);
		PlayerPrefs.SetInt("BeatDelay", bd ? 1 : 0);
	}
	#endregion
}
