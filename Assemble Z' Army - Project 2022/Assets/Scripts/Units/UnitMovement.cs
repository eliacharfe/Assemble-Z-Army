using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Utilities;

using Cinemachine;
using Mirror;

public class UnitMovement:NetworkBehaviour
{
    private NavMeshAgent agent = null;

    private Unit unit = null;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        unit = GetComponent<Unit>();
    }

    [Command]
    public void CmdMove(Vector3 dest)
    {
        dest.z = 0;

        GetComponent<Animator>().SetBool("isRunning", true);

        //RpcMoveAnime();

        agent.SetDestination(dest);

        FlipSideSprite(dest);
    }


    public void Move(Vector3 dest)
    {
        dest.z = 0;

        GetComponent<Animator>().SetBool("isRunning", true);

        //RpcMoveAnime();

        agent.SetDestination(dest);

        FlipSideSprite(dest);
    }


    [ClientRpc]
    public void RpcMoveAnime()
    {
        GetComponent<Animator>().SetBool("isRunning", true);
    }

    private void FlipSideSprite(Vector3 dest)
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
