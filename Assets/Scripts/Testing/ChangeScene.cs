using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void NewScene(InputAction.CallbackContext ctx)
	{
        float value = ctx.ReadValue<float>();
        Debug.Log("test:");
	}
}
