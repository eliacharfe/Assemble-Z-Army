using UnityEngine;

namespace Macros
{

    class Scenes
    {
        public const string PREPERATION_SCENE = "Playground";
        public const string BATTLEFIELD_SCENE = "Battlefield";

    }

    class Constents
    {
        // Phases
        public const int PHASE_ONE = 1;
        public const int PHASE_TWO = 2;
        public const int PHASE_THREE = 3;
        public const int PHASE_FOUR = 3;

        // Workers
        public const int INITIAL_WORKERS_SIZE = 10;

        // Recruits
        public const int INITIAL_RECRUITS_SIZE = 30;

        public static Color[] teamColors = { Color.cyan, Color.yellow, Color.blue, Color.red };

    }

    public enum Buildings{
        NONE,
        ARCHERY_FIELD,
        CROSSBOW_FIELD,
        STABLE,
        SWORD_SMITH,
        SPEAR_SMITH,
        ARMORY,
        CAMP,
        TEMPLE,
        WORKSHOP
    }

    public enum Units
    {
        NONE,
        WORKER,
        RECRUIT,
        SWORDMAN,
        ARCHER,
        SPEARMAN,
        SWORD_KNIGHT,
        SPEAR_KNIGHT,
        SIMPLE_HORSE,
        ARCHER_HORSE,
        SPEAR_HORSE,
        SWORD_HORSE,
        SPEAR_HORSE_KNIGHT,
        SWORD_HORSE_KNIGHT,
        CROSSBOW,
        HEALER,
        CATAPULT,
        SCOUT
    }

    public enum Resources
    {
        NONE,
        WOOD = 1800,
        METAL = 1500,
        GOLD = 400,
        DIAMONDS = 100
    }

    public enum TrainingUnitsInBuildings
    {
        NONE,

        ARCHERY_RECRUIT,
        ARCHERY_SIMPLE_HORSE,

        ARMORY_SWORDMAN,
        ARMORY_SPEARMAN,
        ARMORY_SWORD_HORSE,
        ARMORY_SPEAR_HORSE,

        CROSSBOWERY_RECRUIT,

        SPEARERY_RECRUIT,
        SPEARERY_SIMPLE_HORSE,

        STABLE_RECRUIT,
        STABLE_SWORDMAN,
        STABLE_SPEARMAN,
        STABLE_ARCHER,
        STABLE_SWORD_KNIGHT,
        STABLE_SPEAR_KNIGHT,

        SWORD_SMITH_RECRUIT,
        SWORD_SMITH_SIMPLE_HORSE,

        TEMPLE_RECRUIT

    }



}

/*
    public static class Building
    {
        public const string
            ARCHERY_FIELD = "archery_field",
            CROSSBOW_FIELD = "crossbow_field",
            STABLE = "stable",
            SWORD_SMITH = "swordsmith",
            SPEAR_SMITH = "spearsmith",
            ARMORY = "armory",
            CAMP = "camp",
            TEMPLE = "temple",
            WORKSHOP = "workshop";
        
    }


    public static class Unitss
    {
        // Foot units
        public const string
             RECRUIT = "recruit",
             SWORDMAN = "swordman",
             ARCHER = "archer",
             SPEARMAN = "spearman";

        // Foot knight units.
        public const string
            SWORD_KNIGHT = "sword_knight",
            SPEAR_KNIGHT = "spear_knight";

        // Horses units.
        public const string
            SIMPLE_HORSE = "simple_horse",
            ARCHER_HORSE = "archer_horse",
            SPEAR_HORSE = "spear_horse",
            SWORD_HORSE = "sword_horse";

        // Knights horses units.
        public const string
            SPEAR_HORSE_KNIGHT = "spear_horse_knight",
            SWORD_HORSE_KNIGHT = "sword_horse_knight";

        // Uniqe units.
        public const string
            CROSSBOW = "crossbow",
            HEALER = "healer",
            CATAPULT = "catapult",
            SCOUT = "scout";

        public const string
            NONE = "";
    }

}
*/

