using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthBarController : MonoBehaviour
{
	[SerializeField]
	private Slider m_healthSlider;

	~HealthBarController()
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

	#region updating functions
	public void SetMaxHealth(float maxHealth)
	{
		m_healthSlider.maxValue = maxHealth;
		m_healthSlider.value = maxHealth;
	}

	public void UpdateHealth(float newHealth)
	{
		//m_healthSlider.value = newHealth;
		m_healthSlider.DOValue(newHealth, 0.3f, false);
	}
	#endregion
}
