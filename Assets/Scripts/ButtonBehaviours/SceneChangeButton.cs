// this script is for buttons that change between scenes
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneChangeButton : MonoBehaviour
{
    public void StartQuest()
    {
        SceneController sc = GameObject.FindGameObjectWithTag("SceneController").GetComponent<SceneController>();
        sc.LoadSceneString("ExpeditionScene");
    }

    public void EndQuest()
    {
        SceneController sc = GameObject.FindGameObjectWithTag("SceneController").GetComponent<SceneController>();
        sc.LoadSceneString("HomeBaseScene");
    }
}
