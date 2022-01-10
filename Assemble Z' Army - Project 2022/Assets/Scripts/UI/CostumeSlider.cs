using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class CostumeSlider : MonoBehaviour
{
    [SerializeField] GameObject timeLeft;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    // Increase slider time.
    public void IncreaseSlider(float value)
    {
        /*if (timeLeft.GetComponent<Image>().fillAmount < 1f)
        {
            timeLeft.GetComponent<Image>().fillAmount += value;
        }*/
    }

    public void setValue(float value)
    {
        timeLeft.GetComponent<Image>().fillAmount = value;
    }

    // Intilize slider to 0.
    public void resetSlider()
    {
        timeLeft.GetComponent<Image>().fillAmount = 0;
    }


    // Get fill amount.
    public float FillAmount()
    {
        return timeLeft.GetComponent<Image>().fillAmount;
    }


    // Slider task is done.
    public bool SliderFinished()
    {
        return timeLeft.GetComponent<Image>().fillAmount >= 1f;
    }
}
