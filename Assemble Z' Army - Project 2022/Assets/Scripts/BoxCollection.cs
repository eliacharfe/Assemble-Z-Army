using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCollection : MonoBehaviour
{
    private LineRenderer lineRend;
    private Vector2 initMousePos, currMousePos;
    private BoxCollider2D boxColl;

    void Start()
    {
        lineRend = GetComponent<LineRenderer>();
        lineRend.positionCount = 0;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lineRend.positionCount = 4;
            initMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            lineRend.SetPosition(0, new Vector2(initMousePos.x, initMousePos.y));
            lineRend.SetPosition(1, new Vector2(initMousePos.x, initMousePos.y));
            lineRend.SetPosition(2, new Vector2(initMousePos.x, initMousePos.y));
            lineRend.SetPosition(3, new Vector2(initMousePos.x, initMousePos.y));

            boxColl = gameObject.AddComponent<BoxCollider2D>();
            boxColl.isTrigger = true;
            boxColl.offset = new Vector3(transform.position.x, transform.position.y, transform.position.z);

            Debug.Log("BoxCollection - down");
        }

        if (Input.GetMouseButton(0))
        {
            lineRend.positionCount = 4;
            currMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            lineRend.SetPosition(0, new Vector2(initMousePos.x, initMousePos.y));
            lineRend.SetPosition(1, new Vector2(initMousePos.x, initMousePos.y));
            lineRend.SetPosition(2, new Vector2(initMousePos.x, initMousePos.y));
            lineRend.SetPosition(3, new Vector2(initMousePos.x, initMousePos.y));

            transform.position = (currMousePos + initMousePos) / 2;

            boxColl.size = new Vector2(
                Mathf.Abs(initMousePos.x - currMousePos.x),
                 Mathf.Abs(initMousePos.y - currMousePos.y));

            Debug.Log("BoxCollection - drag");
        }

        if (Input.GetMouseButtonUp(0))
        {
            lineRend.positionCount = 0;
            Destroy(boxColl);
            transform.position = Vector3.zero;
            
            Debug.Log("BoxCollection - Up");
        }

    }
}
