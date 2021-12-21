// overseeing of the all the unit's UI, but individual unit's UI is controlled by it's own UnitBehaviour
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DangoMimikyu.EventManagement;

public class UnitExpeditionUIManager : MonoBehaviour
{
    [SerializeField]
    private List<UnitBehaviour> m_activeUnits;


    ~UnitExpeditionUIManager()
	{

	}

	#region Monobehaviour functions
	void Start()
    {

    }

    void Update()
    {

    }
    #endregion

    #region Event response functions
    private void UnitSpawned(EventArgumentData ead)
	{

	}

    private void UnitDied(EventArgumentData ead)
	{

	}
    #endregion
}
