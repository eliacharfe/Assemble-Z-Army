using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Utilities;

public class UnitMovement : MonoBehaviour
{


    // private void Start()
    // {
    // }

    // private void Update()
    // {
    // }

    // //-----------------------------
     public void MoveUnit(Unit unit)
    {
        //foreach (Unit unit in GetMyUnits())
        {
            unit.MoveTo(Utils.GetMouseWorldPosition());
        
        }
    }
 
}
