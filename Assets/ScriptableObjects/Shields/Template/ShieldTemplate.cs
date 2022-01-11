using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShieldTemplate", menuName = "Scriptable Shield")]
public class ShieldTemplate : ScriptableObject
{
	[Tooltip("Shield Prefab")]
	public GameObject shieldPrefab;
	[Tooltip("Shield Type")]
	public WeaponAttributes.ShieldType shieldType;
	[Tooltip("Shield information")]
	public WeaponAttributes.ShieldData shieldData;
	[Tooltip("Shield colour")]
	public Color shieldColour;
}
