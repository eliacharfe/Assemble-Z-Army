using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Macros;

public class ResourcesPlayer : MonoBehaviour
{
    public Dictionary<Macros.Resources, int> resources = new Dictionary<Macros.Resources, int>();

    public Dictionary<TrainingUnitsInBuildings, List<int>> costsTrainingUnitsInBuildings = 
        new Dictionary<TrainingUnitsInBuildings, List<int>>();

    private void Start()
    {
        resources.Add(Macros.Resources.WOOD, (int)Macros.Resources.WOOD);
        resources.Add(Macros.Resources.METAL, (int)Macros.Resources.METAL);
        resources.Add(Macros.Resources.GOLD, (int)Macros.Resources.GOLD);
        resources.Add(Macros.Resources.DIAMONDS, (int)Macros.Resources.DIAMONDS);

        InitCostsTrainingUnitInBuilding();
    }

    public bool isHaveEnoughResources(List<int> costBuilding)
    {
        int i = 0;
        foreach (KeyValuePair<Macros.Resources, int> resource in resources)
        {
            if (costBuilding[i] > resource.Value)
            {
                return false;
            }
            i++;
        }
        return true;
    }

    public void DecreaseResource(List<int> costBuilding)
    {
        for (int i = 0; i < resources.Count; i++)
        {
            resources[getResource(i)] -= costBuilding[i];
        }
    }

    private Macros.Resources getResource(int i)
    {
        switch (i)
        {
            case 0:
                return Macros.Resources.WOOD;
            case 1:
                return Macros.Resources.METAL;
            case 2:
                return Macros.Resources.GOLD;
            case 3:
                return Macros.Resources.DIAMONDS;
        }
        return Macros.Resources.NONE;
    }

