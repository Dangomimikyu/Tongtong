using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSettings : MonoBehaviour
{
    [Header("External object references")]
    [SerializeField]
    private float musicVol;

    public enum SettingOptions
    {
        Sfx,
        Music,
        BeatDelay,
        Resolution,
    }

    public void SetSetting(SettingOptions option)
    {

    }

    #region Specific option setters
    //private void 
    #endregion
}
