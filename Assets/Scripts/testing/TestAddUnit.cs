using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DangoMimikyu.EventManagement;

public class TestAddUnit : MonoBehaviour
{
    void Start()
    {
		List<UnitBehaviour> ublist = new List<UnitBehaviour>();
		for (int i = 0; i < 3; ++i)
		{
			UnitBehaviour tempub = new UnitBehaviour();
			ublist.Add(tempub);
		}
		EventManager.instance.DispatchEvent(GameEvents.Gameplay_UpdateUnits, ublist);
	}

    void Update()
    {

    }
}
