using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraBehaviour : MonoBehaviour
{
    #region Monobehaviour functions
    void Start()
    {
        CinemachineVirtualCamera vc = Camera.main.GetComponent<CinemachineVirtualCamera>();
        UnitDataManager dataManager = GameObject.FindGameObjectWithTag("UnitManager").GetComponent<UnitDataManager>();
		vc.Follow = dataManager.activeUnits[0].gameObject.transform;
	}
    #endregion
}
