// note: the player settings work by notifying when a specific setting is updated and the value of that setting will be taken from the associated UI element.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSettings : MonoBehaviour
{
    [Header("External object references")]
    [SerializeField]
    private Slider m_musicVolSlider;
    [SerializeField]
    private int m_musicVolValue;
    [SerializeField]
    private Slider m_sfxVolSlider;
    [SerializeField]
    private int m_sfxVolValue;
    [SerializeField]
    private Dropdown m_delayDropdown;
    [SerializeField]
    private bool m_delayEnabled;


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
                m_delayEnabled = m_delayDropdown.options[m_delayDropdown.value] == m_delayDropdown.options[0] ? true : false;
                break;
            case SettingOptions.Resolution:
                break;
            default:
                Debug.LogError("unexpected settings input");
                break;
        }
    }

    #region Specific option setters
    //private void 
    #endregion
}
