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

        if (unit.GetComponent<UnitMovement>().isMoving)
        {
            return;
        }

        if (unit.GetComponent<Attacker>().isInModeAttack())
        {
            return;
        }

        if (!other.gameObject.GetComponent<Targetable>())
        {
            return;
        }

        if (other.gameObject.GetComponent<Targetable>().teamNumber == unit.GetComponent<Targetable>().teamNumber)
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
