using UnityEngine;

// Namespace which organize most of the consts used
namespace Macros
{
    struct Scenes
    {
        public const string PREPERATION_SCENE = "Playground";
        public const string BATTLEFIELD_SCENE = "Battlefield";
        public const string MENU_SCENE = "Menu";
    }


    struct Phases
    {
        public const string PREPERATION_PHASE_NAME = "Preperation Phase";
        public const string BUILDING_PHASE_NAME = "";
        public const string BATTLEFIELD_PHASE_NAME = "Battle Phase";
    }

    struct Constents
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
        WOOD = 1650,
        METAL = 1300,
        GOLD = 500,
        DIAMONDS = 120
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

    struct GameText
    {
        public const string instructions = "Intro: " + "\n"+
            "Welcome to Assemble Z’ Army real-time strategy multiplayer game." +"\n"+
            "The main goal is to gather your army and defeat all your rival’s units. " +"\n"+
            "To do so, you must build training buildings, which qualify your units for the war to come.  " +"\n"+
            "But beware, you have limited time and resources, so plan carefully." +"\n"+
            "" +"\n"+
            "_________________________________________"+
            "Controllers: " + "\n"+ 
            "Basics controls: " + "\n" + 
            "Mouse Left-click – Selecting units/buildings" + "\n"+
            "Mouse Right-click – Command selected unit build/move/attack" +"\n"+
            "Mouse wheel – Zoom in/out" +"\n"
            + "\n" +
            "Map controls:" +"\n"+
            "WASD-keys/Arrow-keys/Mouse direction – Move the world camera in a specific direction" +"\n"
            + "\n" +
            "_________________________________________" +
            "Instructions:" + "\n" + 
            "The game is divided into three main phases:" + "\n" +
            "1.Building phase  " +"\n"+
            "2.Preparation phase " +"\n"+
            "3.Battlefield phase " +"\n"+
            "Each phase has a specific purpose which aims for the main goal, to defeat all players on the battlefield." +"\n"
            + "\n" +
            "Building phase:" + "\n" + " Five workers will come to build the wanted building according to your choice and resources." + "\n"+
            "In this phase, you will have 1 minute to build all the planned buildings." +"\n"+
            "Pay Attention: Buildings that won’t be fully constructed will be unusable and won’t allow you to recruit in the next phase." +"\n"
            + "\n" +
            "Preparation phase:" + "\n" + "After a hard-worked period, the workers will go home to their families, and the fresh recruits will arrive. " + "\n"+
            "You will receive about 15 recruits which each can be sent to the relevant building you had built, and after an amount of time, the unit will be qualified as a certain type of unit." +"\n"
             +"\n"+
            "There are 5 main types of units in the game: " +"\n"+
            "Sword – Medium attack some defense skill " +"\n"+
            "Spear – Has bonus attack against horses " +"\n"+
            "Archer – Can attack from a far distance " +"\n"+
            "Sorcerer – Can use mana for special spells " +"\n"+
            "Horse – Have speed advantage " +"\n"+
            "Full units’ details will be shown at the end of the instructions. " +"\n"+
            "Pay Attention: Units that are middle of recruitment will be unqualified for war and be left behind." +"\n"+
            "" +"\n"+
            "Battlefield phase: " +"\n"+
            "After all, preparations made the time for war has come." +"\n"+
            "Be ready to send your units to attack the enemy. " +"\n"+
            "The winner will be the player who has the last units standing. " +"\n"+
            "" +"\n"+
            "________________________________________" +
            "Units’ stats: " +"\n"+
            "Level1: " +"\n"+
            "SwordMan: a-15 d-5 s-10 sa-1 rd-5 " +"\n"+
            "SpearMan: a-10 d-5 s-10 sa-1.5 rd-5 " +"\n"+
            "Archer:   a-10 d-0 s-10 sa-1 rd-30 " +"\n"+
            "Healer:   a-5 d-0 s-10 sa-1 rd-5 " +"\n"+
            "Crossbow: a-15 d-0 s-10 sa-1 rd-18 " +"\n"+
            "SimpleHorse: a-10 d-5 s-15 sa-2 rd-5 " +"\n"+
            "Level2: " +"\n"+
            "SwordHorse: a-10 d-0 s-10 sa-1 rd-10 " +"\n"+
            "SpearHorse: a-10 d-0 s-10 sa-1 rd-10 " +"\n"+
            "ArcherHorse:a-10 d-0 s-10 sa-1 rd-10 " +"\n"+
            "SpearKnight:a-10 d-0 s-10 sa-1 rd-10 " +"\n"+
            "SwordKnight:a-10 d-0 s-10 sa-1 rd-10 " +"\n"+
            "Level3: " +"\n"+
            "SwordHorseKnight: a-10 d-0 s-10 sa-1 rd-10 " +"\n"+
            "SpearHorseKnight: a-10 d-0 s-10 sa-1 rd-10"+"\n";
    }
}