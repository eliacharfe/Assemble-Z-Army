using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class RTSPlayer : NetworkBehaviour
{
    // Following camera of the player.
    [SerializeField] private Transform cameraTransform = null;

    // TODO - set access modifier
    // The phase three pos update the camera position for the player.
    [SyncVar(hook = nameof(SetPhaseThreeCamera))] public Vector3 phaseThreePos = new Vector3(0,0,0);

    [SyncVar] bool playerLost = false;

    public List<Unit> m_units = new List<Unit>();

    public List<int> m_unitsId = new List<int>();

    private BuildingsFactory buildingsFactory = null;

    private bool isPartyOwner = false;

    // Start is called before the first frame update
    void Start()
    {
        buildingsFactory = FindObjectOfType<BuildingsFactory>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    #region Setters
    public void SetPartyOwner(bool state) { isPartyOwner = state; }


    public void SetCameraPosition(Vector3 pos)
    {
        cameraTransform.position = pos;

        GetComponent<CameraInputSystem>().OnChangeMap();
    }


    public void SetPhaseThreeCamera(Vector3 oldPos,Vector3 newPos)
    {
        SetCameraPosition(newPos);
    }
    #endregion


    #region Getters
    public Vector3 GetCameraPosition()
    {
        return cameraTransform.position;
    }
    #endregion


    #region Server
    public override void OnStartServer()
    {
        Unit.ServerOnUnitSpawned += ServerHandleUnitSpawned;
        Unit.ServerOnUnitDeSpawned += ServerHandleUnitDeSpawned;

        Debug.Log(transform.position);

        DontDestroyOnLoad(gameObject);
    }

    public override void OnStopServer()
    {

        Debug.Log("Stop player server");

        Unit.ServerOnUnitSpawned -= ServerHandleUnitSpawned;
        Unit.ServerOnUnitDeSpawned -= ServerHandleUnitDeSpawned;

        Camera.main.transform.position = cameraTransform.position;

        //SceneManager.LoadScene("Playground");
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
        Debug.Log("Units removed on server");
        if (isUnitBelongToClient(unit))
            if(unit.isDead)
             m_unitsId.Remove((int)unit.id);

        if(m_unitsId.Count <= 0)
        {

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

        ((RtsNetworkManager)NetworkManager.singleton).ShowBattleField();
    }



    public void SpawnRecruitedUnit()
    {
        var factory = FindObjectOfType<UnitsFactory>();
    
        print(m_unitsId.Count);

        List<int> copy = new List<int>(m_unitsId);

        print("Spawning units");

        foreach(int id in copy)
        {
            GameObject unit = FindObjectOfType<UnitsFactory>().GetUnitPrefab((Macros.Units)id).gameObject;
            
            GameObject unitInstance = Instantiate(unit, phaseThreePos, Quaternion.identity);

            NetworkServer.Spawn(unitInstance, connectionToClient);

            m_unitsId.Remove(id);
        }

    }


    // TODO Delete later.
    [Command]
    public void CmdPlayerLostMsg()
    {
        print("Player has lost");

        RpcPlayerHasLost();
    }
    #endregion

    #region Client
    public override void OnStartClient()
    {
        if (NetworkServer.active) return;

        ((RtsNetworkManager)NetworkManager.singleton).players.Add(this);

        DontDestroyOnLoad(gameObject);
    }


    public override void OnStopClient()
    {
        base.OnStopClient();

        if (!isClientOnly) return;

        ((RtsNetworkManager)NetworkManager.singleton).players.Remove(this);

        if (!hasAuthority) return;

        Unit.AuthortyOnUnitSpawned -= AuthortyHandleUnitSpawned;
        Unit.AuthortyOnUnitDeSpawned -= AuthortyHandleUnitDeSpawned;

        SceneManager.LoadScene("Playground");
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
        GameObject buildingInstance = Instantiate(buildingsFactory.GetBuildingById(buildingId), point, Quaternion.identity);

        NetworkServer.Spawn(buildingInstance, connectionToClient);
    }


    [ClientRpc]
    public void RpcPlayerHasLost()
    {
        print("Client player , you lost");
    }
    #endregion

    // Add unit to authorty 'list'.
    private void AuthortyHandleUnitDeSpawned(Unit unit)
    {

        print("Unit removed in authorty");

        if (!hasAuthority) return;

        if(unit.isDead)
            m_unitsId.Remove((int)unit.id);

        if (m_unitsId.Count <= 0 && connectionToClient.isReady)
        {
            CmdPlayerLostMsg();
        }
    }


    // Remove unit to authorty 'list'.
    private void AuthortyHandleUnitSpawned(Unit unit)
    {
        if (!hasAuthority) return;

        //m_unitsId.Add((int)unit.id);
    }


    public void ShowUnits(bool value)
    {
        foreach (Unit unit in m_units)
        {
            unit.ReintilizeNavMesh();

            unit.gameObject.SetActive(value);

            Debug.Log("Unit is now hidden");
        }
    }


    public void SetUnitsPositions(Vector3 pos)
    {
        foreach (Unit unit in m_units)
        {
            unit.SetPostion(pos);

            unit.ReintilizeNavMesh();

            Debug.Log("Unit new position:" + pos);
        }
    }

}
