using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSlider:MonoBehaviour
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

        if (timeLeft.transform.localScale.x <= 1f)
        {
            Debug.Log("Increase slider by" +  value);
            timeLeft.transform.localScale = new Vector3( value,
                timeLeft.transform.localScale.y, 0);
        }
    }

    public void resetSlider()
    {
        timeLeft.transform.localScale = (new Vector3(0,
            timeLeft.transform.localScale.y, 0));
    }
}
