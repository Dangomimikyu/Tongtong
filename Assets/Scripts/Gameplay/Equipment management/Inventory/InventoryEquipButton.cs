using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryEquipButton : MonoBehaviour
{
    [Header("Object references")]
    [SerializeField]
    private TMP_Text weaponText;
    [SerializeField]
    private WeaponTemplate weaponInfo;

    private bool purchased = false;

	#region Monobehaviour functions
	private void Awake()
	{
	}

	void Start()
    {
        weaponText.text = "";
        UpdateTextInfo();
    }

    void Update()
    {

    }
	#endregion

    private void UpdateTextInfo()
	{
        string output = "Dmg: " + weaponInfo.bulletData.damage + "\n";

        if (weaponInfo.bulletData.burstBulletCount > 0)
		{
            output += "burst: yes\n";
            output += "shots: " + weaponInfo.bulletData.burstBulletCount;
		}
        else
		{
            output += "burst: no";
		}

        Debug.Log("output: " + output);

        weaponText.text = output;
	}
}
