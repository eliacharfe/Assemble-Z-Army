using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class Targetable : NetworkBehaviour
{
    public int teamNumber = 0;

    public int GetTeamNumber()
    {
        return teamNumber;
    }

}
