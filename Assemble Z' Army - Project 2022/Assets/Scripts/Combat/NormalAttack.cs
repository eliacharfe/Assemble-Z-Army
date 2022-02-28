using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttack : Attacker
{

    public override void StopAttack()
    {
        GetComponent<Animator>().SetBool("isAttacking", false);
    }



    //Todo play attack animation here.
    public override void Attack()
    {
        GetComponent<Animator>().SetBool("isAttacking", true);
        target.GetComponent<Health>().DealDamage(damage);
    }

}
