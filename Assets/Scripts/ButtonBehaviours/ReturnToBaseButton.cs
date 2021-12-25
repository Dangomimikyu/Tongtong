using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReturnToBaseButton : MonoBehaviour
{
    #region Monobehaviour functions
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(ReturnToBase);
    }
	#endregion

	private void ReturnToBase()
	{
        SceneController sc = GameObject.FindGameObjectWithTag("SceneController").GetComponent<SceneController>();
        sc.LoadSceneString("HomeBaseScene");
	}
}
