using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Macros;

public class ResourcesPlayer : MonoBehaviour
{
    private int wood, metal, gold, diamonds;

    public Macros.Resources id;

    private void Start()
    {
        wood = (int)Macros.Resources.WOOD;
        metal = (int)Macros.Resources.METAL;
        gold = (int)Macros.Resources.GOLD;
        diamonds = (int)Macros.Resources.DIAMONDS;

        //Debug.Log(wood);
    }

    public int GetWood()
    {
        return wood;
    }

    public int GetMetal()
    {
        return metal;
    }

    public int GetGold()
    {
        return gold;
    }

    public int GetDiamonds()
    {
        return diamonds;
    }

    public void DecreaseWood(int value)
    {
        wood -= value;
        Mathf.Clamp(wood, 0, (int)Macros.Resources.WOOD); // wood can be between 0 and 1800
        Debug.Log(wood);
    }

    public void DecreaseMetal(int value)
    {
        metal -= value;
        Mathf.Clamp(metal, 0, (int)Macros.Resources.METAL);
    }

    public void DecreaseGold(int value)
    {
        gold -= value;
        Mathf.Clamp(gold, 0, (int)Macros.Resources.GOLD);
    }

    public void DecreaseDiamonds(int value)
    {
        diamonds -= value;
        Mathf.Clamp(diamonds, 0, (int)Macros.Resources.DIAMONDS);
    }


    public void DecreaseResource(Macros.Resources resource, int value)
    {
        switch (resource)
        {
            case Macros.Resources.WOOD:
                {
                    wood -= value;
                    Mathf.Clamp(wood, 0, (int)Macros.Resources.WOOD);
                    break;
                }
            case Macros.Resources.METAL:
                {
                    metal -= value;
                    Mathf.Clamp(metal, 0, (int)Macros.Resources.METAL);
                    break;
                }
            case Macros.Resources.GOLD:
                {
                    gold -= value;
                    Mathf.Clamp(gold, 0, (int)Macros.Resources.GOLD);
                    break;
                }
            case Macros.Resources.DIAMONDS:
                {
                    diamonds -= value;
                    Mathf.Clamp(diamonds, 0, (int)Macros.Resources.DIAMONDS);
                    break;
                }
        }
    }

}
