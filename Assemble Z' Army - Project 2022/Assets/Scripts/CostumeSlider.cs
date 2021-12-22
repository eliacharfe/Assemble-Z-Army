using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CostumeSlider:MonoBehaviour
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


    public void setFill(float value)
    {

        if (timeLeft.GetComponent<Image>().fillAmount <= 1f)
        {
            Debug.Log("Increase slider by" +  value);
            timeLeft.GetComponent<Image>().fillAmount = value;
        }
    }

    public void resetSlider()
    {
        timeLeft.GetComponent<Image>().fillAmount = 0;
    }
}
