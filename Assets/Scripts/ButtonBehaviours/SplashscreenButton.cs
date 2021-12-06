using UnityEngine;

public class SplashscreenButton : MonoBehaviour
{
    private SceneController m_sceneController;

	#region Monobehaviour functions
	void Start()
    {
        m_sceneController = GameObject.FindGameObjectWithTag("SceneController").GetComponent<SceneController>();
    }
	#endregion

	#region Scene changing functions
	public void StartGame()
	{
        m_sceneController.LoadSceneString("MainMenuScene");
	}

    public void QuitGame()
	{
        Application.Quit();
	}
	#endregion
}
