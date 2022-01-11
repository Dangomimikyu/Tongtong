using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DangoMimikyu.EventManagement;
using DG.Tweening;

public class UnitUIController : MonoBehaviour
{
	~UnitUIController()
	{

	}

	#region Monobehaviour functions
	private void Start()
	{

	}

	private void OnDisable()
	{

	}
	#endregion

	private void ModifyHealthBar(EventArgumentData ead)
	{
		UnitBehaviour ub = (UnitBehaviour)ead.eventParams[0];
		float newHealth = ub.unitData.currentHealth;
	}
}
