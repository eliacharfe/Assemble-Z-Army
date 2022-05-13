using UnityEngine;

public class SwordAttack : Attacker
{
    public override void StopAttackAnime()
    {
        GetComponent<Animator>().SetBool("isAttacking", false);
    }

    public override void Attack()
    {
        GetComponent<Animator>().SetBool("isRunning", false);
        GetComponent<Animator>().SetBool("isAttacking", true);
        target.GetComponent<Health>().DealDamage((int)damage);
    }
}
