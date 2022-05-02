using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PathsTraining : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private GameObject landingPagePanel = null;
     [SerializeField] private GameObject pathsTrainingPanel = null;

       public Building building = null;

      string popupCostBuilding;

    void Start()
    {
        //  popupCostBuilding = "Wood: " + building.getCostBuilding()[0].ToString() + '\n' +
        //                     "Metal: " + building.getCostBuilding()[1].ToString() + '\n' +
        //                     "Gold: " + building.getCostBuilding()[2].ToString() + '\n' +
        //                     "Diamonds: " + building.getCostBuilding()[3].ToString() + '\n';
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //  public void OnPointerEnter(PointerEventData eventData)
    // {
    //     Debug.Log("onMouseEnter");
    //     Tooltip.ShowTooltip_Static(popupCostBuilding, id.ToString());
    // }

    // public void OnPointerOver(PointerEventData eventData)
    // {
    //     Debug.Log("onMouseOver");
    //     Tooltip.ShowTooltip_Static(popupCostBuilding, id.ToString());
    // }

    // private void OnMouseEnter()
    // {
    //     Debug.Log("onMouseEnter");
    //     Tooltip.ShowTooltip_Static(popupCostBuilding, id.ToString());
    // }

    // private void OnMouseOver()
    // {
    //     Debug.Log("onMouseOverrr");
    //     Tooltip.ShowTooltip_Static(popupCostBuilding, id.ToString());
    // }

    public void ActivatePathsCanvas()
    {
        landingPagePanel.SetActive(false);
        pathsTrainingPanel.SetActive(true);

    }

     public void DeactivatePathsCanvas()
    {
        landingPagePanel.SetActive(true);
        pathsTrainingPanel.SetActive(false);

    }

    public void OnPointerUp(PointerEventData eventData)
    {
       //   Tooltip.HideTooltip_Static();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Tooltip.ShowTooltip_Static(popupCostBuilding, "some name building");
    }
}