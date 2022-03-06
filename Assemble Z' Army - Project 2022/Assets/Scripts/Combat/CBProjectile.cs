using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CBProjectile : MonoBehaviour
{
     [Header("CB Projectile Settings")]
    //[SerializeField] private float destroyAfterSeconds = 5;
 
     public int teamNum;
     public float damage;

     private void Start()
     {
     }

     private void Update()
     {
     }

     private void OnTriggerEnter2D(Collider2D collision)
    {
        //arrowPos = transform.position;
        Health target = collision.GetComponent<Health>();
        int collisionTeamNumber = collision.gameObject.GetComponent<Targetable>().teamNumber;

        if (target && collisionTeamNumber != teamNum)
        {
            target.DealDamage(damage);
            Destroy(gameObject);
        }

    }
}
