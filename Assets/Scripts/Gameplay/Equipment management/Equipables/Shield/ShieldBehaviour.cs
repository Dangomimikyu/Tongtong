using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// macros
using cmdPotency = CommandAtrributes.Potency;

public class ShieldBehaviour : MonoBehaviour
{
    [Header("Inspector debug variables")]
    [SerializeField]
    private float m_health;

	#region Value setting functions
	private void SetPotency(cmdPotency pot)
    {

    }

    private void SetShieldHealth(float health)
	{
		m_health = health;
	}
	#endregion
}
