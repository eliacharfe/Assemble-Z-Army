using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Macros;

public class CostTraining : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler
                                                                                 , IPointerExitHandler
{
    string popupCostBuilding;
    [SerializeField] private Macros.TrainingUnitsInBuildings id;

    private Dictionary<Macros.TrainingUnitsInBuildings, List<int>> costsTrainingUnitsInBuildings =
     new Dictionary<Macros.TrainingUnitsInBuildings, List<int>>();

    //  ResourcesCostTraining costs = null;

    void Start()
    {
        InitCostsTrainingUnitInBuilding();

        //  costs = FindObjectOfType<ResourcesCostTraining>();

        popupCostBuilding = "Wood: " + GetCostsTrainingUnitInBuilding(id)[0].ToString() + '\n' +
                            "Metal: " + GetCostsTrainingUnitInBuilding(id)[1].ToString() + '\n' +
                            "Gold: " + GetCostsTrainingUnitInBuilding(id)[2].ToString() + '\n' +
                            "Diam's: " + GetCostsTrainingUnitInBuilding(id)[3].ToString() + '\n';

        // popupCostBuilding = "Wood: " + costs.getCostsTrainingUnitInBuilding(id)[0].ToString() + '\n' +
        //                     "Metal: " + costs.getCostsTrainingUnitInBuilding(id)[1].ToString() + '\n' +
        //                     "Gold: " + costs.getCostsTrainingUnitInBuilding(id)[2].ToString() + '\n' +
        //                     "Diamonds: " + costs.getCostsTrainingUnitInBuilding(id)[3].ToString() + '\n';
    }

    void Update()
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("onPointerEnter");
        Tooltip.ShowTooltip_Static(popupCostBuilding, id.ToString());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("onPointerEnter");
        Tooltip.HideTooltip_Static();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("onPointerUp");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("onPointerDown");
    }


    public List<int> GetCostsTrainingUnitInBuilding(Macros.TrainingUnitsInBuildings id)
    {
        return costsTrainingUnitsInBuildings[id];
    }

    private void InitCostsTrainingUnitInBuilding()
    {
        costsTrainingUnitsInBuildings.Add(Macros.TrainingUnitsInBuildings.ARCHERY_RECRUIT, new List<int>());
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.ARCHERY_RECRUIT].Add(15);
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.ARCHERY_RECRUIT].Add(5);
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.ARCHERY_RECRUIT].Add(1);
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.ARCHERY_RECRUIT].Add(0);

        costsTrainingUnitsInBuildings.Add(Macros.TrainingUnitsInBuildings.ARCHERY_SIMPLE_HORSE, new List<int>());
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.ARCHERY_SIMPLE_HORSE].Add(20);
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.ARCHERY_SIMPLE_HORSE].Add(7);
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.ARCHERY_SIMPLE_HORSE].Add(1);
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.ARCHERY_SIMPLE_HORSE].Add(1);

        costsTrainingUnitsInBuildings.Add(Macros.TrainingUnitsInBuildings.ARMORY_SPEAR_HORSE, new List<int>());
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.ARMORY_SPEAR_HORSE].Add(25);
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.ARMORY_SPEAR_HORSE].Add(10);
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.ARMORY_SPEAR_HORSE].Add(2);
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.ARMORY_SPEAR_HORSE].Add(1);

        costsTrainingUnitsInBuildings.Add(Macros.TrainingUnitsInBuildings.ARMORY_SPEARMAN, new List<int>());
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.ARMORY_SPEARMAN].Add(10);
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.ARMORY_SPEARMAN].Add(6);
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.ARMORY_SPEARMAN].Add(1);
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.ARMORY_SPEARMAN].Add(0);

        costsTrainingUnitsInBuildings.Add(Macros.TrainingUnitsInBuildings.ARMORY_SWORD_HORSE, new List<int>());
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.ARMORY_SWORD_HORSE].Add(25);
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.ARMORY_SWORD_HORSE].Add(12);
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.ARMORY_SWORD_HORSE].Add(2);
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.ARMORY_SWORD_HORSE].Add(1);

        costsTrainingUnitsInBuildings.Add(Macros.TrainingUnitsInBuildings.ARMORY_SWORDMAN, new List<int>());
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.ARMORY_SWORDMAN].Add(15);
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.ARMORY_SWORDMAN].Add(10);
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.ARMORY_SWORDMAN].Add(1);
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.ARMORY_SWORDMAN].Add(1);

        costsTrainingUnitsInBuildings.Add(Macros.TrainingUnitsInBuildings.CROSSBOWERY_RECRUIT, new List<int>());
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.CROSSBOWERY_RECRUIT].Add(13);
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.CROSSBOWERY_RECRUIT].Add(7);
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.CROSSBOWERY_RECRUIT].Add(1);
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.CROSSBOWERY_RECRUIT].Add(0);

        costsTrainingUnitsInBuildings.Add(Macros.TrainingUnitsInBuildings.SPEARERY_RECRUIT, new List<int>());
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.SPEARERY_RECRUIT].Add(10);
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.SPEARERY_RECRUIT].Add(5);
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.SPEARERY_RECRUIT].Add(0);
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.SPEARERY_RECRUIT].Add(0);

        costsTrainingUnitsInBuildings.Add(Macros.TrainingUnitsInBuildings.SPEARERY_SIMPLE_HORSE, new List<int>());
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.SPEARERY_SIMPLE_HORSE].Add(15);
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.SPEARERY_SIMPLE_HORSE].Add(12);
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.SPEARERY_SIMPLE_HORSE].Add(1);
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.SPEARERY_SIMPLE_HORSE].Add(0);

        costsTrainingUnitsInBuildings.Add(Macros.TrainingUnitsInBuildings.STABLE_ARCHER, new List<int>());
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.STABLE_ARCHER].Add(20);
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.STABLE_ARCHER].Add(10);
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.STABLE_ARCHER].Add(2);
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.STABLE_ARCHER].Add(1);

        costsTrainingUnitsInBuildings.Add(Macros.TrainingUnitsInBuildings.STABLE_RECRUIT, new List<int>());
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.STABLE_RECRUIT].Add(15);
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.STABLE_RECRUIT].Add(8);
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.STABLE_RECRUIT].Add(1);
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.STABLE_RECRUIT].Add(1);

        costsTrainingUnitsInBuildings.Add(Macros.TrainingUnitsInBuildings.STABLE_SPEAR_KNIGHT, new List<int>());
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.STABLE_SPEAR_KNIGHT].Add(25);
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.STABLE_SPEAR_KNIGHT].Add(15);
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.STABLE_SPEAR_KNIGHT].Add(3);
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.STABLE_SPEAR_KNIGHT].Add(2);

        costsTrainingUnitsInBuildings.Add(Macros.TrainingUnitsInBuildings.STABLE_SPEARMAN, new List<int>());
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.STABLE_SPEARMAN].Add(20);
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.STABLE_SPEARMAN].Add(13);
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.STABLE_SPEARMAN].Add(2);
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.STABLE_SPEARMAN].Add(1);

        costsTrainingUnitsInBuildings.Add(Macros.TrainingUnitsInBuildings.STABLE_SWORD_KNIGHT, new List<int>());
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.STABLE_SWORD_KNIGHT].Add(25);
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.STABLE_SWORD_KNIGHT].Add(15);
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.STABLE_SWORD_KNIGHT].Add(3);
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.STABLE_SWORD_KNIGHT].Add(2);

        costsTrainingUnitsInBuildings.Add(Macros.TrainingUnitsInBuildings.STABLE_SWORDMAN, new List<int>());
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.STABLE_SWORDMAN].Add(20);
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.STABLE_SWORDMAN].Add(10);
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.STABLE_SWORDMAN].Add(2);
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.STABLE_SWORDMAN].Add(1);

        costsTrainingUnitsInBuildings.Add(Macros.TrainingUnitsInBuildings.SWORD_SMITH_RECRUIT, new List<int>());
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.SWORD_SMITH_RECRUIT].Add(13);
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.SWORD_SMITH_RECRUIT].Add(7);
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.SWORD_SMITH_RECRUIT].Add(1);
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.SWORD_SMITH_RECRUIT].Add(0);

        costsTrainingUnitsInBuildings.Add(Macros.TrainingUnitsInBuildings.SWORD_SMITH_SIMPLE_HORSE, new List<int>());
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.SWORD_SMITH_SIMPLE_HORSE].Add(17);
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.SWORD_SMITH_SIMPLE_HORSE].Add(11);
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.SWORD_SMITH_SIMPLE_HORSE].Add(2);
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.SWORD_SMITH_SIMPLE_HORSE].Add(0);

        costsTrainingUnitsInBuildings.Add(Macros.TrainingUnitsInBuildings.TEMPLE_RECRUIT, new List<int>());
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.TEMPLE_RECRUIT].Add(10);
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.TEMPLE_RECRUIT].Add(4);
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.TEMPLE_RECRUIT].Add(0);
        costsTrainingUnitsInBuildings[Macros.TrainingUnitsInBuildings.TEMPLE_RECRUIT].Add(2);
    }


}
