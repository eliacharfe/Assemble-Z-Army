using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilidingConstruction : MonoBehaviour
{
    [SerializeField] private CostumeSlider buldingConstructionSlider = null; 
    
    public void setBuildingTime(float value)
    {
        buldingConstructionSlider.setFill(value);
    }
}
