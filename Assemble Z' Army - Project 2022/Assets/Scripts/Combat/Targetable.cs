using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class Targetable : NetworkBehaviour
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

}
