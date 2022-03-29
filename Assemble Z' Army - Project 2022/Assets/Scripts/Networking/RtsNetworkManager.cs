using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class RtsNetworkManager : NetworkManager
{
    [SerializeField] GameObject spawnerPrefab = null;

    //Temporary
    int playerCount = 0;

    public static event Action ClientOnConnected;
    public static event Action ClientOnDisconnected;

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);

        ClientOnConnected?.Invoke();
    }

     public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);

        ClientOnDisconnected?.Invoke();
    }


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
