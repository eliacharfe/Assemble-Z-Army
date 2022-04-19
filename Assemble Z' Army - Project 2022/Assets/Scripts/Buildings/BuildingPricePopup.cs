using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildingPricePopup : MonoBehaviour
{

    public static BuildingPricePopup Create(Transform buildingPopup, Vector3 position, List<int> prices)
    {
        Transform pricePopupTransform = Instantiate(buildingPopup, position, Quaternion.identity);
        BuildingPricePopup buildingPricePopup = pricePopupTransform.GetComponent<BuildingPricePopup>();
        buildingPricePopup.Setup(prices);

        return buildingPricePopup;
    }

    private TextMeshPro textMesh;
    private Color textColor;
    private float disappearTimer;

    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    public void Setup(List<int> prices)
    {
        textMesh.SetText("Wood: " + prices[0].ToString() + '\n' +
                         "Metal: " + prices[1].ToString() + '\n' +
                         "Gold: " + prices[2].ToString() + '\n' +
                         "Diamonds: " + prices[3].ToString() + '\n');

        textColor = textMesh.color;
        disappearTimer = 4f;
    }


    private void Update()
    {
        float moveYspeed = -2f;
        transform.position += new Vector3(0, moveYspeed) * Time.deltaTime;

        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0)
        {
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }

}
