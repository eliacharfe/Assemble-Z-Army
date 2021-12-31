using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Utilities;

using Cinemachine;


public class UnitMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent = null;

    void Start()
    {
        //  gameObject.GetComponent<NavMeshAgent>().enabled = true;
        //  agent = GetComponent<NavMeshAgent>();
        // agent.updateRotation = false;
        // agent.updateUpAxis = false;
    }

    void Update()
    {
        // Debug.Log("upd unitMove");

        // if (!agent.hasPath){
        //     return;
        // }

        // if (agent.remainingDistance > agent.stoppingDistance){
        //     return;
        // }

        // agent.ResetPath();
    }

    // public void MoveU(Unit unit)
    // {
    //     unit.MoveTo(Utils.GetMouseWorldPosition());
    //     // Camera.main.ScreenToWorldPoint(
    //     //     new Vector2(Utils.GetMouseWorldPosition().x, 
    //     //                 Utils.GetMouseWorldPosition().y));
    // }
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
