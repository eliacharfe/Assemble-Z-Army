using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaDisplay : MonoBehaviour
{
    [SerializeField] private Mana mana = null;
    [SerializeField] private GameObject manaBar = null;
    [SerializeField] public Image manaBarImage = null;


    private void Awake()
    {
        manaBar.SetActive(true);
        mana.ClientOnManaUpdate += HandleManaUpdated;
    }

    private void OnDestroy()
    {
        mana.ClientOnManaUpdate -= HandleManaUpdated;
    }

    private void OnMouseEnter()
    {
        //healthBar.SetActive(true);
    }

    private void OnMouseExit()
    {
        //healthBar.SetActive(false);
    }

    public void HandleManaUpdated(int currMana, int maxMana)
    {
        manaBarImage.fillAmount = (float)currMana / maxMana;
    }
}
