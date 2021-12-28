using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Health : MonoBehaviour // NetworkBehavior
{

    [SerializeField] private int maxHealth = 100;

   // [SyncVar (hook = nameof(HandleHealthUpdated))]
    private int currHealth;

    // public event Action ServerOnDie;

    public event Action<int, int> ClientOnHealthUpdate;


    void Start()
    {
        currHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

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

     // client
    // private void HandleHealthUpdated(int oldHealth, int newHealth)
    // {
    //     ClientOnHealthUpdate?.Invoke(newHealth, maxHealth);
    // }
}
