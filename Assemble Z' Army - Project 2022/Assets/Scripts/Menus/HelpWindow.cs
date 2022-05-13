using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpWindow : MonoBehaviour
{
    [SerializeField] private GameObject landingPagePanel = null;
    [SerializeField] private GameObject helpPanel = null;
    
    void Update()
    {
        
    }

    public void ActivateHelpPanel()
    {
      //  landingPagePanel.SetActive(false);
        helpPanel.SetActive(true);

    }

     public void DeactivateHelpPanel()
    {
        //landingPagePanel.SetActive(true);
        helpPanel.SetActive(false);

    }
}
