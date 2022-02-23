using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Abstract class for multiple attacks types.
public abstract class Attacker : MonoBehaviour
{
    protected bool isAttacking = false;

    protected Targetable target = null;

    [Header("Attack Settings")]
    [SerializeField] private float attackTime = 1f;
    [SerializeField] private float range = 1f;
    [SerializeField] protected int damage = 5;

    private float time = 0;

    private void Update()
    {
        if (!target) {
            if(isAttacking)
            {
                StopAttack();
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
            GetComponent<Unit>().MoveTo(this.target.transform.position);
        }
    }

    public void SetTargetable(Targetable target)
    {
        this.target = target;
    }


    public abstract void StopAttack();

    public abstract void Attack();
}

