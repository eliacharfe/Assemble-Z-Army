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
    public bool isMoving = false;

    public Macros.Units id;

    public void Move(NavMeshAgent agent, Vector3 dest)
    {
        isMoving = true;
        dest.z = 0;
        agent.SetDestination(dest);

        FlipSideSprite(dest);
    }


    private void FlipSideSprite(Vector3 dest)
    {
        Transform tr = gameObject.transform;
        Transform healthBar = transform.Find("Health Bar Canvas");
        Transform manaBar = transform.Find("Mana Bar Canvas");

        if (dest.x < tr.position.x && tr.localScale.x < Mathf.Epsilon)
        {
            tr.localScale = new Vector3(-tr.localScale.x, tr.localScale.y,
                                        tr.localScale.z);

            healthBar.localScale = new Vector3(-healthBar.localScale.x, healthBar.localScale.y, healthBar.localScale.z);
            if (manaBar)
            {
                manaBar.localScale = new Vector3(-manaBar.localScale.x, manaBar.localScale.y, manaBar.localScale.z);
            }
        }
        else if (dest.x > tr.position.x && tr.localScale.x > Mathf.Epsilon)
        {
            tr.localScale = new Vector3(-tr.localScale.x, tr.localScale.y,
                                         tr.localScale.z);
            healthBar.localScale = new Vector3(-healthBar.localScale.x, healthBar.localScale.y, healthBar.localScale.z);
            if (manaBar)
            {
                manaBar.localScale = new Vector3(-manaBar.localScale.x, manaBar.localScale.y, manaBar.localScale.z);
            }
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
