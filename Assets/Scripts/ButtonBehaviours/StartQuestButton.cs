using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartQuestButton : MonoBehaviour
{
    #region Monobehaviour functions
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(StartQuest);
    }
    #endregion

    private void StartQuest()
    {
        SceneController sc = GameObject.FindGameObjectWithTag("SceneController").GetComponent<SceneController>();
        sc.LoadSceneString("ExpeditionScene");
    }
}
