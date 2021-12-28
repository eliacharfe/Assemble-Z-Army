using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody rb = null;
    [SerializeField] private int damageToDeal = 20;
    [SerializeField] private float destroyAfterSeconds = 5;
    [SerializeField] private float lauchForce = 10f;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

}
