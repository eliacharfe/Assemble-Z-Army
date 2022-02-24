using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class RTSPlayer : NetworkBehaviour
{
    public List<Unit> m_units = new List<Unit>();

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
        Camera.main.transform.position = pos;
    }

    #endregion


    #region Server
    public override void OnStartServer()
    {
        Unit.ServerOnUnitSpawned += ServerHandleUnitSpawned;
        Unit.ServerOnUnitDeSpawned += ServerHandleUnitDeSpawned;

        DontDestroyOnLoad(gameObject);
    }


    public override void OnStopServer()
    {

        Debug.Log("Stop player server");

        Unit.ServerOnUnitSpawned -= ServerHandleUnitSpawned;
        Unit.ServerOnUnitDeSpawned -= ServerHandleUnitDeSpawned;
    }


    // Add new unit to the server 'myUnits' list.
    private void ServerHandleUnitSpawned(Unit unit)
    {
        if (isUnitBelongToClient(unit))
            m_units.Add(unit);
    }


    // Remove unit from the server 'myUnits' list
    private void ServerHandleUnitDeSpawned(Unit unit)
    {
        Debug.Log("Units removed");
        if (isUnitBelongToClient(unit))
            m_units.Remove(unit);
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
        GameObject buildingInstance = Instantiate(buildingsFactory.GetBuildingById(buildingId), point, Quaternion.identity);

        NetworkServer.Spawn(buildingInstance, connectionToClient);
    }
    #endregion

    // Add unit to authorty 'list'.
    private void AuthortyHandleUnitDeSpawned(Unit unit)
    {

        Debug.Log("Units removed");

        if (!hasAuthority) return;
        m_units.Remove(unit);
    }


    // Remove unit to authorty 'list'.
    private void AuthortyHandleUnitSpawned(Unit unit)
    {
        if (!hasAuthority) return;
        m_units.Add(unit);
    }

    public void HideUnits()
    {
        foreach(Unit unit in m_units)
        {
            unit.gameObject.SetActive(false);
            Debug.Log("Unit is now hidden");
        }
    }







  


}
