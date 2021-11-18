using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGoal : MonoBehaviour
{
	#region Monobehaviour functions
	void Start()
    {
    }

    void Update()
    {

    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// end the current quest by dispatching event
		Debug.Log("finished level");
	}
	#endregion
}
