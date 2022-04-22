using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionAttack : MonoBehaviour
{
    GameObject unit = null;
    void Start()
    {
        unit = gameObject.transform.parent.gameObject;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (!unit && unit.GetComponent<UnitMovement>().IsMoving())
        {
            return;
        }

        if (unit.GetComponent<Attacker>().IsAttacking())
        {
            return;
        }

        if (!other.gameObject.GetComponent<Targetable>())
        {
            return;
        }

        if (other.TryGetComponent<Unit>(out Unit targetUnit) && 
            targetUnit.connectionToClient == gameObject.GetComponentInParent<Unit>().connectionToClient)
        {
            return;
        }

        if (unit.GetComponent<Attacker>().isInModeAttackAutomate())
        {
            return;
        }

        unit.GetComponent<Attacker>().SetTargetable(other.gameObject.GetComponent<Targetable>());
        unit.GetComponent<Attacker>().SetAutomateAttack();
    }
}
