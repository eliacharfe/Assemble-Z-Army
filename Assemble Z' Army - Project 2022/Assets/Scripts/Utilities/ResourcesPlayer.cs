using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Macros;

public class ResourcesPlayer : MonoBehaviour
{
    private static readonly Dictionary<Macros.Resources, int> dictionary = new Dictionary<Macros.Resources, int>();
    Dictionary<Macros.Resources, int> resources = dictionary;

    public Macros.Resources id;

    private void Start()
    {
        resources.Add(Macros.Resources.WOOD, (int)Macros.Resources.WOOD);
        resources.Add(Macros.Resources.METAL, (int)Macros.Resources.METAL);
        resources.Add(Macros.Resources.GOLD, (int)Macros.Resources.GOLD);
        resources.Add(Macros.Resources.DIAMONDS, (int)Macros.Resources.DIAMONDS);
    }
    //--------------------
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
    //--------------------------
    public void DecreaseResource(List<int> costBuilding)
    {
        for (int i = 0; i < resources.Count; i++)
        {
            resources[getResource(i)] -= costBuilding[i];
        }
    }
    //-----------------------------------
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
    //------------------------------
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
}









// foreach (var resource in resources.Keys)
// {
//     resources[resource] -= costBuilding[i];
//     Mathf.Clamp(resources[resource], 0, (int)(resource));
// }


// switch (resource)
// {
//     case Macros.Resources.WOOD:
//         {
//             wood -= value;
//             Mathf.Clamp(wood, 0, (int)Macros.Resources.WOOD);
//             break;
//         }
//     case Macros.Resources.METAL:
//         {
//             metal -= value;
//             Mathf.Clamp(metal, 0, (int)Macros.Resources.METAL);
//             break;
//         }
//     case Macros.Resources.GOLD:
//         {
//             gold -= value;
//             Mathf.Clamp(gold, 0, (int)Macros.Resources.GOLD);
//             break;
//         }
//     case Macros.Resources.DIAMONDS:
//         {
//             diamonds -= value;
//             Mathf.Clamp(diamonds, 0, (int)Macros.Resources.DIAMONDS);
//             break;
//         }
//



//  public int GetWood()
//     {
//         return wood;
//     }

//     public int GetMetal()
//     {
//         return metal;
//     }

//     public int GetGold()
//     {
//         return gold;
//     }

//     public int GetDiamonds()
//     {
//         return diamonds;
//     }

//     public void DecreaseWood(int value)
//     {
//         wood -= value;
//         Mathf.Clamp(wood, 0, (int)Macros.Resources.WOOD); // wood can be between 0 and 1800
//         Debug.Log(wood);
//     }

//     public void DecreaseMetal(int value)
//     {
//         metal -= value;
//         Mathf.Clamp(metal, 0, (int)Macros.Resources.METAL);
//     }

//     public void DecreaseGold(int value)
//     {
//         gold -= value;
//         Mathf.Clamp(gold, 0, (int)Macros.Resources.GOLD);
//     }

//     public void DecreaseDiamonds(int value)
//     {
//         diamonds -= value;
//         Mathf.Clamp(diamonds, 0, (int)Macros.Resources.DIAMONDS);