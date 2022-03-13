using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.EventSystems;

public class UnitSpawner : NetworkBehaviour
{
    [SerializeField] GameObject unitPrefab;


    private void Start()
    {
        
    }

    public override void OnStartClient()
    {
       
    }

    public override void OnStopServer()
    {
        print("Spawner destroyed on server");
        Destroy(gameObject);
    }

    public override void OnStopClient()
    {
        print("Spawner destroyed on client");
        Destroy(gameObject);
    }


    [Command]
    void CmdSpawnAUnit()
    {
        GameObject unit = Instantiate(unitPrefab, transform.position, Quaternion.identity);

        NetworkServer.Spawn(unit, connectionToClient);

        print("The client owned this unit is:" + connectionToClient);
    }

    private void OnMouseDown()
    {
        CmdSpawnAUnit();
    }
}
