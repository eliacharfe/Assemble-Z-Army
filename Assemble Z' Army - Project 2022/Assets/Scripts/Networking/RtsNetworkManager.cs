using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RtsNetworkManager : NetworkManager
{
    [SerializeField] GameObject spawnerPrefab = null;

    //Temporary
    int playerCount = 0;

    public static event Action ClientOnConnected;
    public static event Action ClientOnDisconnected;


    private bool isGameInProgress = false;

    public List<RTSPlayer> Players { get; } = new List<RTSPlayer>();

     AudioPlayer audioPlayer;


    #region Server

    public override void OnServerConnect(NetworkConnection conn)
    {

        audioPlayer = FindObjectOfType<AudioPlayer>();
        audioPlayer.PlayBtnClickClip();

        if (!isGameInProgress) { return; }

        conn.Disconnect();
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {

              audioPlayer = FindObjectOfType<AudioPlayer>();
        audioPlayer.PlayBtnClickClip();

        RTSPlayer player = conn.identity.GetComponent<RTSPlayer>();

        Players.Remove(player);

        base.OnServerDisconnect(conn);
    }

    public override void OnStopServer()
    {
        Players.Clear();

        isGameInProgress = false;
    }

    public void StartGame()
    {
       // audioPlayer.PlayBtnClickClip();
        if (Players.Count < 2) { return; }

        isGameInProgress = true;

        ServerChangeScene("WarScene"); // to change name here ****************************************
    }

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
    
      //  audioPlayer.PlayBtnClickClip();
    
 
        base.OnServerAddPlayer(conn);

        RTSPlayer player = conn.identity.GetComponent<RTSPlayer>();

        Players.Add(player);

        player.SetDisplayName($"Player {Players.Count}");

        // player.SetTeamColor(new Color(
        //     UnityEngine.Random.Range(0f, 1f),
        //     UnityEngine.Random.Range(0f, 1f),
        //     UnityEngine.Random.Range(0f, 1f)
        // ));

        player.SetPartyOwner(Players.Count == 1);
    }

    public override void OnServerSceneChanged(string sceneName)
    {
        if (SceneManager.GetActiveScene().name.StartsWith("WarScene")) // to change naame here ****************************************
        {
            // GameOverHandler gameOverHandlerInstance = Instantiate(gameOverHandlerPrefab);
            // NetworkServer.Spawn(gameOverHandlerInstance.gameObject);

            foreach (RTSPlayer player in Players)
            {
                GameObject baseInstance = Instantiate(
                    spawnerPrefab,
                    GetStartPosition().position,
                    Quaternion.identity);

                NetworkServer.Spawn(baseInstance, player.connectionToClient);
            }
        }
    }

    #endregion

    #region Client

    public override void OnClientConnect(NetworkConnection conn)
    {
      audioPlayer = FindObjectOfType<AudioPlayer>();
        audioPlayer.PlayBtnClickClip();

        base.OnClientConnect(conn);

        ClientOnConnected?.Invoke();
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);

        ClientOnDisconnected?.Invoke();
    }

    public override void OnStopClient()
    {
        Players.Clear();
    }

    #endregion
}
