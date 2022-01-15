// note: the player settings work by updating player pref when a specific setting is updated and the value of that setting will be taken from the associated UI element.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerSettings : MonoBehaviour
{
    [Header("External object references")]
    [SerializeField]
    private Slider m_musicVolSlider;
    //[SerializeField]
    //private TMP_Text m_musicVolValue;
    [SerializeField]
    private Slider m_sfxVolSlider;
    //[SerializeField]
    //private TMP_Text m_sfxVolValue;
    [SerializeField]
    private Slider m_uiVolSlider;
    [SerializeField]
    private Toggle m_delayEnabled;


    public enum SettingOptions
    {
        Sfx,
        Music,
        BeatDelay,
        Resolution,
    }

    public void SetSetting(SettingOptions option)
    {
        switch (option)
        {
            case SettingOptions.Sfx:
                break;
            case SettingOptions.Music:
                break;
            case SettingOptions.BeatDelay:
                //m_delayEnabled = m_delayDropdown.options[m_delayDropdown.value] == m_delayDropdown.options[0] ? true : false;
                int delayEnable = m_delayEnabled.enabled ? 1 : 0;
                PlayerPrefs.SetInt("BeatDelay", delayEnable);
                break;
            case SettingOptions.Resolution:
                break;
            default:
                Debug.LogError("unexpected settings input");
                break;
        }
    }

    #region Specific option setters
    public void SetMusicVol(float vol)
	{

	}
    #endregion
}
