using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : Attacker
{

    public override void StopAttackAnime()
    {
        GetComponent<Animator>().SetBool("isAttacking", false);
    }



    //Todo play attack animation here.
    public override void Attack()
    {
        GetComponent<Animator>().SetBool("isRunning", false);
        GetComponent<Animator>().SetBool("isAttacking", true);
        target.GetComponent<Health>().DealDamage((int)damage);
        print("Recruit should attack");
    }

}
