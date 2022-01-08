using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Utilities;

using Cinemachine;


public class UnitMovement:MonoBehaviour
{

    public void Move(Unit unit, NavMeshAgent agent, Vector3 dest)
    {
        dest.z = 0;
        agent.SetDestination(dest);

        FlipSideSprite(unit , dest);
    }


    private void FlipSideSprite(Unit unit, Vector3 dest)
    {
        if (dest.x < unit.transform.position.x && unit.transform.localScale.x > Mathf.Epsilon)
        {
            unit.transform.localScale = new Vector3(-unit.transform.localScale.x, unit.transform.localScale.y,
            unit.transform.localScale.z);
        }
        else if (dest.x > unit.transform.position.x && unit.transform.localScale.x < Mathf.Epsilon)
        {
            unit.transform.localScale = new Vector3(-unit.transform.localScale.x, unit.transform.localScale.y,
                        unit.transform.localScale.z);
        }
    }
}











// namespace UnitMove{
//     public static class Movement{

//    // [SerializeField] private static NavMeshAgent agent = null;

//     private static void Update()
//     {
//         Debug.Log("upd unitMove");

//     }

//     public static void MoveUnit(Unit unit)
//     {
//         NavMeshAgent agent = unit.GetComponent<NavMeshAgent>();
//          if (!agent.hasPath){
//             return;
//         }

//         if (agent.remainingDistance > agent.stoppingDistance){
//             return;
//         }

//         agent.ResetPath();

//             unit.MoveTo(Utils.GetMouseWorldPosition());
//             // Camera.main.ScreenToWorldPoint(
//             //     new Vector2(Utils.GetMouseWorldPosition().x, 
//             //                 Utils.GetMouseWorldPosition().y));
//     }
// }
// }
