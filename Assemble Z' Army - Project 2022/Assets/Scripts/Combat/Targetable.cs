using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetable : MonoBehaviour
{
    [SerializeField] private Transform aimAtPoint = null;
    public int teamNumber = 0;

    public Transform GetAimAtPoint()
    {
        return aimAtPoint;
    }

    public int GetTeamNumber()
    {
        return teamNumber;
    }

    public void Heal()
    {
        GetComponent<Health>().currHealth = 100f;
        GetComponent<HealthDisplay>().HandleHealthUpdated(100, 100);
    }
}
