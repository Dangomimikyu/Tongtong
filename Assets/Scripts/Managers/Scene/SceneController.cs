using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using DangoMimikyu.EventManagement;

public class SceneController : MonoBehaviour
{
	private PlayerInputs m_playerInputAction;

	#region Monobehaviour functions
	private void Awake()
	{
		m_playerInputAction = new PlayerInputs();
		m_playerInputAction.Menus.changescenetest.performed += ChangeScene;
		m_playerInputAction.Menus.changescenenext.performed += LoadSceneNext;
	}

	private void Start()
	{
		//UnloadAllScenes();
		LoadSceneString("LoginScene");
	}

	private void OnEnable()
	{
		m_playerInputAction.Enable();
	}

	private void OnDisable()
	{
		m_playerInputAction.Disable();
	}

	void Update()
	{

	}
	#endregion

	#region Coroutines
	private IEnumerator LoadNewScene()
	{
		yield return null;
		EventManager.instance.DispatchEvent(GameEvents.Misc_SceneChange, SceneManager.GetActiveScene().name);
		yield break;
	}
	#endregion

	#region Scene loading functions
	private void UnloadAllScenes()
	{
		int numScenes = SceneManager.sceneCount;
		for (int i = 0; i < numScenes; ++i)
		{
			Scene scene = SceneManager.GetSceneAt(i);
			Debug.Log("Scene name: " + scene.name);
			if (scene.name != "PersistentManagers") // don't unload the PersistentManagers scene
			{
				SceneManager.UnloadSceneAsync(scene);
			}
		}
	}

	private void LoadSplashScene()
	{
		SceneManager.LoadScene("ExpeditionScene", LoadSceneMode.Additive);
	}

	private void LoadLoginScene(EventArgumentData ead)
	{

	}

	public void LoadSceneString(string name)
	{
		// load scene by name
		UnloadAllScenes();
		SceneManager.LoadScene(name);
		StartCoroutine(LoadNewScene());
	}

	private void LoadSceneNext(InputAction.CallbackContext ctx)
	{
		// load next scene according to build settings
		int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
		SceneManager.LoadScene(nextSceneIndex);
		StartCoroutine(LoadNewScene());
	}

	private void ChangeScene(InputAction.CallbackContext ctx)
	{
		float value = ctx.ReadValue<float>();
		Debug.Log("test: " + value);
	}
	#endregion
}
