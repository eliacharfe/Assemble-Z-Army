using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    public static DamagePopup Create(Transform damagePopup, Vector3 position,
                                     int damageAmount, bool isCriticalHit)
    {
        Transform damagePopupTransform = Instantiate(damagePopup, position, Quaternion.identity);
        DamagePopup myDamagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        myDamagePopup.Setup(damageAmount, isCriticalHit);

        return myDamagePopup;
    }

    private const float DISAPPEAR_TIMER_MAX = 0.5f;
    private const float DISAPPEAR_TIMER_CRITICAL_MAX = 1f;

    private static int sortingOrder = 0;

    private TextMeshPro textMesh;
    private Color textColor;
    private float disappearTimer;

    private Color defaultColor;
    float disappearSpeed;

    private Vector3 moveVector;

    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
        defaultColor = textMesh.color;
    }

    public void Setup(int damageAmount, bool isCriticalHit)
    {
        if (damageAmount != 0)
        {
            textMesh.SetText(damageAmount.ToString());
        }
        else
        {
            textMesh.SetText("Dead");
        }

        if (!isCriticalHit)
        {
            textMesh.fontSize = 15f;
            textColor = defaultColor;
            disappearTimer = DISAPPEAR_TIMER_MAX;
        }
        else
        {
            textMesh.fontSize = 25f;
            textColor = Color.red;
            disappearTimer = DISAPPEAR_TIMER_CRITICAL_MAX;
        }

        textMesh.color = textColor;
        disappearSpeed = 4f;

        moveVector = new Vector3(0.7f, 1) * 20f;

        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;
    }


    private void Update()
    {
        // float moveYspeed = 20f;
        //transform.position += new Vector3(0, moveYspeed) * Time.deltaTime;
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 8f * Time.deltaTime;

        if (disappearTimer > DISAPPEAR_TIMER_MAX * 0.5)
        {
            float increaseScaleAmount = 1f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        }
        else
        {
            float increaseScaleAmount = 1f;
            transform.localScale -= Vector3.one * increaseScaleAmount * Time.deltaTime;
        }

        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0)
        {
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}