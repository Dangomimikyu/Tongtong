// note: this class is to manage the behaviour of any individual friendly unit, enemies will have their own behaviour script.
// units will be dependent on the ArmyManager class for the execution of commands because having 1 event listener that controls others makes more sense than having n event listeners all waiting at the same time.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

// macros
using cmdCommand = CommandAtrributes.Commands;
using cmdInput = CommandAtrributes.Inputs;
using cmdPotency = CommandAtrributes.Potency;
using wpnType = WeaponAttributes.WeaponType;

public class UnitBehaviour : MonoBehaviour
{
    [Header("Unit script references")]
    [SerializeField]
    public UnitData unitData = new UnitData();
    [SerializeField]
    private BoxCollider2D m_selfCollider;
    [SerializeField]
    private Image m_overheadUI;

    [Header("Unit stats")]
    [SerializeField]
    private HealthBarController m_healthBarController;
    private bool m_spawned = false;

    [Header("Prefab references")]
    [SerializeField]
    private GameObject m_shieldPrefab;

    [Header("Object references")]
    [SerializeField]
    private GameObject m_leftWeaponObject = null;
    [SerializeField]
    private GameObject m_rightWeaponObject = null;

    private UnitObjectSpawner m_unitObjSpawner;

	#region Monobehaviour functions
	void Start()
    {
        m_unitObjSpawner = GameObject.FindGameObjectWithTag("UnitManager").GetComponent<UnitObjectSpawner>();
        m_overheadUI.gameObject.SetActive(false);

        m_shieldPrefab = GameObject.FindGameObjectWithTag("WeaponAttributes").GetComponent<WeaponAttributes>().GetShieldPrefab(unitData.shieldData);

        TakeDamage(0); // init the health bar value to what it's supposed to be
    }
    #endregion

    #region Spawning functions
    public void Spawn()
	{
        if (!m_spawned)
		{
            m_spawned = true;
		}
	}
    #endregion

    #region Movement functions
    public void MoveForward(cmdPotency potency)
	{
        // choose a random point in front
        float rand = Random.Range(0.75f, 1.25f);
        // distance depends on the potency
        float moveDistance = (float)potency * rand + 0.5f; // range: 0.5 to 3
        //Debug.Log("multiplier: " + multiplier);
        transform.DOMoveX(transform.localPosition.x + moveDistance, 1.0f).SetEase(Ease.InOutSine);
	}

    public void Retreat(cmdPotency potency)
	{
        // choose a random point in front
        float rand = Random.Range(0.75f, 1.25f);
        // distance depends on the potency
        float moveDistance = (float)potency * rand + 0.5f; // range: 0.5 to 3
        //Debug.Log("multiplier: " + multiplier);
        transform.DOMoveX(transform.localPosition.x - moveDistance, 1.0f).SetEase(Ease.InOutSine);
    }
    #endregion

    #region Combat functions
    public void AttackStraight(cmdPotency potency)
	{
        m_leftWeaponObject.transform.DORotate(new Vector3(0.0f, 0.0f, 0.0f), 0.3f);

        if (!unitData.leftWeapon.twoHanded)
		{
            m_rightWeaponObject?.transform.DORotate(new Vector3(0.0f, 0.0f, 0.0f), 0.3f);
		}
        ShootBullets(potency, 0.0f);
    }

    public void AttackUpward(cmdPotency potency)
    {
        //need additional step of rotating the guns upward for 2s
        m_leftWeaponObject.transform.DORotate(new Vector3(0.0f, 0.0f, 30.0f), 0.3f);

        if (!unitData.leftWeapon.twoHanded)
		{
            m_rightWeaponObject?.transform.DORotate(new Vector3(0.0f, 0.0f, 30.0f), 0.3f);
		}
        ShootBullets(potency, 30.0f);
	}

    public void ShootBullets(cmdPotency potency, float angle)
	{
        m_unitObjSpawner.SpawnPlayerBullet(gameObject, unitData.leftWeapon, potency, angle);

        if (!unitData.leftWeapon.twoHanded)
		{
            m_unitObjSpawner.SpawnPlayerBullet(gameObject, unitData.rightWeapon, potency, angle);
		}
    }

    public void Defend(cmdPotency potency)
    {
        Debug.Log("defend called");

        GameObject newShield = Instantiate(m_shieldPrefab, transform);
        newShield.GetComponent<ShieldBehaviour>().m_health = unitData.shieldData.health;

        switch (potency)
		{
			case cmdPotency.Low:
                newShield.GetComponent<ShieldBehaviour>().SetLifetime(unitData.shieldData.lifetimeDur);
                break;
			case cmdPotency.Medium:
                newShield.GetComponent<ShieldBehaviour>().SetLifetime(unitData.shieldData.lifetimeDur * 1.25f);
                break;
			case cmdPotency.High:
                newShield.GetComponent<ShieldBehaviour>().SetLifetime(unitData.shieldData.lifetimeDur * 1.5f);
				break;
			default:
                Debug.LogError("unexpected potency level");
				break;
		}
	}
	#endregion

	#region Preparation functions
	public void Focus(cmdPotency potency)
    {
	}

    public void Pray(cmdPotency potency)
	{

	}
    #endregion

    #region Health functions
    public void TakeDamage(float dmg)
    {
        unitData.currentHealth -= dmg;
        m_healthBarController.UpdateHealth(unitData.currentHealth);
    }

    public void SetMaxHealth(float health)
	{
        m_healthBarController.SetMaxHealth(health);
	}

    public void SetCurrentHealth(float currHealth)
    {
        unitData.currentHealth = currHealth;
    }

    public void ToggleHealthUI(bool enable)
	{
        m_healthBarController.gameObject.SetActive(enable);
	}
	#endregion

	#region UI functions
    public void UpdateHeadUI(Sprite newSprite)
	{
        if (m_overheadUI == null)
            return;

        if (newSprite == null)
		{
            m_overheadUI.gameObject?.SetActive(false);
		}
		else
		{
            m_overheadUI.sprite = newSprite;
            m_overheadUI.gameObject?.SetActive(true);
		}
	}
	#endregion

	#region Weapon functions
    public void AddWeapons(bool left, GameObject wpn)
	{
        if (left)
		{
            m_leftWeaponObject = wpn;
		}
		else
		{
            m_rightWeaponObject = wpn;
		}
	}
	#endregion
}
