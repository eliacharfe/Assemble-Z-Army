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

        Debug.Log("Given player " + players.Count + " position: " + player.transform.position);

        NetworkServer.Spawn(baseInstance, player.connectionToClient);

        player.SetPartyOwner(players.Count == 1);

        //player.SetCameraPosition(position);

        if(players.Count >= 2)
        {
           //FindObjectOfType<PhaseManager>().SetTimer(true);
        }
    }
    public override void OnServerChangeScene(string newSceneName)
    {

        Debug.Log(newSceneName + " !!" + SceneManager.GetActiveScene().name);

        if (newSceneName == "Battlefield")
        {
            Debug.Log("Changing to battlefield scene");
            foreach (RTSPlayer player in players)
            {
                player.HideUnits();
            }

        }

    }
    #endregion


    #region Client
    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);

        ClientOnConnected?.Invoke();
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);

        ClientOnDisConnected?.Invoke();
    }

    #endregion




    public void ShowBattleField()
    {
        //NetworkManager.singleton.ServerChangeScene("Battlefield");
    }

}
