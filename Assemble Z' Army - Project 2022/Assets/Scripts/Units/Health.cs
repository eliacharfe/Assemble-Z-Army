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
    [SerializeField] ParticleSystem hitEffect;
    AudioPlayer audioPlayer;

    // public event Action ServerOnDie;

    public event Action<int, int> ClientOnHealthUpdate;

    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

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

            int def = (int)GetComponent<Unit>().Defense.BaseValue;

        if (damageAmount > def)
        {
            currHealth = Mathf.Max(currHealth - damageAmount + def, 0);

            if (currHealth <= 10)
            {
                createDamagePopup(true);
            }
            else
            {
                createDamagePopup(false);
            }

            GetComponent<Animator>().SetBool("gotHit", true);
            /*   if (currHealth > 0)
                audioPlayer.PlayDamageClip();*/

            ClientOnHealthUpdate?.Invoke((int)currHealth, maxHealth);
        }
    }


    private void createDamagePopup(bool isCriticalHit)
    {
        InstantiatePopupDamage(isCriticalHit);   
    }

    [ClientRpc]
    void InstantiatePopupDamage(bool isCriticalHit)
    {
        DamagePopup popUp = DamagePopup.Create(damagePopup,
                   new Vector3(transform.position.x-2, transform.position.y + 3f, 0f),
                   (int)currHealth,
                   gameObject.transform.localScale.x,
                   isCriticalHit);
        NetworkServer.Spawn(popUp.gameObject);

        var effectPos = Utilities.Utils.ChangeYAxis(transform.position,transform.position.y+2);
        Instantiate(hitEffect, effectPos, Quaternion.identity);
    }

    private void HandleHealthUpdated(int oldHealth, int newHealth)
    {
        ClientOnHealthUpdate?.Invoke(newHealth, maxHealth);

        if (currHealth <= 0)
        {
            StopHitAnimation();
            GetComponent<Unit>().SetDead();
            GetComponent<Unit>().StopMove();
            GetComponent<Animator>().SetBool("isDead", true);
            Destroy(gameObject, 2f);
        }
    }

    public void StopHitAnimation()
    {
        GetComponent<Animator>().SetBool("gotHit", false);
    }
}



