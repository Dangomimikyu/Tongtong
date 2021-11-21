using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DangoMimikyu.EventManagement;

public class CameraBehaviour : MonoBehaviour
{
    #region Monobehaviour functions
    void Start()
    {
        EventManager.instance.StartListening(GameEvents.Unit_Spawn, ChangeFollowTarget);
	}
    #endregion

    #region Following functions
    private void ChangeFollowTarget(EventArgumentData ead)
	{
        CinemachineVirtualCamera vc = Camera.main.GetComponent<CinemachineVirtualCamera>();
        UnitDataManager dataManager = GameObject.FindGameObjectWithTag("UnitManager").GetComponent<UnitDataManager>();
        vc.Follow = dataManager.activeUnits[0].gameObject.transform;
    }
    #endregion
}
