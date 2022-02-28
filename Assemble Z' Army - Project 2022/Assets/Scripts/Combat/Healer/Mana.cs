using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Mana : MonoBehaviour
{
    [SerializeField] private int maxMana = 100;

    public float currMana;

    private float time;
    private float regenerateTime;

    public bool canHeal;

    private void Start()
    {
        currMana = maxMana;
        time = 0;
        regenerateTime = 5f;
        canHeal = true;
    }

    // To Do: mana timer regeration
    private void Update()
    {
        if (currMana <= 0)
        {
            if (time < regenerateTime)
            {
                canHeal = false;
                time += Time.deltaTime;
            }
            else
            {
                time = 0;
                canHeal = true;
                currMana = 100f;
                GetComponent<ManaDisplay>().HandleHealthUpdated((int)currMana, 100);
            }
        }

    }

}