    public int getResource(Macros.Resources resourse)
    {
        switch (resourse)
        {
            case Macros.Resources.WOOD:
                return resources[Macros.Resources.WOOD];
            case Macros.Resources.METAL:
                return resources[Macros.Resources.METAL];
            case Macros.Resources.GOLD:
                return resources[Macros.Resources.GOLD];
            case Macros.Resources.DIAMONDS:
                return resources[Macros.Resources.DIAMONDS];
        }
        return 0;
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
        costsTrainingUnitsInBuildings.Add( TrainingUnitsInBuildings.ARCHERY_RECRUIT, new List<int>());
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.ARCHERY_RECRUIT].Add(15);
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.ARCHERY_RECRUIT].Add(5);
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.ARCHERY_RECRUIT].Add(1);
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.ARCHERY_RECRUIT].Add(0);

        costsTrainingUnitsInBuildings.Add( TrainingUnitsInBuildings.ARCHERY_SIMPLE_HORSE, new List<int>());
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.ARCHERY_SIMPLE_HORSE].Add(20);
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.ARCHERY_SIMPLE_HORSE].Add(7);
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.ARCHERY_SIMPLE_HORSE].Add(1);
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.ARCHERY_SIMPLE_HORSE].Add(1);

        costsTrainingUnitsInBuildings.Add( TrainingUnitsInBuildings.ARMORY_SPEAR_HORSE, new List<int>());
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.ARMORY_SPEAR_HORSE].Add(25);
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.ARMORY_SPEAR_HORSE].Add(10);
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.ARMORY_SPEAR_HORSE].Add(2);
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.ARMORY_SPEAR_HORSE].Add(1);

        costsTrainingUnitsInBuildings.Add( TrainingUnitsInBuildings.ARMORY_SPEARMAN, new List<int>());
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.ARMORY_SPEARMAN].Add(10);
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.ARMORY_SPEARMAN].Add(6);
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.ARMORY_SPEARMAN].Add(1);
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.ARMORY_SPEARMAN].Add(0);

        costsTrainingUnitsInBuildings.Add( TrainingUnitsInBuildings.ARMORY_SWORD_HORSE, new List<int>());
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.ARMORY_SWORD_HORSE].Add(25);
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.ARMORY_SWORD_HORSE].Add(12);
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.ARMORY_SWORD_HORSE].Add(2);
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.ARMORY_SWORD_HORSE].Add(1);

        costsTrainingUnitsInBuildings.Add( TrainingUnitsInBuildings.ARMORY_SWORDMAN, new List<int>());
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.ARMORY_SWORDMAN].Add(15);
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.ARMORY_SWORDMAN].Add(10);
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.ARMORY_SWORDMAN].Add(1);
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.ARMORY_SWORDMAN].Add(1);

        costsTrainingUnitsInBuildings.Add( TrainingUnitsInBuildings.CROSSBOWERY_RECRUIT, new List<int>());
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.CROSSBOWERY_RECRUIT].Add(13);
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.CROSSBOWERY_RECRUIT].Add(7);
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.CROSSBOWERY_RECRUIT].Add(1);
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.CROSSBOWERY_RECRUIT].Add(0);

        costsTrainingUnitsInBuildings.Add( TrainingUnitsInBuildings.SPEARERY_RECRUIT, new List<int>());
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.SPEARERY_RECRUIT].Add(10);
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.SPEARERY_RECRUIT].Add(5);
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.SPEARERY_RECRUIT].Add(0);
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.SPEARERY_RECRUIT].Add(0);

        costsTrainingUnitsInBuildings.Add( TrainingUnitsInBuildings.SPEARERY_SIMPLE_HORSE, new List<int>());
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.SPEARERY_SIMPLE_HORSE].Add(15);
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.SPEARERY_SIMPLE_HORSE].Add(12);
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.SPEARERY_SIMPLE_HORSE].Add(1);
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.SPEARERY_SIMPLE_HORSE].Add(0);

        costsTrainingUnitsInBuildings.Add( TrainingUnitsInBuildings.STABLE_ARCHER, new List<int>());
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.STABLE_ARCHER].Add(20);
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.STABLE_ARCHER].Add(10);
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.STABLE_ARCHER].Add(2);
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.STABLE_ARCHER].Add(1);

        costsTrainingUnitsInBuildings.Add( TrainingUnitsInBuildings.STABLE_RECRUIT, new List<int>());
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.STABLE_RECRUIT].Add(15);
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.STABLE_RECRUIT].Add(8);
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.STABLE_RECRUIT].Add(1);
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.STABLE_RECRUIT].Add(1);

        costsTrainingUnitsInBuildings.Add( TrainingUnitsInBuildings.STABLE_SPEAR_KNIGHT, new List<int>());
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.STABLE_SPEAR_KNIGHT].Add(25);
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.STABLE_SPEAR_KNIGHT].Add(15);
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.STABLE_SPEAR_KNIGHT].Add(3);
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.STABLE_SPEAR_KNIGHT].Add(2);

        costsTrainingUnitsInBuildings.Add( TrainingUnitsInBuildings.STABLE_SPEARMAN, new List<int>());
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.STABLE_SPEARMAN].Add(20);
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.STABLE_SPEARMAN].Add(13);
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.STABLE_SPEARMAN].Add(2);
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.STABLE_SPEARMAN].Add(1);

        costsTrainingUnitsInBuildings.Add( TrainingUnitsInBuildings.STABLE_SWORD_KNIGHT, new List<int>());
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.STABLE_SWORD_KNIGHT].Add(25);
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.STABLE_SWORD_KNIGHT].Add(15);
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.STABLE_SWORD_KNIGHT].Add(3);
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.STABLE_SWORD_KNIGHT].Add(2);

        costsTrainingUnitsInBuildings.Add( TrainingUnitsInBuildings.STABLE_SWORDMAN, new List<int>());
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.STABLE_SWORDMAN].Add(20);
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.STABLE_SWORDMAN].Add(10);
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.STABLE_SWORDMAN].Add(2);
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.STABLE_SWORDMAN].Add(1);

        costsTrainingUnitsInBuildings.Add( TrainingUnitsInBuildings.SWORD_SMITH_RECRUIT, new List<int>());
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.SWORD_SMITH_RECRUIT].Add(13);
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.SWORD_SMITH_RECRUIT].Add(7);
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.SWORD_SMITH_RECRUIT].Add(1);
        costsTrainingUnitsInBuildings[ TrainingUnitsInBuildings.SWORD_SMITH_RECRUIT].Add(0);

        costsTrainingUnitsInBuildings.Add(TrainingUnitsInBuildings.SWORD_SMITH_SIMPLE_HORSE, new List<int>());
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.SWORD_SMITH_SIMPLE_HORSE].Add(17);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.SWORD_SMITH_SIMPLE_HORSE].Add(11);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.SWORD_SMITH_SIMPLE_HORSE].Add(2);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.SWORD_SMITH_SIMPLE_HORSE].Add(0);

        costsTrainingUnitsInBuildings.Add(TrainingUnitsInBuildings.TEMPLE_RECRUIT, new List<int>());
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.TEMPLE_RECRUIT].Add(10);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.TEMPLE_RECRUIT].Add(4);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.TEMPLE_RECRUIT].Add(0);
        costsTrainingUnitsInBuildings[TrainingUnitsInBuildings.TEMPLE_RECRUIT].Add(2);
    }
}
