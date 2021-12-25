// note: this class is to manage the behaviour of any individual friendly unit, enemies will have their own behaviour script.
// units will be dependent on the ArmyManager class for the execution of commands because having 1 event listener that controls others makes more sense than having n event listeners all waiting at the same time.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DangoMimikyu.EventManagement;
using DG.Tweening;

// macros
using cmdCommand = CommandAtrributes.Commands;
using cmdInput = CommandAtrributes.Inputs;
using cmdPotency = CommandAtrributes.Potency;
using wpnType = WeaponAttributes.WeaponType;

public class UnitBehaviour : MonoBehaviour
{
    [Header("Unit stats")]
    [SerializeField]
    public UnitData unitData = new UnitData();
    [SerializeField]
    private BoxCollider2D m_selfCollider;
    [SerializeField]
    private HealthBarController m_healthBarController;
    // private Enemy m_targettedEnemy
    private bool m_spawned = false;

    private UnitObjectSpawner m_unitObjSpawner;

	#region Monobehaviour functions
	void Start()
    {
        m_unitObjSpawner = GameObject.FindGameObjectWithTag("UnitManager").GetComponent<UnitObjectSpawner>();
    }

    void Update()
    {

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
    public void Attack(cmdPotency potency, cmdCommand cmd)
	{
        //Transform firingPoint = unitData.leftWeapon.firingPoint;
        //Debug.Log("firing point: " + firingPoint.transform.position);

        //m_unitObjSpawner.SpawnBullet(gameObject, firingPoint, unitData.leftWeapon);
        //m_unitObjSpawner.SpawnBullet(gameObject, firingPoint, unitData.rightWeapon);
        ShootBullets();
	}

    public void ShootBullets()
	{
        m_unitObjSpawner.SpawnPlayerBullet(gameObject, unitData.leftWeapon);
        m_unitObjSpawner.SpawnPlayerBullet(gameObject, unitData.rightWeapon);
    }

    public void Defend(cmdPotency potency)
    {

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
        unitData.health -= dmg;
        m_healthBarController.UpdateHealth(unitData.health);
    }

    public void SetMaxHealth(float health)
	{
        unitData.health = health;
        m_healthBarController.SetMaxHealth(health);
	}

    public void ToggleHealthUI(bool enable)
	{
        m_healthBarController.gameObject.SetActive(enable);
	}
    #endregion
}
