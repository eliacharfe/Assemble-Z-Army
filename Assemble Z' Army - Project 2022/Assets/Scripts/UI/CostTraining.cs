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
        if (id != TrainingUnitsInBuildings.NONE)
        {
            popupCostBuilding = "Wood: " + GetCostsTrainingUnitInBuilding(id)[0].ToString() + '\n' +
                                "Metal: " + GetCostsTrainingUnitInBuilding(id)[1].ToString() + '\n' +
                                "Gold: " + GetCostsTrainingUnitInBuilding(id)[2].ToString() + '\n' +
                                "Diam's: " + GetCostsTrainingUnitInBuilding(id)[3].ToString() + '\n';
        }
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

    public List<int> GetCostsTrainingUnitInBuildingByIds(Macros.Units idUnit, Macros.Buildings idBuilding)
    {
        if (idUnit == Units.RECRUIT && idBuilding == Buildings.ARCHERY_FIELD)
            return costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.ARCHERY_RECRUIT];
        else if (idUnit == Units.RECRUIT && idBuilding == Buildings.CROSSBOW_FIELD)
            return costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.CROSSBOWERY_RECRUIT];
        else if (idUnit == Units.RECRUIT && idBuilding == Buildings.TEMPLE)
            return costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.TEMPLE_RECRUIT];
        else if (idUnit == Units.RECRUIT && idBuilding == Buildings.STABLE)
            return costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.STABLE_RECRUIT];
        else if (idUnit == Units.RECRUIT && idBuilding == Buildings.SWORD_SMITH)
            return costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.SWORD_SMITH_RECRUIT];
        else if (idUnit == Units.RECRUIT && idBuilding == Buildings.SPEAR_SMITH)
            return costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.SPEARERY_RECRUIT];

        else if (idUnit == Units.SIMPLE_HORSE && idBuilding == Buildings.SWORD_SMITH)
            return costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.SWORD_SMITH_SIMPLE_HORSE];
        else if (idUnit == Units.SIMPLE_HORSE && idBuilding == Buildings.ARCHERY_FIELD)
            return costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.ARCHERY_SIMPLE_HORSE];
        else if (idUnit == Units.SIMPLE_HORSE && idBuilding == Buildings.SPEAR_SMITH)
            return costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.SPEARERY_SIMPLE_HORSE];

        else if (idUnit == Units.ARCHER && idBuilding == Buildings.STABLE)
            return costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.STABLE_ARCHER];

        else if (idUnit == Units.SWORDMAN && idBuilding == Buildings.STABLE)
            return costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.STABLE_SWORDMAN];
        else if (idUnit == Units.SWORDMAN && idBuilding == Buildings.ARMORY)
            return costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.ARMORY_SWORDMAN];

        else if (idUnit == Units.SPEARMAN && idBuilding == Buildings.STABLE)
            return costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.STABLE_SPEARMAN];
        else if (idUnit == Units.SPEARMAN && idBuilding == Buildings.ARMORY)
            return costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.ARMORY_SPEARMAN];

        else if (idUnit == Units.SWORD_HORSE && idBuilding == Buildings.ARMORY)
            return costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.ARMORY_SWORD_HORSE];

        else if (idUnit == Units.SPEAR_HORSE && idBuilding == Buildings.ARMORY)
            return costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.ARMORY_SPEAR_HORSE];

        else if (idUnit == Units.SWORD_KNIGHT && idBuilding == Buildings.STABLE)
            return costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.STABLE_SWORD_KNIGHT];

        else if (idUnit == Units.SPEAR_KNIGHT && idBuilding == Buildings.STABLE)
            return costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.STABLE_SPEAR_KNIGHT];
            
        return null;
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
