using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesCostTraining : MonoBehaviour
{
    private static ResourcesCostTraining instance;

    private Dictionary<Macros.TrainingUnitsInBuildings, List<int>> costsTrainingUnitsInBuildings =
    new Dictionary<Macros.TrainingUnitsInBuildings, List<int>>();

    private void Awake()
    {
        instance = this;
        InitCostsTrainingUnitInBuilding();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static List<int> getCostsTrainingUnitInBuilding_Static(Macros.TrainingUnitsInBuildings id)
    {
        return instance.getCostsTrainingUnitInBuilding(id);
    }

    public List<int> getCostsTrainingUnitInBuilding(Macros.TrainingUnitsInBuildings id)
    {
        Debug.Log(id);
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
