using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Health : MonoBehaviour // NetworkBehavior
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] GameObject hitPoint;

    [SerializeField] private Transform damagePopup;

    // [SyncVar (hook = nameof(HandleHealthUpdated))]
    public float currHealth;

    // public event Action ServerOnDie;

    public event Action<int, int> ClientOnHealthUpdate;
    Unit unit;

    AudioPlayer audioPlayer;

    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

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
        audioPlayer.PlayDeathClip();
    }

    public void DealDamage(float damageAmount)
    {
        if (currHealth <= 10)
        {
            DamagePopup.Create(damagePopup,
                          new Vector3(transform.position.x, transform.position.y + 10f, 0f),
                          (int)currHealth, true);
        }
        else
        {
            DamagePopup.Create(damagePopup,
                                      new Vector3(transform.position.x, transform.position.y + 10f, 0f),
                                      (int)currHealth, false);
        }

        if (currHealth == 0)
            return;


        if (damageAmount > unit.Defense.BaseValue)
        {
            currHealth = Mathf.Max(currHealth - damageAmount + unit.Defense.BaseValue, 0);
            PlayHitEffect();
            GetComponent<Animator>().SetBool("gotHit", true);
            if (currHealth > 0)
                audioPlayer.PlayDamageClip();
        }

        ClientOnHealthUpdate?.Invoke((int)currHealth, maxHealth);

    }

    public void PlayHitEffect()
    {
        // Vector3 position = new Vector3(transform.position.x, transform.position.y + 10f, transform.position.z);
        if (hitEffect != null)
        {
            ParticleSystem instance = Instantiate(hitEffect, hitPoint.transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    // client
    // private void HandleHealthUpdated(int oldHealth, int newHealth)
    // {
    //     ClientOnHealthUpdate?.Invoke(newHealth, maxHealth);
    // }
}
