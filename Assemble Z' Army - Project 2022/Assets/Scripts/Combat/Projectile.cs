using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Projectile Settings")]
    [SerializeField] private int damageToDeal = 20;
    [SerializeField] private float destroyAfterSeconds = 5;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health target = collision.GetComponent<Health>();
        if (target)
        {
            target.DealDamage(damageToDeal);
        }

        Destroy(gameObject);
    }
}
