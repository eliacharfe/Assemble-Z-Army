using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : Attacker
{
    public override void Attack()
    {
        target.GetComponent<Health>().DealDamage(damage);
    }

}
