using System;
using Mirror;
using UnityEngine;

public class Health : NetworkBehaviour
{


    [SerializeField] [SyncVar(hook = nameof(HandleHealthUpdated))] public int currHealth;
    [SerializeField] private Transform damagePopup;
    [SerializeField] private ParticleSystem hitEffect;

    private int maxHealth = 100;

    AudioPlayer audioPlayer;

    public event Action<int, int> ClientOnHealthUpdate;

    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Start()
    {
        currHealth = maxHealth;
    }

    #region server
    [Command]
    public void CmdHeal(int healAmount)
    {
        currHealth = Mathf.Min(currHealth + healAmount, 100);

        ClientOnHealthUpdate?.Invoke(currHealth, maxHealth);
    }
    #endregion


    #region client
    [ClientRpc]
    void InstantiatePopupDamage(bool isCriticalHit)
    {
        DamagePopup popUp = DamagePopup.Create(damagePopup,
                   new Vector3(transform.position.x - 2, transform.position.y + 3f, 0f),
                   (int)currHealth,
                   gameObject.transform.localScale.x,
                   isCriticalHit);
        NetworkServer.Spawn(popUp.gameObject);

        var effectPos = Utilities.Utils.ChangeYAxis(transform.position, transform.position.y + 2);
        //var effect = Instantiate(hitEffect, effectPos, Quaternion.identity);
        //Destroy(effect, 1.5f);
    }
    #endregion

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

            ClientOnHealthUpdate?.Invoke((int)currHealth, maxHealth);
        }
    }

    private void createDamagePopup(bool isCriticalHit)
    {
        InstantiatePopupDamage(isCriticalHit);   
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


    public bool isDead()
    {
        return currHealth <= 0;
    }
}



