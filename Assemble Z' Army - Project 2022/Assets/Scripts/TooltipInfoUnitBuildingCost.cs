using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Macros;
using System;

public class TooltipInfoUnitBuildingCost : MonoBehaviour
{
    private static TooltipInfoUnitBuildingCost instance;

    private const float SIZE_IMAGE = 40f;
    private const string IMAGE = "Image";
    private const string TEXT = "Text";
    private const string EMPTY = "";

    [SerializeField] private Camera uiCamera;

    [SerializeField] private Sprite archerImage;
    [SerializeField] private Sprite archerHorseImage;
    [SerializeField] private Sprite crossbowImage;
    [SerializeField] private Sprite healerImage;
    [SerializeField] private Sprite recruitImage;
    [SerializeField] private Sprite simpleHorseImage;
    [SerializeField] private Sprite spearmanImage;
    [SerializeField] private Sprite spearHorseImage;
    [SerializeField] private Sprite spearKnightImage;
    [SerializeField] private Sprite spearKnightHorseImage;
    [SerializeField] private Sprite swordmanImage;
    [SerializeField] private Sprite swordHosreImage;
    [SerializeField] private Sprite swordKnightImage;
    [SerializeField] private Sprite swordKnightHorseImage;
    [SerializeField] private Sprite workerImage;
    [SerializeField] private Sprite arrowImage;

    private RectTransform background;
    private Text textInfo;

    private Vector3 startPos;

    ResourcesPlayer resourcesPlayer = null;

    private void Awake()
    {
        instance = this;

        background = transform.Find("BackgroundImage").GetComponent<RectTransform>();
        textInfo = transform.Find("TextInfo").GetComponent<Text>();

        startPos = transform.position;

        resourcesPlayer = FindObjectOfType<ResourcesPlayer>();

        HideTooltip();
    }

    private void ShowTooltip(List<Units> units, Macros.Buildings idBuilding)
    {
        gameObject.SetActive(true);
        background.gameObject.SetActive(true);

        Reset();

        CreateNewInfoTooltip(units, idBuilding);
    }

    private void Reset()
    {
        textInfo.text = EMPTY;

        foreach (Transform child in transform)
        {
            if (child.name == IMAGE || child.name == TEXT)
            {
                Destroy(child.gameObject);
            }
        }
    }

    private void CreateNewInfoTooltip(List<Units> units, Buildings idBuilding)
    {
        int index = 1;
        float heightSize = 0f;
        float textPaddingSize = 5f;
        List<Macros.Units> idsUnits = new List<Units>();

        foreach (Units unit in units)
        {
            List<int> Costs = new List<int>();
            Costs = resourcesPlayer.GetCostsTrainingUnitInBuildingByIds(unit, idBuilding);

            if (Costs != null && !idsUnits.Contains(unit))
            {
                idsUnits.Add(unit);

                string costsTextString =   "W-" + Costs[0].ToString() +
                                         ", M-" + Costs[1].ToString() +
                                         ", G-" + Costs[2].ToString() +
                                         ", D-" + Costs[3].ToString(); /* ToStringUnit(unit) + ": "*/

                CreateText(costsTextString, new Vector3(startPos.x + SIZE_IMAGE * 4, startPos.y + (index * SIZE_IMAGE) - 10f, 0));

                Image imageBefore = CreateImage(new Vector3(startPos.x + SIZE_IMAGE, startPos.y + (index * SIZE_IMAGE), 0),
                                                SIZE_IMAGE);
                imageBefore.sprite = GetSpriteImage(unit);

                Image imageArrow = CreateImage(new Vector3(startPos.x + SIZE_IMAGE * 2, startPos.y + (index * SIZE_IMAGE), 0),
                                               SIZE_IMAGE / 2);
                imageArrow.sprite = arrowImage;

                Image imageAfter = CreateImage(new Vector3(startPos.x + SIZE_IMAGE * 3, startPos.y + (index * SIZE_IMAGE), 0),
                                               SIZE_IMAGE);
                imageAfter.sprite = GetSpriteImprovementUnit(unit, idBuilding);

                heightSize += (index * SIZE_IMAGE);
                ++index;
            }
        }


        if (textInfo.text == EMPTY)
        {
            background.gameObject.SetActive(false);
            return;
        }

        Vector2 backgroundSize = new Vector2(textInfo.preferredWidth + textPaddingSize * 10f + SIZE_IMAGE * 7,
         360f);
        //  heightSize + textPaddingSize * 10f);
        background.sizeDelta = backgroundSize;

        textInfo.text = EMPTY;
    }

    private void CreateText(string costsTextString, Vector3 position)
    {
        GameObject newText = Instantiate(textInfo.gameObject);
        newText.transform.position = position;
        newText.transform.parent = transform;
        newText.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        newText.GetComponent<RectTransform>().SetParent(gameObject.transform);
        newText.name = TEXT;

        newText.GetComponent<Text>().text = costsTextString;
        textInfo.text += costsTextString + '\n';
    }

