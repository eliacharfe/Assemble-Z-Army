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
    [SerializeField] [SyncVar(hook = nameof(HandleRecruitmentTimeUpdated))] float timePassed = 0;
    [SerializeField] private Transform spawnPoint = null;
    [SerializeField] private Transform enterPoint = null;

    [Header("Building Type")]
    [SerializeField] private Macros.Buildings Id;

    [Header("Costs")]
    public List<int> costResourcesBuilding = null;
    [SerializeField] private int woodCost;
    [SerializeField] private int metalCost;
    [SerializeField] private int goldCost;
    [SerializeField] private int diamondsCost;

    [Header("UI")]
    [SerializeField] private GameObject token = null;
    [SerializeField] private CostumeSlider timeSlider = null;

    // Units waiting to be recruited.
    public List<Unit> waitingUnit = new List<Unit>();

    // Prefab of the current unit need to be spawned.
    private Unit spawnUnitPrefab = null;

    private bool inProgess = false;

    private UnitsFactory unitsFactory = null;




    public static event Action<Building> ServerOnBuildingSpawned;
    public static event Action<Building> ServerOnBuildingDeSpawned;
 
    public static event Action<Building> AuthortyOnBuildingSpawned;
    public static event Action<Building> AuthortyOnBuildingDeSpawned;

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

    public void HandleRecruitmentTimeUpdated(float oldTime,float newTime)
    {
        //timeSlider.setValue(newTime/spawnTime);

    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //Set the building accoridng to the existed navmesh z position.
        Vector3 pos = FindObjectOfType<NavMeshScript>().transform.position;

        gameObject.transform.position = new Vector3
            (gameObject.transform.position.x, gameObject.transform.position.y, pos.z);

        unitsFactory = FindObjectOfType<UnitsFactory>();

        print("Does building has unitsFactory?" + unitsFactory);
    }



    // Update is called once per frame
    [ClientCallback]
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

    public List<int> getCostBuilding()
    {
        return costResourcesBuilding;
    }


    // Try to recruit if there is waiting units.
    void TryToRecruitUnit()
    {

        if (waitingUnit.Count <= 0){ return; }

        Unit unit = waitingUnit[0];

        if (!unit) { return; }

        if (Vector2.Distance(unit.transform.position, enterPoint.position) 
            > spawnDistancePoint){ return; }

        spawnUnitPrefab = unitsFactory.GetBuildingOutputUnit(this.Id, unit.id);

        print(spawnUnitPrefab);

        RemoveUnitFromWaitingList(unit);

        if (spawnUnitPrefab)
        {
            inProgess = true;

            unit.SetDead();

            Destroy(unit.gameObject);

        }

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


    //Spawn new unit.
    [Command]
    private void CmdSpawnNewUnit(Macros.Units id)
    {

        Unit unit = Instantiate(unitsFactory.GetUnitPrefab(id), spawnPoint.position, Quaternion.identity) as Unit;

        NetworkServer.Spawn(unit.gameObject, connectionToClient);

        //unit.MoveTo(spawnPoint.position + new Vector3(10f, 0, 0));
    }


    // Intilize recruitment data time and allow recruit other unit.
    private void FreeBuildingSpace()
    {
        timePassed = 0f;
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
            if (timePassed < spawnTime)
            {
                timePassed += Time.deltaTime;
            }
            else
            {
                CmdSpawnNewUnit(spawnUnitPrefab.id);

                FreeBuildingSpace();

                timePassed = 0;
            }
        }
        timeSlider.setValue(timePassed / spawnTime);
    }


    // Enter Unit into the waiting list recruitemnt.
    public Vector3 EnterWaitingRecruitment(Unit unit)
    {

        if (!waitingUnit.Contains(unit) && unitsFactory.GetBuildingOutputUnit(this.Id, unit.id))
        {
            waitingUnit.Add(unit);

            return enterPoint.position + new Vector3(-20 * (waitingUnit.Count - 1), 0, 0);
        }

        return unit.transform.position;
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
        return (int)Id ;
    }
    public Sprite GetBuildingSprite()
    {
        return GetComponent<Sprite>();
    }

    public bool isMatchedUnit(Unit unit)
    {
        return unitsFactory.GetBuildingOutputUnit(this.Id, unit.id);
    }
}
