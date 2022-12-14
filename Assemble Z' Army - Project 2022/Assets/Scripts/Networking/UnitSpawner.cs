using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class UnitSpawner : NetworkBehaviour
{
    [SerializeField] GameObject unitPrefab;


    private void Start()
    {
        
    }

    public override void OnStartClient()
    {
       
    }

    [Command]
    void CmdSpawnAUnit()
    {
        GameObject unit = Instantiate(unitPrefab, transform.position, Quaternion.identity);

        NetworkServer.Spawn(unit, connectionToClient);
    }

    private void OnMouseDown()
    {
        CmdSpawnAUnit();
        if (isLocalPlayer)
        {

            Debug.Log("Mouse clicked on spawner");
        }
    }
}
