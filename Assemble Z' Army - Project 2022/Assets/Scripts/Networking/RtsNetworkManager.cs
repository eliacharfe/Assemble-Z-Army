using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Macros;
using UnityEngine.SceneManagement;
using Utilities;
using System;

public class RtsNetworkManager : NetworkManager
{
    [Header ("Workers")]
    [SerializeField] int amountOfWorkers = 10;

    [Header("Recruits")]
    [SerializeField] int amountOfRecruits = 10;

    [SerializeField] GameObject gameOverHandler = null;

    public List<RTSPlayer> players = new List<RTSPlayer>();

    // Client connections events
    public static event Action ClientOnConnected;
    public static event Action ClientOnDisConnected;

    # region Server 

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        base.OnServerAddPlayer(conn);

        RTSPlayer player = conn.identity.GetComponent<RTSPlayer>();
        print("Player have been added");
        players.Add(player);

        //GameObject baseInstance = Instantiate(spawnerPrefab,
              //player.transform.position, Quaternion.identity);

        //NetworkServer.Spawn(baseInstance, player.connectionToClient);

        //player.unitSpawner = baseInstance;

        player.SetPartyOwner(players.Count == 1);

        //player.SetCameraPosition(position);

        if(players.Count >= 2)
        {
           FindObjectOfType<PhaseManager>().SetTimer(true);

           SetPhaseOne();
        }
    }

    
    public override void OnServerDisconnect(NetworkConnection conn)
    {
        players.Remove(conn.identity.GetComponent<RTSPlayer>());
        base.OnServerDisconnect(conn);

        // Client is not destroyed by
       // Destroy(conn.identity.gameObject);
    }


    public override void OnServerSceneChanged(string sceneName)
    {

        GameObject  EndGameHandler = Instantiate(gameOverHandler);

        // Spawn the player on server.
        NetworkServer.Spawn(EndGameHandler);

        foreach (RTSPlayer player in players)
        {

            var startPos = GetStartPosition().position;

            var pos = Utilities.Utils.ChangeZAxis(startPos, -5);

            player.phaseThreePos = pos;

            player.SpawnRecruitedUnit();

            //GameObject baseInstance = Instantiate(spawnerPrefab,
            // startPos, Quaternion.identity);

            // Spawn the player on server.
            //NetworkServer.Spawn(baseInstance, player.connectionToClient);

        }

    }



    // Set wokers units which suppose to build with given resources avaible.
    public void SetPhaseOne()
    {
        UnitsFactory factory = gameObject.GetComponent<UnitsFactory>();

        foreach (RTSPlayer player in players)
        {

            Vector3 startinPoint = player.transform.position;

            for (int i = 0; i < amountOfWorkers; i++)
            {

                GameObject workerInstance = Instantiate(factory.GetUnitPrefab(Macros.Units.WORKER).gameObject, startinPoint, Quaternion.identity);

                // Spawn the player on server.
                NetworkServer.Spawn(workerInstance, player.connectionToClient);

                player.m_workers.Add(workerInstance);
            }
        }

    }

    // Set wokers units which suppose to build with given resources avaible.
    public void SetPhaseTwo()
    {
        UnitsFactory factory = gameObject.GetComponent<UnitsFactory>();

        foreach (RTSPlayer player in players)
        {

            Vector3 startinPoint = player.transform.position;

            player.removeWorkers();

            for (int i = 0; i < amountOfRecruits; i++)
            {
                GameObject RecruitsInstance = Instantiate(factory.GetUnitPrefab(Macros.Units.RECRUIT).gameObject, startinPoint, Quaternion.identity);

                // Spawn the player on server.
                NetworkServer.Spawn(RecruitsInstance, player.connectionToClient);
            }
        }

    }







    public void ShowBattleField()
    {
        NetworkManager.singleton.ServerChangeScene("Battlefield");
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



}
