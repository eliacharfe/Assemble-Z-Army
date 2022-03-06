using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseTwoManager : NetworkBehaviour
{
    public override void OnStartServer()
    {
        print(isServer);
        print(isClient);
        print("PhaseTwoServerStarted");
        clientRPCTest();
    }



    public override void OnStartClient()
    {
        //print("The client should change camera!!! ");

        //RTSPlayer localPlayer = NetworkClient.localPlayer.GetComponent<RTSPlayer>();

        //localPlayer.SetCameraPosition(new Vector3(0, 0, -1));
        // clientRPCTest();

        //RTSPlayer player = connectionToServer.identity.GetComponent<RTSPlayer>();

        // player.SetCameraPosition(gameObject.transform.position);
    }

    [ClientRpc]
    public void clientRPCTest()
    {
        print("PhaseTwoStarted ");

        RTSPlayer localPlayer = NetworkClient.localPlayer.GetComponent<RTSPlayer>();

        localPlayer.SetCameraPosition(new Vector3(0,0,0));
    }
}
