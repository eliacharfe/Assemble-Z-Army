using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using Mirror;

public class BuildingButton : MonoBehaviour,IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]private Building building = null;

    public GameObject buildingPreview;

    private GameObject spritePreview;

    private RTSPlayer player;

    private float navMeshZAxis;

    ResourcesPlayer resourcesPlayer = null;


    private void Start()
    {
        Sprite buildingSprite = building.GetComponentInChildren<SpriteRenderer>().sprite;
        gameObject.GetComponent<Image>().sprite = buildingSprite;
        buildingPreview.GetComponent<SpriteRenderer>().sprite = buildingSprite;

        navMeshZAxis = FindObjectOfType<NavMeshScript>().transform.position.z;

        resourcesPlayer = FindObjectOfType<ResourcesPlayer>();

        building.InitiateCosts();
    }

    private void Update()
    {
        if (spritePreview == null) { return; }

        if (!player) {
            player = NetworkClient.connection.identity.GetComponent<RTSPlayer>();
        }

        if (Input.GetMouseButtonDown(1))
        {
            Destroy(spritePreview);
            spritePreview = null;
            return;
        }

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mousePos.z = navMeshZAxis;

        spritePreview.transform.position = mousePos;

        spritePreview.GetComponent<SpriteRenderer>().color = Color.green;

        if(!CanPlaceBuilding(spritePreview.GetComponent<BoxCollider2D>()))
        {
            spritePreview.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) { return; }

        spritePreview = Instantiate(buildingPreview, 
            Camera.main.ScreenToWorldPoint(Input.mousePosition),
            Quaternion.identity);
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        if (!spritePreview){return;}

        if (CanPlaceBuilding(spritePreview.GetComponent<BoxCollider2D>())) {
            SpawnBuilding(spritePreview.transform.position);
        }

        Destroy(spritePreview);
    }

    public void SpawnBuilding(Vector3 pos)
    {
        if (resourcesPlayer.isHaveEnoughResources(building.getCostBuilding()))
        {
            resourcesPlayer.DecreaseResource(building.getCostBuilding());

            player.CmdTryPlaceBuilding(building.GetBuildingId(), pos);
        }

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
