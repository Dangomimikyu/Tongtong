using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// macros
using cmdPotency = CommandAtrributes.Potency;

public class ShieldBehaviour : MonoBehaviour
{
    [Header("Inspector debug variables")]
    public float m_health;

	#region Value setting functions
	public void SetLifetime(float duration)
	{
		Destroy(gameObject, duration);
	}


	public void TakeDamage(float dmg)
	{
		Debug.Log("shield take damage");
		m_health -= dmg;
		if (m_health <= 0.0f)
		{
			Debug.Log("shield destroyed");
			Destroy(gameObject);
		}
	}
	#endregion
}
