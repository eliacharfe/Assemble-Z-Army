using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using Mirror;

public class BuildingButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Building building = null;

    public GameObject buildingPreview;

    private GameObject spritePreview;

    private RTSPlayer player;

    private float navMeshZAxis;

    ResourcesPlayer resourcesPlayer = null;

    AudioPlayer audioPlayer;

    [SerializeField] private Transform buildingPopup;

    string popupCostBuilding;

    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    private void Start()
    {
        if (player) {
            player = NetworkClient.connection.identity.GetComponent<RTSPlayer>();
        }

        resourcesPlayer = FindObjectOfType<ResourcesPlayer>();
        building.InitiateCosts();

        Sprite buildingSprite = building.GetComponentInChildren<SpriteRenderer>().sprite;
        gameObject.GetComponent<Image>().sprite = buildingSprite;
        buildingPreview.GetComponent<SpriteRenderer>().sprite = buildingSprite;

        navMeshZAxis = FindObjectOfType<NavMeshScript>().transform.position.z;

        popupCostBuilding = "Wood: " + building.getCostBuilding()[0].ToString() + '\n' +
                            "Metal: " + building.getCostBuilding()[1].ToString() + '\n' +
                            "Gold: " + building.getCostBuilding()[2].ToString() + '\n' +
                            "Diamonds: " + building.getCostBuilding()[3].ToString() + '\n';
    }

    private void Update()
    {
        if (spritePreview == null) { return; }

        if (!player) {
            // player = NetworkClient.connection.identity.GetComponent<RTSPlayer>();
        }

        if (!resourcesPlayer.isHaveEnoughResources(building.getCostBuilding()))
        {
            audioPlayer.PlayBtnClickErrorClip();
            return;
        }

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = navMeshZAxis;
        spritePreview.transform.position = mousePos;

        spritePreview.GetComponent<SpriteRenderer>().color = Color.green;

        if (!CanPlaceBuilding(spritePreview.GetComponent<BoxCollider2D>()))
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

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = navMeshZAxis;

        if (resourcesPlayer.isHaveEnoughResources(building.getCostBuilding()))
        {
            Tooltip.ShowTooltip_Static(popupCostBuilding);
            
            BuildingPricePopup.Create(buildingPopup, mousePos, building.getCostBuilding());
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("onMouseEnter");
        Tooltip.ShowTooltip_Static(popupCostBuilding);
    }

    public void OnPointerOver(PointerEventData eventData)
    {
        Debug.Log("onMouseOver");
        Tooltip.ShowTooltip_Static(popupCostBuilding);
    }

    private void OnMouseEnter()
    {
        Debug.Log("onMouseEnter");
        Tooltip.ShowTooltip_Static(popupCostBuilding);
    }

    private void OnMouseOver()
    {
        Debug.Log("onMouseOverrr");
        Tooltip.ShowTooltip_Static(popupCostBuilding);
    }

    // private void OnMouseExit()
    // {
    //     Tooltip.HideTooltip_Static();
    // }


    public void OnPointerUp(PointerEventData eventData)
    {
        Tooltip.HideTooltip_Static();

        if (CanPlaceBuilding(spritePreview.GetComponent<BoxCollider2D>()))
        {
            SpawnBuilding(spritePreview.transform.position);
        }

        Destroy(spritePreview);
    }


    public void SpawnBuilding(Vector3 pos)
    {
        if (resourcesPlayer.isHaveEnoughResources(building.getCostBuilding()))
        {
            resourcesPlayer.DecreaseResource(building.getCostBuilding());
        }

    }


    private bool CanPlaceBuilding(BoxCollider2D buildingCollider)
    {
        var buildings = FindObjectsOfType<Building>();

        foreach (var building in buildings)
        {

            if (buildingCollider.bounds.Intersects(building.GetComponent<BoxCollider2D>().bounds))
            {
                return false;
            }
        }
        return true;
    }

}
