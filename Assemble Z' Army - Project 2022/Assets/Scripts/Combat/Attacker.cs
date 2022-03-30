using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

// Abstract class for multiple attacks types.
public abstract class Attacker : NetworkBehaviour
{
    protected bool isAttacking = false;

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
            if(isAttacking)
            {
                StopAttackAnime();
                isAttacking = false;
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
                isAttacking = true;
                Attack();
            }
        }
        else
        {
            isAttacking = false;
            StopAttackAnime();
            if (target && movement)
            {
                movement.Move(this.target.transform.position);

            }
        }
    }

    [Command]
    public void CmdSetTargetable(Targetable target)
    {
        this.target = target;
    }


    public abstract void StopAttackAnime();

    public abstract void Attack();
}
