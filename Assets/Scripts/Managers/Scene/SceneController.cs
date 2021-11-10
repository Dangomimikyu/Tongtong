using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DangoMimikyu.EventManagement;

public class SceneController : MonoBehaviour
{
    #region Monobehaviour functions
    void Start()
    {
        //Listen for menu navigation

        // load the splash screen scene
    }

    void Update()
    {
        
    }
    #endregion

    #region Scene loading functions
    private void LoadSplashScene()
    {
        SceneManager.LoadScene("ExpeditionScene", LoadSceneMode.Additive);
    }

    private void LoadLoginScene(EventArgumentData ead)
    {

    }
    #endregion
}
