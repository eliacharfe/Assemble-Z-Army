using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using CodeMonkey.Utils;

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


    private TextMeshPro textMesh;
    private Color textColor;
    private float disappearTimer;

    private Color defaultColor;
    float disappearSpeed;

    private void Awake()
    {
        //defaultColor = new Color(243, 97, 0, 255);
        
        textMesh = transform.GetComponent<TextMeshPro>();
          defaultColor = textMesh.color;
    }

    public void Setup(int damageAmount, bool isCriticalHit)
    {
        textMesh.SetText(damageAmount.ToString());

        if (!isCriticalHit)
        {
            textMesh.fontSize = 60f;
            textColor = defaultColor;
             disappearTimer = 0.5f;
        }
        else
        {
            textMesh.fontSize = 90f;
            textColor = Color.red;
             disappearTimer = 1.5f;
        }

        textMesh.color = textColor;
        disappearSpeed = 4f;
    }


    private void Update()
    {
        float moveYspeed = 20f;
        transform.position += new Vector3(0, moveYspeed) * Time.deltaTime;

        Debug.Log(disappearTimer);

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
