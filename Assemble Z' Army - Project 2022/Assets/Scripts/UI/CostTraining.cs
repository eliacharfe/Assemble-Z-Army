using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Macros;

public class CostTraining : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    string popupCostBuilding;
    [SerializeField] private TrainingUnitsInBuildings id;

    private Dictionary<TrainingUnitsInBuildings, List<int>> costsTrainingUnitsInBuildings =
     new Dictionary<TrainingUnitsInBuildings, List<int>>();

    void Start()
    {
        InitCostsTrainingUnitInBuilding();
        popupCostBuilding = "Wood: " + GetCostsTrainingUnitInBuilding(id)[0].ToString() + '\n' +
                            "Metal: " + GetCostsTrainingUnitInBuilding(id)[1].ToString() + '\n' +
                            "Gold: " + GetCostsTrainingUnitInBuilding(id)[2].ToString() + '\n' +
                            "Diamonds: " + GetCostsTrainingUnitInBuilding(id)[3].ToString() + '\n';

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Tooltip.ShowTooltip_Static(popupCostBuilding, id.ToString());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tooltip.HideTooltip_Static();
    }

    public List<int> GetCostsTrainingUnitInBuilding(TrainingUnitsInBuildings id)
    {
        return costsTrainingUnitsInBuildings[id];
    }

    private void InitCostsTrainingUnitInBuilding()
    {
        // Level 1 units
        costsTrainingUnitsInBuildings.Add(TrainingUnitsInBuildings.ARCHERY_RECRUIT, new List<int>());
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.ARCHERY_RECRUIT].Add(20);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.ARCHERY_RECRUIT].Add(20);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.ARCHERY_RECRUIT].Add(20);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.ARCHERY_RECRUIT].Add(5);

        costsTrainingUnitsInBuildings.Add(TrainingUnitsInBuildings.CROSSBOWERY_RECRUIT, new List<int>());
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.CROSSBOWERY_RECRUIT].Add(20);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.CROSSBOWERY_RECRUIT].Add(20);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.CROSSBOWERY_RECRUIT].Add(20);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.CROSSBOWERY_RECRUIT].Add(5);

        costsTrainingUnitsInBuildings.Add(TrainingUnitsInBuildings.SPEARERY_RECRUIT, new List<int>());
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.SPEARERY_RECRUIT].Add(20);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.SPEARERY_RECRUIT].Add(20);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.SPEARERY_RECRUIT].Add(20);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.SPEARERY_RECRUIT].Add(5);

        costsTrainingUnitsInBuildings.Add(TrainingUnitsInBuildings.SWORD_SMITH_RECRUIT, new List<int>());
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.SWORD_SMITH_RECRUIT].Add(20);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.SWORD_SMITH_RECRUIT].Add(20);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.SWORD_SMITH_RECRUIT].Add(20);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.SWORD_SMITH_RECRUIT].Add(5);

        costsTrainingUnitsInBuildings.Add(TrainingUnitsInBuildings.STABLE_RECRUIT, new List<int>());
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.STABLE_RECRUIT].Add(20);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.STABLE_RECRUIT].Add(20);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.STABLE_RECRUIT].Add(20);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.STABLE_RECRUIT].Add(5);

        costsTrainingUnitsInBuildings.Add(TrainingUnitsInBuildings.TEMPLE_RECRUIT, new List<int>());
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.TEMPLE_RECRUIT].Add(20);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.TEMPLE_RECRUIT].Add(20);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.TEMPLE_RECRUIT].Add(20);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.TEMPLE_RECRUIT].Add(5);

        // Level 2 units
        costsTrainingUnitsInBuildings.Add(TrainingUnitsInBuildings.ARCHERY_SIMPLE_HORSE, new List<int>());
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.ARCHERY_SIMPLE_HORSE].Add(25);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.ARCHERY_SIMPLE_HORSE].Add(25);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.ARCHERY_SIMPLE_HORSE].Add(20);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.ARCHERY_SIMPLE_HORSE].Add(5);


        costsTrainingUnitsInBuildings.Add(TrainingUnitsInBuildings.STABLE_SWORDMAN, new List<int>());
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.STABLE_SWORDMAN].Add(25);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.STABLE_SWORDMAN].Add(25);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.STABLE_SWORDMAN].Add(20);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.STABLE_SWORDMAN].Add(5);

        costsTrainingUnitsInBuildings.Add(TrainingUnitsInBuildings.ARMORY_SPEARMAN, new List<int>());
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.ARMORY_SPEARMAN].Add(25);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.ARMORY_SPEARMAN].Add(25);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.ARMORY_SPEARMAN].Add(20);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.ARMORY_SPEARMAN].Add(5);

        costsTrainingUnitsInBuildings.Add(TrainingUnitsInBuildings.ARMORY_SWORDMAN, new List<int>());
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.ARMORY_SWORDMAN].Add(25);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.ARMORY_SWORDMAN].Add(25);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.ARMORY_SWORDMAN].Add(20);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.ARMORY_SWORDMAN].Add(5);

        costsTrainingUnitsInBuildings.Add(TrainingUnitsInBuildings.SWORD_SMITH_SIMPLE_HORSE, new List<int>());
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.SWORD_SMITH_SIMPLE_HORSE].Add(25);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.SWORD_SMITH_SIMPLE_HORSE].Add(25);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.SWORD_SMITH_SIMPLE_HORSE].Add(20);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.SWORD_SMITH_SIMPLE_HORSE].Add(5);

        costsTrainingUnitsInBuildings.Add(TrainingUnitsInBuildings.STABLE_ARCHER, new List<int>());
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.STABLE_ARCHER].Add(25);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.STABLE_ARCHER].Add(25);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.STABLE_ARCHER].Add(20);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.STABLE_ARCHER].Add(5);

        costsTrainingUnitsInBuildings.Add(TrainingUnitsInBuildings.SPEARERY_SIMPLE_HORSE, new List<int>());
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.SPEARERY_SIMPLE_HORSE].Add(25);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.SPEARERY_SIMPLE_HORSE].Add(25);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.SPEARERY_SIMPLE_HORSE].Add(20);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.SPEARERY_SIMPLE_HORSE].Add(5);

        costsTrainingUnitsInBuildings.Add(TrainingUnitsInBuildings.STABLE_SPEARMAN, new List<int>());
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.STABLE_SPEARMAN].Add(25);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.STABLE_SPEARMAN].Add(25);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.STABLE_SPEARMAN].Add(20);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.STABLE_SPEARMAN].Add(5);

        // Level 3 units
        costsTrainingUnitsInBuildings.Add(TrainingUnitsInBuildings.STABLE_SPEAR_KNIGHT, new List<int>());
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.STABLE_SPEAR_KNIGHT].Add(30);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.STABLE_SPEAR_KNIGHT].Add(30);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.STABLE_SPEAR_KNIGHT].Add(20);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.STABLE_SPEAR_KNIGHT].Add(5);

        costsTrainingUnitsInBuildings.Add(TrainingUnitsInBuildings.ARMORY_SWORD_HORSE, new List<int>());
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.ARMORY_SWORD_HORSE].Add(30);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.ARMORY_SWORD_HORSE].Add(30);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.ARMORY_SWORD_HORSE].Add(20);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.ARMORY_SWORD_HORSE].Add(5);

        costsTrainingUnitsInBuildings.Add(TrainingUnitsInBuildings.STABLE_SWORD_KNIGHT, new List<int>());
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.STABLE_SWORD_KNIGHT].Add(30);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.STABLE_SWORD_KNIGHT].Add(30);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.STABLE_SWORD_KNIGHT].Add(20);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.STABLE_SWORD_KNIGHT].Add(5);

        costsTrainingUnitsInBuildings.Add(TrainingUnitsInBuildings.ARMORY_SPEAR_HORSE, new List<int>());
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.ARMORY_SPEAR_HORSE].Add(30);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.ARMORY_SPEAR_HORSE].Add(30);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.ARMORY_SPEAR_HORSE].Add(20);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.ARMORY_SPEAR_HORSE].Add(5);

    }


}
