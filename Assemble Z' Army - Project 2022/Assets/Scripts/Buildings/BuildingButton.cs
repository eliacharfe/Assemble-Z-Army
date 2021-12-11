using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class BuildingButton : MonoBehaviour,IPointerDownHandler, IPointerUpHandler
{
    public Building building = null;

    public GameObject buildingPreview;

    private GameObject spritePreview;


    private void Start()
    {
        Sprite buildingSprite = building.GetComponentInChildren<SpriteRenderer>().sprite;
        gameObject.GetComponent<Image>().sprite = buildingSprite;
        buildingPreview.GetComponent<SpriteRenderer>().sprite = buildingSprite;
    }

    private void Update()
    {
        if (spritePreview == null) { return; }

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        spritePreview.transform.position = Camera.main.ScreenToWorldPoint(mousePos);

        Vector3 placePosition = spritePreview.transform.position;
        spritePreview.GetComponent<SpriteRenderer>().color = Color.green;

        if(!CanPlaceBuilding(spritePreview.GetComponent<BoxCollider2D>(), placePosition))
        {
            spritePreview.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) { return; }

        spritePreview = Instantiate(buildingPreview, Input.mousePosition, Quaternion.identity);
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        Vector3 placePosition = spritePreview.transform.position;

        if (CanPlaceBuilding(spritePreview.GetComponent<BoxCollider2D>(),placePosition)) {
            Instantiate(building, spritePreview.transform.position, Quaternion.identity);
        }

        Destroy(spritePreview);
    }


    public void spawnBuilding()
    {
        Instantiate(building, Camera.main.transform.position, Quaternion.identity);
    }


    private bool CanPlaceBuilding(BoxCollider2D buildingCollider, Vector3 pos)
    {
        Vector2 positiosn2D = new Vector2(pos.x,pos.y);

        var buildings = FindObjectsOfType<Building>();

        Debug.Log("Intial check building:" + buildingCollider.transform.position);

        foreach (var building in buildings)
        {

            if (building.GetComponent<BoxCollider2D>().bounds.Intersects(buildingCollider.bounds))
            {
                Debug.Log("Intersection !!!!");
                return false;
            }
        }

        Debug.Log("Can be placed building");
        return true;
    }

}
