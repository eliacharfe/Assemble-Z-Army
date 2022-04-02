using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Abstract class for multiple attacks types.
public abstract class Attacker : MonoBehaviour
{
    protected bool isAttacking = false;

    protected bool isInAttackMode = false;
    private bool isInModeAttackAutomated = false;

    protected Targetable target = null;

    [Header("Attack Settings")]
    [SerializeField] private float attackTime;
    [SerializeField] private float range;
    [SerializeField] protected float damage;

    private float time = 0;
 
    private void Start()
    {
        attackTime = GetComponent<Unit>().SpeedAttack.BaseValue;
        damage = GetComponent<Unit>().Attack.BaseValue;
        range = GetComponent<Unit>().ReachDistance.BaseValue;
    }

    private void Update()
    {
        if (!target)
        {
            isInModeAttackAutomated = false;
           // isInAttackMode = false;
            if (isAttacking)
            {
                StopAttack();
                isAttacking = false;
                isInModeAttackAutomated = false;
                isInAttackMode = false;
            }
            return;
        }

        range = GetComponent<Unit>().ReachDistance.BaseValue;

        if (Vector2.Distance(gameObject.transform.position, this.target.transform.position) < range)
        {
            GetComponent<Unit>().StopMove();
            if (time < attackTime)
            {
                time += Time.deltaTime;
            }
            else
            {
                time = 0;
                isAttacking = true;
                //isInModeAttackAutomated = false;
                Attack();
            }
        }
        else
        {

            isAttacking = false;
            GetComponent<Unit>().MoveTo(this.target.transform.position);
        }
    }

    public void SetTargetable(Targetable target)
    {
        this.target = target;
    }

    public bool isInModeAttackAutomate()
    {
        return isInModeAttackAutomated;
    }

    public void SetAutomateAttack()
    {
        isInModeAttackAutomated = true;
    }

    public void SetStopAutomateAttack()
    {
        isInModeAttackAutomated = false;
    }

    public bool isInModeAttack()
    {
        return isInAttackMode;
    }

    public void setAttackMode()
    {
        isInAttackMode = true;
    }

    public void setStopAttackMode()
    {
        isInAttackMode = false;
    }


    public abstract void StopAttack();

    public abstract void Attack();
}

