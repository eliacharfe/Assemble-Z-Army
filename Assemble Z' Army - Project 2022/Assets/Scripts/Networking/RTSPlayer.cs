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
        Unit.ServerOnUnitSpawned += AddUnit;
        Unit.ServerOnUnitDeSpawned += RemoveUnit;

        DontDestroyOnLoad(gameObject);
    }


    public override void OnStopServer()
    {
        Unit.ServerOnUnitSpawned -= AddUnit;
        Unit.ServerOnUnitDeSpawned -= RemoveUnit;
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

        Unit.AuthortyOnUnitSpawned -= AddUnit;
        Unit.AuthortyOnUnitDeSpawned -= RemoveUnit;
    }


    public override void OnStartAuthority()
    {
        Unit.AuthortyOnUnitSpawned += AddUnit;
        Unit.AuthortyOnUnitDeSpawned += RemoveUnit;
    }


    public override void OnStopAuthority()
    {
        Unit.AuthortyOnUnitSpawned -= AddUnit;
        Unit.AuthortyOnUnitDeSpawned -= RemoveUnit;
    }


    [Command]
    public void CmdTryPlaceBuilding(int buildingId, Vector3 point)
    {
        GameObject buildingInstance = Instantiate(buildingsFactory.GetBuildingById(buildingId), point, Quaternion.identity);

        NetworkServer.Spawn(buildingInstance, connectionToClient);
    }
    #endregion


    void AddUnit(Unit unit)
    {
        if(!unit.hasAuthority && unit.id != Macros.Units.WORKER)
            m_units.Add(unit);
    }


    void RemoveUnit(Unit unit)
    {
        if (!unit.hasAuthority)
            m_units.Remove(unit);
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