    private Image CreateImage(Vector3 position, float sizeImage)
    {
        GameObject newObjectImage = new GameObject();
        Image image = newObjectImage.AddComponent<Image>();

        image.rectTransform.sizeDelta = new Vector2(sizeImage, sizeImage);
        newObjectImage.GetComponent<RectTransform>().SetParent(transform);
        newObjectImage.transform.position = position;
        //newObjectImage.transform.localScale = new Vector3(1f, 1f, 1f);
        newObjectImage.name = IMAGE;
        newObjectImage.SetActive(true);

        return image;
    }

    private void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    public static void ShowTooltip_Static(List<Units> units, Macros.Buildings idBuilding)
    {
        instance.ShowTooltip(units, idBuilding);
    }

    public static void HideTooltip_Static()
    {
        instance.HideTooltip();
    }

    private Sprite GetSpriteImage(Units unit)
    {
        switch (unit)
        {
            case Macros.Units.ARCHER: return archerImage;
            case Macros.Units.ARCHER_HORSE: return archerHorseImage;
            case Macros.Units.CROSSBOW: return crossbowImage;
            case Macros.Units.HEALER: return healerImage;
            case Macros.Units.RECRUIT: return recruitImage;
            case Macros.Units.SIMPLE_HORSE: return simpleHorseImage;
            case Macros.Units.SPEAR_HORSE: return spearHorseImage;
            case Macros.Units.SPEAR_HORSE_KNIGHT: return spearKnightHorseImage;
            case Macros.Units.SPEAR_KNIGHT: return spearKnightImage;
            case Macros.Units.SPEARMAN: return spearmanImage;
            case Macros.Units.SWORD_HORSE: return swordHosreImage;
            case Macros.Units.SWORD_HORSE_KNIGHT: return swordKnightHorseImage;
            case Macros.Units.SWORD_KNIGHT: return swordKnightImage;
            case Macros.Units.SWORDMAN: return swordmanImage;
            case Macros.Units.WORKER: return workerImage;
        };

        return null;
    }

    private Sprite GetSpriteImprovementUnit(Units unit, Buildings idBuilding)
    {
        if (unit == Units.RECRUIT && idBuilding == Buildings.TEMPLE)
            return healerImage;
        else if (unit == Units.RECRUIT && idBuilding == Buildings.STABLE)
            return simpleHorseImage;
        else if (unit == Units.RECRUIT && idBuilding == Buildings.ARCHERY_FIELD)
            return archerImage;
        else if (unit == Units.RECRUIT && idBuilding == Buildings.SWORD_SMITH)
            return swordmanImage;
        else if (unit == Units.RECRUIT && idBuilding == Buildings.SPEAR_SMITH)
            return spearmanImage;
        else if (unit == Units.RECRUIT && idBuilding == Buildings.CROSSBOW_FIELD)
            return crossbowImage;

        else if (unit == Units.SIMPLE_HORSE && idBuilding == Buildings.SWORD_SMITH
                 || unit == Units.SWORDMAN && idBuilding == Buildings.STABLE)
            return swordHosreImage;
        else if (unit == Units.SIMPLE_HORSE && idBuilding == Buildings.SPEAR_SMITH
              || unit == Units.SPEARMAN && idBuilding == Buildings.STABLE)
            return spearHorseImage;
        else if (unit == Units.SIMPLE_HORSE && idBuilding == Buildings.ARCHERY_FIELD
                 || unit == Units.ARCHER && idBuilding == Buildings.STABLE)
            return archerHorseImage;

        else if (unit == Units.SWORDMAN && idBuilding == Buildings.ARMORY)
            return swordKnightImage;
        else if (unit == Units.SPEARMAN && idBuilding == Buildings.ARMORY)
            return spearKnightImage;
        else if (unit == Units.SWORD_HORSE && idBuilding == Buildings.ARMORY
                 || unit == Units.SWORD_KNIGHT && idBuilding == Buildings.STABLE)
            return swordKnightHorseImage;
        else if (unit == Units.SPEAR_HORSE && idBuilding == Buildings.ARMORY
                || unit == Units.SPEAR_KNIGHT && idBuilding == Buildings.STABLE)
            return spearKnightHorseImage;

        return null;
    }


    // private string ToStringUnit(Units unit)
    // {
    //     switch (unit)
    //     {
    //         case Macros.Units.ARCHER: return "Archer";
    //         case Macros.Units.ARCHER_HORSE: return "Archer Horseman";
    //         case Macros.Units.CROSSBOW: return "Crossbow";
    //         case Macros.Units.HEALER: return "Healer";
    //         case Macros.Units.RECRUIT: return "Recruit";
    //         case Macros.Units.SIMPLE_HORSE: return "Simple Horse";
    //         case Macros.Units.SPEAR_HORSE: return "Spear Horsman";
    //         case Macros.Units.SPEAR_HORSE_KNIGHT: return "Spear Knight Horsman";
    //         case Macros.Units.SPEAR_KNIGHT: return "Spear Knight";
    //         case Macros.Units.SPEARMAN: return "Spearman";
    //         case Macros.Units.SWORD_HORSE: return "Sword Horseman";
    //         case Macros.Units.SWORD_HORSE_KNIGHT: return "Sword Knight Horseman";
    //         case Macros.Units.SWORD_KNIGHT: return "Sword Knight";
    //         case Macros.Units.SWORDMAN: return "Swordman";
    //         case Macros.Units.WORKER: return "Worker";
    //     };

    //     return "None";
    // }

}
