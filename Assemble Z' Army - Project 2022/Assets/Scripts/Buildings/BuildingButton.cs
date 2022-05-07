using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using Mirror;

public class BuildingButton : MonoBehaviour,IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField]private Building building = null;

    public GameObject buildingPreview;

    private GameObject spritePreview;

    private RTSPlayer player;

    private float navMeshZAxis;

    ResourcesPlayer resourcesPlayer = null;

    string popupCostBuilding;

    private void Start()
    {
        Sprite buildingSprite = building.GetComponentInChildren<SpriteRenderer>().sprite;
        gameObject.GetComponent<Image>().sprite = buildingSprite;
        buildingPreview.GetComponent<SpriteRenderer>().sprite = buildingSprite;

        navMeshZAxis = FindObjectOfType<NavMeshScript>().transform.position.z;

        resourcesPlayer = FindObjectOfType<ResourcesPlayer>();

        building.InitiateCosts();

        popupCostBuilding = "Wood: " + building.getCostBuilding()[0].ToString() + '\n' +
                            "Metal: " + building.getCostBuilding()[1].ToString() + '\n' +
                            "Gold: " + building.getCostBuilding()[2].ToString() + '\n' +
                            "Diam's: " + building.getCostBuilding()[3].ToString() + '\n';

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

        if(!resourcesPlayer.isHaveEnoughResources(building.getCostBuilding())) { 
            // Show message if doesnt have enough resources
            return;
        }

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
            FindObjectOfType<AudioPlayer>().PlaySpawnBuilding();

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

    public void OnPointerEnter(PointerEventData eventData)
    {
        Tooltip.ShowTooltip_Static(popupCostBuilding, building.GetBuildingText());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tooltip.HideTooltip_Static();
    }

}
