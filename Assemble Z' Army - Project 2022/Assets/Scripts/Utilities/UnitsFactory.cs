using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Macros;

// The class return units according the building and unit given.
// The comprantion between units is mad according the given tags.
// TODO compare types by scripts object.
public class UnitsFactory : MonoBehaviour
{
    public Unit
        swordman,
        spearman,
        archer,
        simpleHorse,
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
        scout,
        worker,
        recruit;




    public Unit GetBuildingOutputUnit(Buildings buildingId, Units unitId)
    {
        switch(buildingId)
        {
            case Buildings.SWORD_SMITH:
                return GetSwordmanUnit(unitId);

            case Buildings.SPEAR_SMITH:
                return GetSpearUnit(unitId);

            case Buildings.ARCHERY_FIELD:
                return GetArcherUnit(unitId);

            case Buildings.ARMORY:
                return GetArmoredUnit(unitId);

            case Buildings.STABLE:
                return GetStableUnit(unitId);

            case Buildings.CROSSBOW_FIELD:
                return GetCrossbowUnit(unitId);

            case Buildings.WORKSHOP:
                return GetCatapultUnit(unitId);

            case Buildings.TEMPLE:
                return GetHealerUnit(unitId);

            case Buildings.CAMP:
                return GetScoutUnit(unitId);
        }
        return null;
    }


    public Unit GetUnitPrefab(Units unitId)
    {

        print("Unit id recived "+unitId);
        switch (unitId)
        {
            case Units.WORKER:
                return worker;
           
            case Units.RECRUIT:
                return recruit;

            case Units.ARCHER:
                return archer;
            case Units.CROSSBOW:
                return crossbow;

            case Units.SWORDMAN:
                return swordman;
            case Units.SWORD_KNIGHT:
                return swordKnight;

            case Units.SPEARMAN:
                return spearman;
            case Units.SPEAR_KNIGHT:
                return spearKnight;

            case Units.SIMPLE_HORSE:
                return simpleHorse;
            case Units.SWORD_HORSE:
                return swordHorse;
            case Units.SWORD_HORSE_KNIGHT:
                return swordHorseKnight;
            case Units.SPEAR_HORSE:
                return spearHorse;
            case Units.SPEAR_HORSE_KNIGHT:
                return spearHorseKnight;
            case Units.ARCHER_HORSE:
                return archerHorse;

            case Units.SCOUT:
                return scout;
            case Units.HEALER:
                return healer;
        }

        return null;
    }


    private bool IsRecruit(Units unitId){
        return unitId == Units.RECRUIT;
    }
    private Unit GetHealerUnit(Units unitId){
        return IsRecruit(unitId) ? healer : null;
    }

    private Unit GetCrossbowUnit(Units unitId){
        return IsRecruit(unitId) ? crossbow : null;
    }


    private Unit GetCatapultUnit(Units unitId){
        return IsRecruit(unitId) ? catapult : null;
    }


    private Unit GetScoutUnit(Units unitId){
        return IsRecruit(unitId) ? scout : null;
    }


    private Unit GetSwordmanUnit(Units unitId){
        switch (unitId)
        {
            case Units.SIMPLE_HORSE:
                return swordHorse;
            case Units.RECRUIT:
                return swordman;
        }

        return null;
    }

    private Unit GetSpearUnit(Units unitId){
        switch (unitId)
        {
            case Units.SIMPLE_HORSE:
                return spearHorse;
            case Units.RECRUIT:
                return spearman;
        }

        return null;
    }

    private Unit GetArcherUnit(Units unitId){
        switch (unitId)
        {
            case Units.SIMPLE_HORSE:
                return archerHorse;
            case Units.RECRUIT:
                return archer;
        }

        return null;
    }

    private Unit GetArmoredUnit(Units unitId){
        switch (unitId)
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


    private Unit GetStableUnit(Units unitId){
        switch (unitId)
        {
            case Units.RECRUIT:
                return simpleHorse;
            case Units.SWORDMAN:
                return swordHorse;
            case Units.SPEARMAN:
                return spearHorse;
            case Units.ARCHER:
                return archerHorse;
            case Units.SWORD_KNIGHT:
                return swordHorseKnight;
            case Units.SPEAR_KNIGHT:
                return spearHorseKnight;
        }

        return null;
    }

}