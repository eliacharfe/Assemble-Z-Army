using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class RTSPlayer : NetworkBehaviour
{

    public List<Unit> m_units = new List<Unit>();

    private BuildingsFactory buildingsFactory = null;


    // Start is called before the first frame update
    void Start()
    {
        buildingsFactory = FindObjectOfType<BuildingsFactory>();
    }



    // Update is called once per frame
    void Update()
    {
        
    }


    #region Server
    public override void OnStartServer()
    {
        Unit.ServerOnUnitSpawned += addUnit;
        Unit.ServerOnUnitDeSpawned += removeUnit;
    }

    public override void OnStopServer()
    {
        Unit.ServerOnUnitSpawned -= addUnit;
        Unit.ServerOnUnitDeSpawned -= removeUnit;
    }

    #endregion

    #region Client

    public override void OnStartAuthority()
    {
        Unit.AuthortyOnUnitSpawned += addUnit;
        Unit.AuthortyOnUnitDeSpawned += removeUnit;
    }
    public override void OnStopAuthority()
    {
        Unit.AuthortyOnUnitSpawned -= addUnit;
        Unit.AuthortyOnUnitDeSpawned -= removeUnit;
    }

    [Command]
    public void CmdTryPlaceBuilding(int buildingId, Vector3 point)
    {
        Debug.Log(buildingId + " " + point);

        GameObject buildingInstance = Instantiate(buildingsFactory.GetBuildingById(buildingId), point, Quaternion.identity);

        NetworkServer.Spawn(buildingInstance, connectionToClient);
    }

    #endregion
    void addUnit(Unit unit)
    {
        if(!unit.hasAuthority && unit.id != Macros.Units.WORKER)
            m_units.Add(unit);
    }

    void removeUnit(Unit unit)
    {
        if (!unit.hasAuthority)
            m_units.Remove(unit);
    }





  


}
