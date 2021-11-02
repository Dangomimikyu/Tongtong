using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DangoMimikyu.EventManagement;
using UnityEngine.InputSystem;

// macros
using cmdCommand = CommandAtrributes.Commands;
using cmdInput = CommandAtrributes.Inputs;
using cmdPotency = CommandAtrributes.Potency;

public class PlayerInputHandler : MonoBehaviour
{
	[SerializeField]
	private PlayerInputs m_playerInputAction;

	#region Monobehaviour functions
	private void Awake()
	{
		m_playerInputAction = new PlayerInputs();
	}

	private void Start()
	{
		m_playerInputAction.Expedition.Drum.performed += ParseCommandInput;
	}
	private void OnEnable()
	{
		m_playerInputAction.Enable();
	}

	private void OnDisable()
	{
		m_playerInputAction.Disable();
	}

	private void Update()
	{
		//var input =
	}
	#endregion

	private void ParseCommandInput(InputAction.CallbackContext ctx)
	{
		cmdInput input = (cmdInput)ctx.ReadValue<float>();

		switch (input)
		{
			case cmdInput.None:
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

	}
}
