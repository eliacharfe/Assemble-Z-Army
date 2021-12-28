using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowAttack : Attacker
{
    [SerializeField] private GameObject arrowPrefab = null;

    public override void Attack()
    {
        Debug.Log("Shooting Enemy with arrows");
        
    }
}
