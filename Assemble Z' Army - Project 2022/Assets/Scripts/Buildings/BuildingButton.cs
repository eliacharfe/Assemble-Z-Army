using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.AI;

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

        mousePos.z = 1;

        spritePreview.transform.position = Camera.main.ScreenToWorldPoint(mousePos);

        spritePreview.GetComponent<SpriteRenderer>().color = Color.green;

        if(!CanPlaceBuilding(spritePreview.GetComponent<BoxCollider2D>()))
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

        if (CanPlaceBuilding(spritePreview.GetComponent<BoxCollider2D>())) {
            spawnBuilding(spritePreview.transform.position);
        }

        Destroy(spritePreview);
    }


    public void spawnBuilding(Vector3 pos)
    {
        Instantiate(building, pos, Quaternion.identity);
    }


    private bool CanPlaceBuilding(BoxCollider2D buildingCollider)
    {

        var buildings = FindObjectsOfType<Building>();

        foreach (var building in buildings){

            if (buildingCollider.bounds.Intersects(building.GetComponent<BoxCollider2D>().bounds)){
                return false;
            }
        }
        return true;
    }

}
