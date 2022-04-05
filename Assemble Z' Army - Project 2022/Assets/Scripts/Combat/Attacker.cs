using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

// Abstract class for multiple attacks types.
public abstract class Attacker : NetworkBehaviour
{
    protected bool reachedTarget = false;
    protected bool isInAttackMode = false;
    private bool isInModeAttackAutomated = false;

    protected Targetable target = null;

    protected UnitMovement movement = null;

    [Header("Attack Settings")]
    [SerializeField] private float attackTime = 1f;
    [SerializeField] private float range = 1f;
    [SerializeField] protected int damage = 5;

    private float time = 0;

    private void Start()
    {
        movement = GetComponent<UnitMovement>();
    }

    [ServerCallback]
    private void Update()
    {
        if (!target) {
            isInModeAttackAutomated = false;
            if (reachedTarget)
            {
                StopAttackAnime();
                reachedTarget = false;
                isInAttackMode = false;
            }
            return; 
        }

        if(Vector2.Distance(gameObject.transform.position,this.target.transform.position) < range)
        {
            GetComponent<Unit>().StopMove();
            if (time < attackTime)
            {
                time += Time.deltaTime;
            }else
            {
                time = 0;
                reachedTarget = true;
                Attack();
            }
        }
        else
        {
            reachedTarget = false;
            StopAttackAnime();
            if (target && movement)
            {
                movement.Move(this.target.transform.position);

            }
        }
    }

    public void SetAutomateAttack()
    {
        //isInModeAttackAutomated = true;
    }

    public bool isInModeAttackAutomate()
    {
        return isInModeAttackAutomated;
    }

    [Command]
    public void CmdSetTargetable(Targetable target)
    {
        this.target = target;
    }

    public void SetTargetable(Targetable target)
    {
        this.target = target;
    }


    public abstract void StopAttackAnime();

    public abstract void Attack();

    public bool IsAttacking()
    {
        return isInAttackMode;
    }

    public void setAttackMode()
    {
        //isInAttackMode = true;
    }
}
