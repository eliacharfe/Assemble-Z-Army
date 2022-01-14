using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Projectile : NetworkBehaviour
{
    [Header("Projectile Settings")]
    [SerializeField] private int damageToDeal = 20;
    [SerializeField] private float destroyAfterSeconds = 5;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<NetworkIdentity>(out NetworkIdentity networkIdentity))
        {
            if (networkIdentity.connectionToClient == connectionToClient) {

                Debug.Log("Belong to the same client:" + connectionToClient + " arrow client:" + networkIdentity.connectionToClient);
                return; }

            Health target = networkIdentity.GetComponent<Health>();
            if (target)
            {
                target.DealDamage(damageToDeal);
            }

            Destroy(gameObject);
        }
    }
}
