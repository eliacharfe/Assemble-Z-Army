using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpWindow : MonoBehaviour
{
    [SerializeField] private GameObject landingPagePanel = null;
    [SerializeField] private GameObject helpPanel = null;
    [SerializeField] private GameObject instrctionText = null;


    private void Start()
    {
        instrctionText.GetComponent<Text>().text = Macros.GameText.instructions;
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
