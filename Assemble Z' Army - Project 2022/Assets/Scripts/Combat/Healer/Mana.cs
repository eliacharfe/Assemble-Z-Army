using System;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class Mana : NetworkBehaviour
{
    [SerializeField] ParticleSystem manaEffect;
    [SerializeField] GameObject healingPointStart;
    [SyncVar(hook = nameof(HandleManaValueUpdated))] public int currMana;

    private int maxMana = 100;
    private float time;
    private float regenerateTime;
    private bool isRegenerating;
    private Image manaBarImage;

    private Color BaseColor;

    public bool canHeal;
    public event Action<int, int> ClientOnManaUpdate;

    private void Start()
    {
        currMana = maxMana;
        time = 0;
        regenerateTime = 5f;
        canHeal = true;
        isRegenerating = false;
        manaBarImage = GetComponent<ManaDisplay>().manaBarImage;
        BaseColor = manaBarImage.color;
    }

    #region server
    [Command]
    public void CmdUseHeal()
    {
        GetComponent<Animator>().SetBool("isHealing", true);

        currMana -= 35;

        if (currMana < 0)
        {
            currMana = 0;
        }

        GetComponent<Mana>().RpcPlayManaEffect();
    }

    private void HandleManaValueUpdated(int oldMana, int newMana)
    {
        ClientOnManaUpdate?.Invoke(newMana, maxMana);
    }
    #endregion

    #region client
    [ClientCallback]
    private void Update()
    {
        if (currMana <= 0 && !isRegenerating)
        {
            isRegenerating = true;
        }

        if (isRegenerating)
        {
            if (time < regenerateTime)
            {
                canHeal = false;
                time += Time.deltaTime;
                currMana = (int)(time * 20f);
                manaBarImage.color = Color.yellow;
                ClientOnManaUpdate?.Invoke((int)currMana, 100);
            }
            else
            {
                time = 0;
                canHeal = true;
                isRegenerating = false;
                currMana = 100;
                manaBarImage.color = BaseColor;
                ClientOnManaUpdate?.Invoke((int)currMana, 100);
            }
        }
    }

    [ClientRpc]
    public void RpcPlayManaEffect()
    {
        if (manaEffect != null)
        {
            ParticleSystem instance = Instantiate(manaEffect, healingPointStart.transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }
    #endregion

    public void StopAnimation()
    { 
        GetComponent<Animator>().SetBool("isHealing", false);
    }
}
