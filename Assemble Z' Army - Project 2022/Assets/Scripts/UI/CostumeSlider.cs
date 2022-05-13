using UnityEngine;
using UnityEngine.UI;

public class CostumeSlider : MonoBehaviour
{
    [SerializeField] GameObject timeLeft;

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
