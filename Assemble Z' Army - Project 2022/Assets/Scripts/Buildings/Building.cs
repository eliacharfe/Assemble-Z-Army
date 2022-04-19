using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Mirror;
using System;

public class Building : NetworkBehaviour
{
    // Type of solider building recived and create.

    [Header("Spawning Settings")]
    [SerializeField] private float spawnTime = 5f;
    [SerializeField] private float spawnDistancePoint = 1.5f;
    [SerializeField] private Transform spawnPoint = null;
    [SerializeField] private Transform enterPoint = null;

    [Header("Building Type")]
    [SerializeField] private Macros.Buildings Id;

    [Header("Costs")]
    [SerializeField] private int woodCost;
    [SerializeField] private int metalCost;
    [SerializeField] private int goldCost;
    [SerializeField] private int diamondsCost;

    [Header("UI")]
    [SerializeField] private GameObject token = null;
    [SerializeField] private CostumeSlider timeSlider = null;

    // Units waiting to be recruited.
    public List<Unit> waitingUnit = new List<Unit>();
    public List<int> costResourcesBuilding = null;

    // Prefab of the current unit need to be spawned.
    private Unit spawnUnitPrefab = null;

    private bool inProgess = false;
    private float timeLeft = 0;

    private UnitsFactory unitsFactory = null;

    public static event Action<Building> ServerOnBuildingSpawned;
    public static event Action<Building> ServerOnBuildingDeSpawned;

    public static event Action<Building> AuthortyOnBuildingSpawned;
    public static event Action<Building> AuthortyOnBuildingDeSpawned;

    Building building = null;

    void Start()
    {
        //Set the building accoridng to the existed navmesh z position.
        Vector3 pos = FindObjectOfType<NavMeshScript>().transform.position;

        gameObject.transform.position = new Vector3
            (gameObject.transform.position.x, gameObject.transform.position.y, pos.z);

        unitsFactory = FindObjectOfType<UnitsFactory>();
    }
    //-------------
    public List<int> getCostBuilding()
    {
        return costResourcesBuilding;
    }
    //-------------------
    public void InitiateCosts()
    {
        costResourcesBuilding = new List<int>();

        costResourcesBuilding.Add(woodCost);
        costResourcesBuilding.Add(metalCost);
        costResourcesBuilding.Add(goldCost);
        costResourcesBuilding.Add(diamondsCost);
    }


    #region Server

    public override void OnStartServer()
    {
        ServerOnBuildingSpawned?.Invoke(this);
    }

    public override void OnStopServer()
    {
        ServerOnBuildingDeSpawned?.Invoke(this);
    }
    #endregion


    #region Client
    public override void OnStartAuthority()
    {
        AuthortyOnBuildingSpawned?.Invoke(this);
    }

    public override void OnStopAuthority()
    {
        AuthortyOnBuildingDeSpawned?.Invoke(this);
    }
    #endregion


    // Update is called once per frame
    void Update()
    {
        if (!this.enabled)
        {
            return;
        }

        // Hide UI elements.
        if (Input.GetMouseButtonDown(0))
        {
            token.SetActive(false);

            ShowBuildingPanel(false);
        }

        SpawningProgession();
    }


    // Show building token and panel.
    private void OnMouseUp()
    {
        token.SetActive(true);

        ShowBuildingPanel(true);
    }


    // When mouse hover.
    private void OnMouseEnter()
    {
        if (!token.activeSelf)
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.grey;
        }
    }


    // When mouse doesnt hover.
    private void OnMouseExit()
    {
        gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }


    // Try to recruit if there is waiting units.
    void TryToRecruitUnit()
    {

        if (waitingUnit.Count <= 0) { return; }

        Unit unit = waitingUnit[0];

        if (!unit) { return; }

        if (Vector2.Distance(unit.transform.position, enterPoint.position)
            > spawnDistancePoint) { return; }

        spawnUnitPrefab = unitsFactory.GetBuildingOutputUnit(this.Id, unit.id);

        RemoveUnitFromWaitingList(unit);

        if (spawnUnitPrefab)
        {
            inProgess = true;

            Destroy(unit.gameObject);
        }

    }


    //Spawn new unit.
    private void SpawnNewUnit()
    {
        Unit unit = Instantiate(spawnUnitPrefab, spawnPoint.position, Quaternion.identity) as Unit;

        unit.MoveTo(spawnPoint.position + new Vector3(10f, 0, 0));
    }


    // Intilize recruitment data time and allow recruit other unit.
    private void FreeBuildingSpace()
    {
        timeLeft = 0f;
        inProgess = false;
        timeSlider.resetSlider();
    }


    // Show the building panel info for description or options.
    private void ShowBuildingPanel(bool value)
    {
        BuildinPanelDisplay panel = FindObjectOfType<BuildinPanelDisplay>();

        if (panel)
        {
            panel.GetComponent<Image>().enabled = value;
        }

    }


    // Deal with unit spawing time.
    private void SpawningProgession()
    {
        if (!inProgess)
        {
            TryToRecruitUnit();
        }
        else
        {
            if (timeLeft < 0.1f)
            {
                timeLeft += Time.deltaTime;
            }
            else
            {
                timeSlider.IncreaseSlider(0.1f / spawnTime);
                timeLeft = 0;
            }
        }

        if (timeSlider.SliderFinished())
        {
            SpawnNewUnit();

            FreeBuildingSpace();
        }
    }


    // Enter Unit into the waiting list recruitemnt.
    public Vector3 EnterWaitingRecruitment(Unit unit)
    {

        if (!waitingUnit.Contains(unit) && unitsFactory.GetBuildingOutputUnit(this.Id, unit.id))
        {
            waitingUnit.Add(unit);

            return enterPoint.position + new Vector3(-20 * (waitingUnit.Count - 1), 0, 0);
        }

        return Vector3.zero;
    }


    // Remove the unit from waiting and move the other units whom waiting.
    public void RemoveUnitFromWaitingList(Unit unit)
    {

        if (waitingUnit.Contains(unit))
        {
            waitingUnit.Remove(unit);

            for (int i = 0; i < waitingUnit.Count; i++)
            {
                waitingUnit[i].MoveTo(enterPoint.position + new Vector3(-20 * i, 0, 0));
            }
        }
    }

    public int GetBuildingId()
    {
        return (int)Id;
    }
    public Sprite GetBuildingSprite()
    {
        return GetComponent<Sprite>();
    }
}
