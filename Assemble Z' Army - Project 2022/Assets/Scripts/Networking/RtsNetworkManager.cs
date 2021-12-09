using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class RtsNetworkManager : NetworkManager
{
    [SerializeField] GameObject spawnerPrefab = null;

    //Temporary
    int playerCount = 0;


    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        base.OnServerAddPlayer(conn);

        Debug.Log(conn.identity.GetComponent<RTSPlayer>());
        
        GameObject baseInstance = Instantiate(spawnerPrefab,
               GetStartPosition().position, Quaternion.identity);

        RTSPlayer player = conn.identity.GetComponent<RTSPlayer>();

        NetworkServer.Spawn(baseInstance, player.connectionToClient);
    }

}
