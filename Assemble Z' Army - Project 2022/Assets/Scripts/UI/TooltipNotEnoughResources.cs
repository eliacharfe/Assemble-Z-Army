using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipNotEnoughResources : MonoBehaviour
{
    private static TooltipNotEnoughResources instance;

    [SerializeField] private Camera uiCamera;

    private Text tooltipText;
    private RectTransform backgroundRectTransform;

    private Vector3 startPos;

    private void Awake()
    {
        instance = this;

        backgroundRectTransform = transform.Find("BackgroundImage").GetComponent<RectTransform>();
        tooltipText = backgroundRectTransform.Find("TextInfo").GetComponent<Text>();

        startPos = transform.position;

        HideTooltip();
    }

    private void ShowTooltip(string tooltipString, string name)
    {
        gameObject.SetActive(true);

        tooltipText.text = tooltipString + name;
        float textPaddingSize = 4f;
        Vector2 backgroundSize = new Vector2(tooltipText.preferredWidth + textPaddingSize * 10f,
         tooltipText.preferredHeight + textPaddingSize * 5f);

        backgroundRectTransform.sizeDelta = backgroundSize;

        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        for (float f = 1f; f >= -0.05; f -= 0.01f)
        {
            yield return new WaitForSeconds(0.01f);
        }
        HideTooltip();
    }

    private void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    public static void ShowTooltip_Static(string tooltipString, string name)
    {
        instance.ShowTooltip(tooltipString, name);
    }

    public static void HideTooltip_Static()
    {
        instance.HideTooltip();
    }


    //     private IEnumerator FadeIn(RectTransform gameObj)
    //     {
    //        Image spRenderer = gameObj.GetComponent<Image>();
    //         for (float f = 0f; f <= 1f; f += 0.05f)
    //         {
    //             Color color = spRenderer.material.color;
    //             color.a = f;
    //             spRenderer.material.color = color;
    //             yield return new WaitForSeconds(0.05f);
    //         }
    //         HideTooltip();
    //     }

}
