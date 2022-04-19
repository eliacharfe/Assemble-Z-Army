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

    RectTransform canvas;

    private void Awake()
    {
        instance = this;

        backgroundRectTransform = transform.Find("background").GetComponent<RectTransform>();
        tooltipText = transform.Find("text").GetComponent<Text>();


        HideTooltip();
        //ShowTooltip("some text...");
    }

    private void Update()
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(),
         new Vector2(Input.mousePosition.x + 60f,Input.mousePosition.y + 60f) , uiCamera , out localPoint);

         transform.localPosition = localPoint;
    }

    private void ShowTooltip(string tooltipString)
    {
        gameObject.SetActive(true);

        tooltipText.text = tooltipString;
        float textPaddingSize = 4f;
        Vector2 backgroundSize = new Vector2(tooltipText.preferredWidth + textPaddingSize * 5f,
         tooltipText.preferredHeight + textPaddingSize * 5f);

        backgroundRectTransform.sizeDelta = backgroundSize;
    }

    private void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    public static void ShowTooltip_Static(string tooltipString)
    {
        instance.ShowTooltip(tooltipString);
    }

     public static void HideTooltip_Static()
    {
        instance.HideTooltip();
    }
}
