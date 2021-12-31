using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBehaviour : MonoBehaviour
{
    [Header("Inspector debug variables")]
    [SerializeField]
    private float m_health;

	#region Value setting functions
    public void SetShieldHealth(float health)
	{
		m_health = health;
	}
	#endregion
}
