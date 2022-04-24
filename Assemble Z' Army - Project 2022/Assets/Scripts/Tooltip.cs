using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
     private static Tooltip instance;

    [SerializeField] private Camera uiCamera;

    private Text tooltipText;
    private RectTransform backgroundRectTransform;

    private Text tooltipTextName;
    private RectTransform backgroundNameRectTransform;

    private void Awake()
    {
        instance = this;

        backgroundRectTransform = transform.Find("background").GetComponent<RectTransform>();
        tooltipText = transform.Find("text").GetComponent<Text>();

        backgroundNameRectTransform = transform.Find("backgroundNameBuilding").GetComponent<RectTransform>();
        tooltipTextName = backgroundNameRectTransform.transform.Find("textNameBuilding").GetComponent<Text>();

        HideTooltip();
    }

    private void Update()
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(),
         new Vector2(Input.mousePosition.x + 60f,Input.mousePosition.y + 60f) , uiCamera , out localPoint);

         transform.localPosition = localPoint;
    }

    private void ShowTooltip(string tooltipString, string nameBuilding)
    {
        gameObject.SetActive(true);

        tooltipText.text = tooltipString;
        float textPaddingSize = 4f;
        Vector2 backgroundSize = new Vector2(tooltipText.preferredWidth + textPaddingSize * 5f,
         tooltipText.preferredHeight + textPaddingSize * 5f);

        tooltipTextName.text = nameBuilding;
        Vector2 backgroundSizeName = new Vector2(tooltipTextName.preferredWidth + textPaddingSize * 5f,
         tooltipTextName.preferredHeight + textPaddingSize * 5f);

        backgroundRectTransform.sizeDelta = backgroundSize;
        backgroundNameRectTransform.sizeDelta = backgroundSizeName;
        backgroundNameRectTransform.position = new Vector3(tooltipText.GetComponent<RectTransform>().position.x + 40f,
        backgroundNameRectTransform.position.y, backgroundNameRectTransform.position.z);
    }

    private void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    public static void ShowTooltip_Static(string tooltipString, string nameBuilding)
    {
        instance.ShowTooltip(tooltipString, nameBuilding);
    }

    public static void HideTooltip_Static()
    {
        instance.HideTooltip();
    }
}
