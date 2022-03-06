using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;
using Utilities;
using System;

public class RtsNetworkManager : NetworkManager
{
    [SerializeField] GameObject spawnerPrefab = null;

    public List<RTSPlayer> players = new List<RTSPlayer>();

    // Client connections events
    public static event Action ClientOnConnected;
    public static event Action ClientOnDisConnected;

    # region Server 
    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        base.OnServerAddPlayer(conn);

        RTSPlayer player = conn.identity.GetComponent<RTSPlayer>();

        players.Add(player);

        GameObject baseInstance = Instantiate(spawnerPrefab,
              player.transform.position, Quaternion.identity);

        NetworkServer.Spawn(baseInstance, player.connectionToClient);

        player.SetPartyOwner(players.Count == 1);

        //player.SetCameraPosition(position);

        if(players.Count >= 1)
        {
           FindObjectOfType<PhaseManager>().SetTimer(true);
        }
    }
    public override void OnServerChangeScene(string newSceneName)
    {

        if (newSceneName == "Battlefield")
        {
            foreach (RTSPlayer player in players)
            {
                //player.ShowUnits(false);

                //print("New pos given is:" + pos.position);
            }

        }

    }


    public override void OnServerSceneChanged(string sceneName)
    {

        foreach (RTSPlayer player in players)
        {

            var startPos = GetStartPosition().position;

            var pos = Utilities.Utils.ChangeZAxis(startPos, -5);

            player.ShowUnits(false);

            player.SetUnitsPositions(startPos);

            player.phaseThreePos = pos;

            GameObject baseInstance = Instantiate(spawnerPrefab,
             startPos, Quaternion.identity);

            // Spawn the player on server.
            NetworkServer.Spawn(baseInstance, player.connectionToClient);
        }

    }
    #endregion


    #region Client
    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);

        ClientOnConnected?.Invoke();
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 100, 100), "SOME");
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);

        ClientOnDisConnected?.Invoke();
    }
    #endregion




    public void ShowBattleField()
    {
        print("Changed to battlefield");
        NetworkManager.singleton.ServerChangeScene("Battlefield");
    }

}
