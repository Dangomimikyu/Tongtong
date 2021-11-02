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

public class InputManager : MonoBehaviour
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
		m_playerInputAction.Expedition.Drum.performed += ParseInput;
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

	private void test(InputAction.CallbackContext c)
	{
		Debug.Log("BURUH");
		var f = c.ReadValue<float>();
		Debug.Log("f: " + f);
		//if (f == 1.0f)
		//{
		//	Debug.Log("1");
		//}
		//else if (f == 2.0f)
		//{
		//	Debug.Log("2");
		//}
		//else if (f == 3.0f)
		//{
		//	Debug.Log("3");
		//}
		//else if (f == 4.0f)
		//{
		//	Debug.Log("4");
		//}
		//else
		//{
		//	Debug.Log("unexpected input from context");
		//}
		Debug.Log("test called");
	}

	private void ParseInput(InputAction.CallbackContext ctx)
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


		//if (input == 1.0f)
		//{
		//	Debug.Log("left drum");
		//}
		//else if (input == 2.0f)
		//{
		//	Debug.Log("right drum");
		//}
		//else if (input == 3.0f)
		//{
		//	Debug.Log("up drum");
		//}
		//else if (input == 4.0f)
		//{
		//	Debug.Log("down drum");
		//}
		//else
		//{
		//	Debug.Log("unexpected input inputrom context");
		//}
	}
}
