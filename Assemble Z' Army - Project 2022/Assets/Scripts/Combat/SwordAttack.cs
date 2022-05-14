using UnityEngine;

public class SwordAttack : Attacker
{
    float bonusDamage = 0;

    private void Start()
    {
        var spear = GetComponent<Spear>();
        if (spear)
            bonusDamage = spear.bonuseAttack;
    }

    public override void StopAttackAnime()
    {
        GetComponent<Animator>().SetBool("isAttacking", false);
    }

    public override void Attack()
    {
        GetComponent<Animator>().SetBool("isRunning", false);
        GetComponent<Animator>().SetBool("isAttacking", true);

        var preciseDamage = damage;
        if (target.GetComponent<Horse>())
            preciseDamage += bonusDamage;

        target.GetComponent<Health>().DealDamage((int)preciseDamage);
    }
}
