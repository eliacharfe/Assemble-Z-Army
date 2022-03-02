using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class UIDisplay : MonoBehaviour
{
    [Header("Resources Slider")]
    [SerializeField] Slider woodSlider;
    [SerializeField] Slider metalSlider;
    [SerializeField] Slider goldSlider;
    [SerializeField] Slider diamondsSlider;

    [Header("Resources Text")]
    [SerializeField] TextMeshProUGUI woodText;
    [SerializeField] TextMeshProUGUI metalText;
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI diamondsText;

    ResourcesPlayer resourcesPlayer;

    private void Awake()
    {
        resourcesPlayer = FindObjectOfType<ResourcesPlayer>();
    }

    private void Start()
    {
        woodSlider.maxValue = (int)Macros.Resources.WOOD;
        // metalSlider.maxValue = (int)Macros.Resources.METAL;
        // goldSlider.maxValue = (int)Macros.Resources.GOLD;
        // diamondsSlider.maxValue = (int)Macros.Resources.DIAMONDS;
    }

    private void Update()
    {
        woodSlider.value = resourcesPlayer.GetWood();
        woodText.text = resourcesPlayer.GetWood().ToString();
    
        // metalSlider.value = resourcesPlayer.GetMetal();
        //  metalText.text = resourcesPlayer.GetMetal().ToString();
        // goldSlider.value = resourcesPlayer.GetGold();
        //  goldText.text = resourcesPlayer.GetGold().ToString();
        // diamondsSlider.value = resourcesPlayer.GetDiamonds();
        //  diamondsText.text = resourcesPlayer.GetDiamonds().ToString();
    }
}
