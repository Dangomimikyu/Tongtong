// note: this class doesn't need to listen for Misc_SceneChange because it's locally put on a gameobject on the scene rather than being in PersistentManagers
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DangoMimikyu.EventManagement;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

// macros
using cmdCommand = CommandAtrributes.Commands;
using cmdInput = CommandAtrributes.Inputs;
using cmdPotency = CommandAtrributes.Potency;

public class PlayerInputHandler : MonoBehaviour
{
	[Header("External object references")]
	[SerializeField]
	private PauseMenuHandler m_pauseMenuHandler;

	// player input
	private PlayerInputs m_playerInputAction;

	public enum MenuType
	{
		Pause = 1,
	}

	#region Monobehaviour functions
	private void Awake()
	{
		m_playerInputAction = new PlayerInputs();
	}

	private void Start()
	{
		m_playerInputAction.Expedition.Drum.performed += ParseCommandInput;
		m_playerInputAction.Expedition.PauseMenu.performed += ParseMenuInput;
		m_playerInputAction.Menus.PauseMenu.performed += ParseMenuInput;

		ChangeInputMap();
	}

	private void OnEnable()
	{
		m_playerInputAction.Enable();
	}

	private void OnDisable()
	{
		m_playerInputAction.Disable();
	}
	#endregion

	#region Input map functions
	private void ChangeInputMap()
    {
		string currentSceneName = SceneManager.GetActiveScene().name;
		Debug.Log("current scene: " + currentSceneName);

		if (currentSceneName == "ExpeditionScene")
        {
			m_playerInputAction.Menus.Disable();
			m_playerInputAction.Expedition.Enable();
        }
        else
        {
			m_playerInputAction.Expedition.Disable();
			m_playerInputAction.Menus.Enable();
        }
    }
	#endregion

	private void ParseCommandInput(InputAction.CallbackContext ctx)
	{
		Debug.Log("call call");
		cmdInput input = (cmdInput)ctx.ReadValue<float>();

		switch (input)
		{
			case cmdInput.Walk:
			case cmdInput.Attack:
			case cmdInput.Defend:
			case cmdInput.Magic:
				EventManager.instance.DispatchEvent(GameEvents.Input_Drum, input); // call the drum input event and let the beat tracker decide what to do
				break;
			default:
				Debug.LogError("Unexpected player input");
				break;
		}
	}

	private void ParseMenuInput(InputAction.CallbackContext ctx)
	{
		Debug.Log("pause menu");
		MenuType scaleVal = (MenuType)ctx.ReadValue<float>();

		switch (scaleVal)
		{
			case MenuType.Pause:
				// change the input map
				// [change input map]
				// change UI and inform game that it's being paused
				m_pauseMenuHandler?.TogglePause();
				break;
			default:
				Debug.Log("invalid menu change");
				break;
		}
	}
}
