using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaDisplay : MonoBehaviour
{
    [SerializeField] private Health mana = null;
    [SerializeField] private GameObject manaBar = null;
    [SerializeField] private Image manaBarImage = null;


    private void Awake()
    {
        manaBar.SetActive(true);
        mana.ClientOnHealthUpdate += HandleHealthUpdated;
    }

    private void OnDestroy()
    {
        mana.ClientOnHealthUpdate -= HandleHealthUpdated;
    }

    private void OnMouseEnter()
    {
        //healthBar.SetActive(true);
    }

    private void OnMouseExit()
    {
        //healthBar.SetActive(false);
    }

    public void HandleHealthUpdated(int currHealth, int maxHealth)
    {
        manaBarImage.fillAmount = (float)currHealth / maxHealth;
    }
}
