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
            return;

        currHealth = Mathf.Max(currHealth - damageAmount, 0);

        ClientOnHealthUpdate?.Invoke(currHealth, maxHealth);

        if (currHealth != 0)
            return;

    }

   
     private void HandleHealthUpdated(int oldHealth, int newHealth)
     {
         ClientOnHealthUpdate?.Invoke(newHealth, maxHealth);

        if (currHealth <= 0)
        {
            GetComponent<Unit>().SetDead();
            GetComponent<Unit>().isDead = true;
            GetComponent<Unit>().StopMove();
            GetComponent<Animator>().SetBool("isDead", true);
            Destroy(gameObject,2f);
        }
    }
}
