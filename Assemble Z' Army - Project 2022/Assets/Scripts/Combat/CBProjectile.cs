using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CBProjectile : MonoBehaviour
{
    [Header("CB Projectile Settings")]
    [SerializeField] private float destroyAfterSeconds;

    public int teamNum;
    public float damage;

    private float time;

    private void Start()
    {
        time = 0;
        destroyAfterSeconds = 0.7f;
    }

    private void Update()
    {
        if (time < destroyAfterSeconds)
        {
            time += Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health target = collision.GetComponent<Health>();
        if (!target)
        {
            return;
        }
        int collisionTeamNumber = collision.gameObject.GetComponent<Targetable>().teamNumber;

        if (collisionTeamNumber != teamNum)
        {
            target.DealDamage(damage);
            Destroy(gameObject);
        }

    }
}