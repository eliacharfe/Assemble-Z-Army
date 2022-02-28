using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Health : MonoBehaviour // NetworkBehavior
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] ParticleSystem hitEffect;

    // [SyncVar (hook = nameof(HandleHealthUpdated))]
    public float currHealth;

    // public event Action ServerOnDie;

    public event Action<int, int> ClientOnHealthUpdate;
    Unit unit;

    void Start()
    {
        unit = GetComponent<Unit>();
        currHealth = maxHealth;
    }

    void Update()
    {
        if (currHealth <= 0)
        {
            unit.StopMove();
            unit.SetDead();
        }
    }

    void Dead()
    {
        Destroy(gameObject);
    }

    public void DealDamage(float damageAmount)
    {
        if (currHealth == 0)
            return;

        if (damageAmount > unit.Defense.BaseValue)
        {
            currHealth = Mathf.Max(currHealth - damageAmount + unit.Defense.BaseValue, 0);
            PlayHitEffect();
        }

        ClientOnHealthUpdate?.Invoke((int)currHealth, maxHealth);


        if (currHealth != 0)
            return;

    }

    public void PlayHitEffect()
    {
        Vector3 position = new Vector3(transform.position.x, transform.position.y + 10f, transform.position.z);
        if (hitEffect != null)
        {
            ParticleSystem instance = Instantiate(hitEffect, position , Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    // client
    // private void HandleHealthUpdated(int oldHealth, int newHealth)
    // {
    //     ClientOnHealthUpdate?.Invoke(newHealth, maxHealth);
    // }
}
