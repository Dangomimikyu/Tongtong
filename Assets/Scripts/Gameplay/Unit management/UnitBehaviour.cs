// note: this class is to manage the behaviour of any individual friendly unit, enemies will have their own behaviour script.
// units will be dependent on the ArmyManager class for the execution of commands because having 1 event listener that controls others makes more sense than having n event listeners all waiting at the same time.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DangoMimikyu.EventManagement;

// macros
using cmdCommand = CommandAtrributes.Commands;
using cmdInput = CommandAtrributes.Inputs;
using cmdPotency = CommandAtrributes.Potency;
using wpnType = WeaponAttributes.WeaponType;

public class UnitBehaviour : MonoBehaviour
{
    [Header("Unit stats")]
    [SerializeField]
    private float m_health = 10;
    [SerializeField]
    private Weapon m_leftHandWeapon;        // defensive item
    [SerializeField]
    private Weapon m_rightHandWeapon;       // offensive item
    [SerializeField]
    private BoxCollider2D m_selfCollider;
    [SerializeField]
    private BoxCollider2D m_aggroCollider;
    // private Enemy m_targettedEnemy
    private bool m_spawned = false;

	#region Monobehaviour functions
	void Start()
    {
        // tell the unit manager that this has been spawned
        //EventManager.instance.DispatchEvent(GameEvents.Unit_Spawn, GetComponent<UnitBehaviour>());
        // init the attack range based on the currently held weapon

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
        // distance depends on the potency
        float multiplier = (float)potency + 0.5f;
        Debug.Log("multiplier: " + multiplier);
        Vector3 vel = Vector3.zero;
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, new Vector3(transform.localPosition.x + multiplier, transform.localPosition.y, transform.localPosition.z), ref vel, Time.deltaTime * 0.01f);
        //transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(transform.localPosition.x + multiplier, transform.localPosition.y, transform.localPosition.z), Time.deltaTime);
	}

    public void Retreat(cmdPotency potency)
	{
        // choose a random point behind
        // distance depends on the potency
	}
    #endregion

    #region Combat functions
    public void Attack(cmdPotency potency, cmdCommand cmd)
	{

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
}
