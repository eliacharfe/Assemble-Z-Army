using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Mirror;

public class DamagePopup : NetworkBehaviour
{
    public static DamagePopup Create(Transform damagePopup, Vector3 position,
                                     int damageAmount, float localScale, bool isCriticalHit)
    {
        GameObject damagePopupTransform = Instantiate(damagePopup.gameObject, position, Quaternion.identity) ;
        DamagePopup myDamagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        myDamagePopup.Setup(damageAmount,  localScale, isCriticalHit);
        return myDamagePopup;
    }

    private const float DISAPPEAR_TIMER_MAX = 0.5f;
    private const float DISAPPEAR_TIMER_CRITICAL_MAX = 1f;
    private const float REGULAR_FONT_SIZE = 15;
    private const float CRITICAL_FONT_SIZE = 20;
    private const string DEAD = "Dead";

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

    public void Setup(int damageAmount, float localScale, bool isCriticalHit)
    {
        if (damageAmount != 0)
        {
            textMesh.SetText(damageAmount.ToString());
        }
        else
        {
            textMesh.SetText(DEAD);
        }

        if (!isCriticalHit)
        {
            textMesh.fontSize = REGULAR_FONT_SIZE;
            textColor = defaultColor;
            disappearTimer = DISAPPEAR_TIMER_MAX;
        }
        else
        {
            textMesh.fontSize = CRITICAL_FONT_SIZE;
            textColor = Color.red;
            disappearTimer = DISAPPEAR_TIMER_CRITICAL_MAX;
        }

        textMesh.color = textColor;
        disappearSpeed = 4f;

        if (localScale > Mathf.Epsilon)
        {
            moveVector = new Vector3(1.3f, 1) * 20;
        }
        else
        {
            moveVector = new Vector3(-1.3f, 1) * 20f;
        }

        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;
    }


    private void Update()
    {
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 8f * Time.deltaTime;

        float increaseScaleAmount = 1f;

        if (disappearTimer > DISAPPEAR_TIMER_MAX * 0.5)
        {
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        }
        else
        {
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