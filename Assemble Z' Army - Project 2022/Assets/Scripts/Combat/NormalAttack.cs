using UnityEngine;

public class NormalAttack : Attacker
{
    private float bonusDamage = 0;

    private void Awake()
    {
        // Is spear type
        Spear spear = GetComponent<Spear>();
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

        float preciseDamage = damage;
        if (target.GetComponent<Horse>())
            preciseDamage += bonusDamage;

        target.GetComponent<Health>().DealDamage((int)preciseDamage);
    }
}
