using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private Health health = null;
    [SerializeField] private GameObject healthBar = null;
    [SerializeField] private Image healthBarImage = null;


    private void Awake()
    {
        healthBar.SetActive(true);
        health.ClientOnHealthUpdate += HandleHealthUpdated;
    }

    private void OnDestroy()
    {
        health.ClientOnHealthUpdate -= HandleHealthUpdated;
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
        healthBarImage.fillAmount = (float)currHealth / maxHealth;
    }

}
