using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Macros;

public class UnitsFactory
{
    public GameObject
        swordman,
        spearman,
        archer,
        swordKnight,
        spearKnight,
        swordHorse,
        spearHorse,
        archerHorse,
        swordHorseKnight,
        spearHorseKnight,
        crossbow,
        healer,
        catapult,
        scout;


    GameObject GetBuildingOutputUnit(string buildingTag,string unitTag)
    {
        switch(buildingTag)
        {
            case Macros.Building.SWORD_SMITH:
                return GetSwordmanUnit(unitTag);

            case Macros.Building.SPEAR_SMITH:
                return GetSpearUnit(unitTag);

            case Macros.Building.ARCHERY_FIELD:
                return GetArcherUnit(unitTag);

            case Macros.Building.ARMORY:
                return GetArmoredUnit(unitTag);

            case Macros.Building.STABLE:
                return GetStableUnit(unitTag);

            case Macros.Building.CROSSBOW_FIELD:
                return GetCrossbowUnit(unitTag);

            case Macros.Building.WORKSHOP:
                return GetCatapultUnit(unitTag);

            case Macros.Building.TEMPLE:
                return GetHealerUnit(unitTag);

            case Macros.Building.CAMP:
                return GetScoutUnit(unitTag);
        }
        return null;
    }

    private bool IsRecruit(string unitTag){
        return unitTag == Units.RECRUIT;
    }
    GameObject GetHealerUnit(string unitTag){
        return IsRecruit(unitTag) ? healer : null;
    }

    GameObject GetCrossbowUnit(string unitTag){
        return IsRecruit(unitTag) ? crossbow : null;
    }


    GameObject GetCatapultUnit(string unitTag){
        return IsRecruit(unitTag) ? catapult : null;
    }


    GameObject GetScoutUnit(string unitTag){
        return IsRecruit(unitTag) ? scout : null;
    }


    GameObject GetSwordmanUnit(string unitTag){
        switch (unitTag)
        {
            case Units.SIMPLE_HORSE:
                return swordHorse;
            case Units.RECRUIT:
                return swordman;
        }

        return null;
    }

    GameObject GetSpearUnit(string unitTag){
        switch (unitTag)
        {
            case Units.SIMPLE_HORSE:
                return spearHorse;
            case Units.RECRUIT:
                return spearman;
        }

        return null;
    }

    GameObject GetArcherUnit(string unitTag){
        switch (unitTag)
        {
            case Units.SIMPLE_HORSE:
                return archerHorse;
            case Units.RECRUIT:
                return archer;
        }

        return null;
    }

    GameObject GetArmoredUnit(string unitTag){
        switch (unitTag)
        {
            case Units.SWORDMAN:
                return swordKnight;
            case Units.SPEARMAN:
                return spearKnight;
            case Units.SWORD_HORSE:
                return swordHorseKnight;
            case Units.SPEAR_HORSE:
                return spearHorseKnight;
        }

        return null;
    }


    GameObject GetStableUnit(string unitTag){
        switch (unitTag)
        {
            case Units.SWORDMAN:
                return swordHorse;
            case Units.SPEARMAN:
                return spearHorse;
            case Units.ARCHER:
                return archerHorse;
        }

        return null;
    }

}