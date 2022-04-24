using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Health : NetworkBehaviour // NetworkBehavior
{

    private int maxHealth = 100;

    // [SyncVar (hook = nameof(HandleHealthUpdated))]
    [SerializeField] [SyncVar(hook = nameof(HandleHealthUpdated))] public int currHealth;
    [SerializeField] private Transform damagePopup;

    // public event Action ServerOnDie;

    public event Action<int, int> ClientOnHealthUpdate;


    void Start()
    {
        currHealth = maxHealth;
    }

    void Update()
    {

    }


    [Command]
    public void CmdHeal(int healAmount)
    {
        currHealth = Mathf.Min(currHealth + healAmount, 100);

        ClientOnHealthUpdate?.Invoke(currHealth, maxHealth);
    }

    public void DealDamage(int damageAmount)
    {

        if (currHealth == 0)
        {
            createDamagePopup(true);
            return;
        }      

        currHealth = Mathf.Max(currHealth - damageAmount, 0);

        if (currHealth <= 10)
        {
            createDamagePopup(true);
        }
        else
        {
            createDamagePopup(false);
        }

        ClientOnHealthUpdate?.Invoke((int)currHealth, maxHealth);
    }

    private void createDamagePopup(bool isCriticalHit)
    {
        DamagePopup.Create(damagePopup,
                           new Vector3(transform.position.x, transform.position.y + 3f, 0f),
                           (int)currHealth,
                           isCriticalHit);
    }


    private void HandleHealthUpdated(int oldHealth, int newHealth)
    {
        ClientOnHealthUpdate?.Invoke(newHealth, maxHealth);

        if (currHealth <= 0)
        {
            GetComponent<Unit>().SetDead();
            GetComponent<Unit>().isDead = true;
            GetComponent<Unit>().CmdStopMove();
            GetComponent<Animator>().SetBool("isDead", true);
            Destroy(gameObject, 2f);
        }
    }
}



