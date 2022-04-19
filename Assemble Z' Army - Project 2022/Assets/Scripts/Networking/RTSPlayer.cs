using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;
using System;

public class RTSPlayer : NetworkBehaviour
{
    // Following camera of the player.
    [SerializeField] private Transform cameraTransform = null;

    // TODO - set access modifier
    // The phase three pos update the camera position for the player.
    [SyncVar(hook = nameof(HandleCameraChange))] public Vector3 phaseThreePos = new Vector3(0,0,0);

    [SyncVar]public bool isPlayerLost = false;

    public GameObject unitSpawner = null;

    public List<GameObject> m_workers = new List<GameObject>();

    public List<int> m_unitsId = new List<int>();

    private BuildingsFactory buildingsFactory = null;

    private UnitsFactory unitsFactory = null;

    private bool isPartyOwner = false;

    private Color teamColor = new Color();

    [SyncVar(hook = nameof(ClientHandleDisplayNameUpdated))]
    private string displayName;

    // Server Unit despawned event.
    public static event Action<int> PlayerLostAllUnits;

    public static event Action ClientOnInfoUpdated;
    public static event Action<bool> AuthorityOnPartyOwnerStateUpdated;

    const String PHASE_ONE_SCENE = "Playground",PHASE_TWO_SCENE = "Battlefield";

    // Start is called before the first frame update
    void Start()
    {
        buildingsFactory = FindObjectOfType<BuildingsFactory>();
    }


    // Update is called once per frame
    void Update(){}

    #region Setters
    public void SetPartyOwner(bool state) { isPartyOwner = state; }

    [Server]
    public void SetDisplayName(string displayName) { this.displayName = displayName; }

    [Server]
    public void SetTeamColor(Color newTeamColor) { teamColor = newTeamColor; }
   
    public void SetCameraPosition(Vector3 pos)
    {
        cameraTransform.position = pos;

        GetComponent<CameraInputSystem>().OnStartingGame();
    }


    public void HandleCameraChange(Vector3 oldPos,Vector3 newPos)
    {

        String sceneName = NetworkManager.networkSceneName;

        SetCameraPosition(newPos);
        if (sceneName == PHASE_ONE_SCENE)
        {
            GetComponent<CameraInputSystem>().OnStartingGame();
            print("Phase one camera set!!!");
        }
        else if (sceneName == PHASE_TWO_SCENE)
        {
            GetComponent<CameraInputSystem>().OnChangePhaseThreeMap();
            print("Phase three camera set!!!");
        }


        //
    }
    #endregion


    #region Getters
    public string GetDisplayName() { return displayName; }

    public Vector3 GetCameraPosition()
    {
        return cameraTransform.position;
    }
    #endregion

    public Color GetTeamColor()
    {
        return teamColor;
    }
    #region Server
    public override void OnStartServer()
    {
        Unit.ServerOnUnitSpawned += ServerHandleUnitSpawned;
        Unit.ServerOnUnitDeSpawned += ServerHandleUnitDeSpawned;

        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        Destroy(unitSpawner);
    }

    public override void OnStopServer()
    {
        Unit.ServerOnUnitSpawned -= ServerHandleUnitSpawned;
        Unit.ServerOnUnitDeSpawned -= ServerHandleUnitDeSpawned;
    }

    // Add new unit to the server 'myUnits' list.
    private void ServerHandleUnitSpawned(Unit unit)
    {
        if (isUnitBelongToClient(unit))
        {
            m_unitsId.Add((int)unit.id);
        }
       
    }

    // Remove unit from the server 'myUnits' list
    private void ServerHandleUnitDeSpawned(Unit unit)
    {

        if (!isUnitBelongToClient(unit)) return;

        if(unit.isDead)
             m_unitsId.Remove((int)unit.id);

        if(m_unitsId.Count <= 0)
        {
            isPlayerLost = true;

            PlayerLostAllUnits?.Invoke(connectionToClient.connectionId);
        }
    }

    private bool isUnitBelongToClient(Unit unit)
    {
        return unit.connectionToClient.connectionId ==
            connectionToClient.connectionId;
    }

    // Command the server to start the game.
    [Command]
    public void CmdStartGame()
    {
        if (!isPartyOwner) { return; }

        ((RtsNetworkManager)NetworkManager.singleton).ShowPreparationPhase();
    }

    public void SpawnRecruitedUnit()
    {
        var factory = FindObjectOfType<UnitsFactory>();

        List<int> copy = new List<int>(m_unitsId);

        foreach(int id in copy)
        {
            GameObject unit = factory.GetUnitPrefab((Macros.Units)id).gameObject;
            
            GameObject unitInstance = Instantiate(unit, phaseThreePos, Quaternion.identity);

            NetworkServer.Spawn(unitInstance, connectionToClient);

            m_unitsId.Remove(id);
        }

    }


    [Server]
    public void removeWorkers()
    {
        m_unitsId.Clear();

        foreach(GameObject worker in m_workers)
        {
            Destroy(worker);
        }
    }


    #endregion

    #region Client
    public override void OnStartClient()
    {
        if (NetworkServer.active) return;

        DontDestroyOnLoad(gameObject);

        ((RtsNetworkManager)NetworkManager.singleton).players.Add(this);
    }


    public override void OnStopClient()
    {
        base.OnStopClient();

        if (!isClientOnly) return;

        ((RtsNetworkManager)NetworkManager.singleton).players.Remove(this);

        if (!hasAuthority) return;

        Unit.AuthortyOnUnitSpawned -= AuthortyHandleUnitSpawned;
        Unit.AuthortyOnUnitDeSpawned -= AuthortyHandleUnitDeSpawned;
    }

    
    public override void OnStartAuthority()
    {
        Unit.AuthortyOnUnitSpawned += AuthortyHandleUnitSpawned;
        Unit.AuthortyOnUnitDeSpawned += AuthortyHandleUnitDeSpawned;
    }


    public override void OnStopAuthority()
    {
        Unit.AuthortyOnUnitSpawned -= AuthortyHandleUnitSpawned;
        Unit.AuthortyOnUnitDeSpawned -= AuthortyHandleUnitDeSpawned;
    }


    [Command]
    public void CmdTryPlaceBuilding(int buildingId, Vector3 point)
    {
        print("Player putted building" + buildingId + " from " + buildingsFactory);

        buildingsFactory = FindObjectOfType<BuildingsFactory>();

        GameObject buildingInstance = Instantiate(buildingsFactory.GetBuildingById(buildingId), point, Quaternion.identity);

        NetworkServer.Spawn(buildingInstance, connectionToClient);
    }


    [ClientRpc]
    public void RpcPlayerHasLost(string playerLost)
    {
        print("Client player with number" + playerLost +" has lost");
    }
    #endregion

    // Add unit to authorty 'list'.
    private void AuthortyHandleUnitDeSpawned(Unit unit)
    {
        if (!hasAuthority) return;

        if (unit.isDead)
            m_unitsId.Remove((int)unit.id);
    }


    // Remove unit to authorty 'list'.
    private void AuthortyHandleUnitSpawned(Unit unit)
    {
        if (!hasAuthority) return;
    }


    private void ClientHandleDisplayNameUpdated(string oldName, string newName)
    {
        ClientOnInfoUpdated?.Invoke();
    }
}
