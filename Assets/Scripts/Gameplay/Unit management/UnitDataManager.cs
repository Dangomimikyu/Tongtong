using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitData
{
	public float health = 10.0f;
	public Weapon leftWeapon;
	public Weapon rightWeapon;
}

public class UnitDataManager : MonoBehaviour
{
	[Header("Object references")]
	[SerializeField]
	private GameObject m_unitPrefab;
	private UnitSpawner spawner;

	// Start is called before the first frame update
	void Start()
	{
		spawner = GetComponent<UnitSpawner>();
	}

	// Update is called once per frame
	void Update()
	{

	}
}
