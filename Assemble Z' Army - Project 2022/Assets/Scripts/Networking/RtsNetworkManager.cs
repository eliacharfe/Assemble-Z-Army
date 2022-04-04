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

    // The progession of the game.
    private bool isGameInProgess = false;

    # region Server 

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);

        RTSPlayer player = conn.identity.GetComponent<RTSPlayer>();

        players.Add(player);

        player.SetDisplayName($"Player {players.Count}");

        player.SetTeamColor(UnityEngine.Random.ColorHSV());

        player.SetPartyOwner(players.Count == 1);
    }

    public override void OnServerConnect(NetworkConnectionToClient conn)
    {
        if (isGameInProgess)
            conn.Disconnect();
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        players.Remove(conn.identity.GetComponent<RTSPlayer>());
        base.OnServerDisconnect(conn);
    }


    public override void OnServerSceneChanged(string sceneName)
    {
        if(sceneName == "Playground")
        {
            SetPhaseOne();
        }


        if (sceneName == "Battlefield")
        {
            GameObject EndGameHandler = Instantiate(gameOverHandler);

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



    }



    // Set wokers units which suppose to build with given resources avaible.
    public void SetPhaseOne()
    {
        UnitsFactory factory = FindObjectOfType<UnitsFactory>();

        FindObjectOfType<PhaseManager>().SetTimer(true);

        print("Setting phase one");

        foreach (RTSPlayer player in players)
        {
            var startPos = GetStartPosition().position;

            var pos = Utilities.Utils.ChangeZAxis(startPos, -5);

            player.transform.position = startPos;

            player.phaseThreePos = pos;

            print("Setting camera in "+pos);

            for (int i = 0; i < amountOfWorkers; i++)
            {

                GameObject workerInstance = Instantiate(factory.GetUnitPrefab(Macros.Units.WORKER).gameObject, startPos, Quaternion.identity);

                // Spawn the player on server.
                NetworkServer.Spawn(workerInstance, player.connectionToClient);

                player.m_workers.Add(workerInstance);
            }
        }

        print("Phase one ready");
    }

    // Set wokers units which suppose to build with given resources avaible.
    public void SetPhaseTwo()
    {
        UnitsFactory factory = FindObjectOfType<UnitsFactory>();

        foreach (RTSPlayer player in players)
        {

            Vector3 startinPoint = player.transform.position;

            player.removeWorkers();

            for (int i = 0; i < amountOfRecruits; i++)
            {
                GameObject RecruitsInstance = Instantiate(factory.GetUnitPrefab(Macros.Units.SWORDMAN).gameObject, startinPoint, Quaternion.identity);

                // Spawn the player on server.
                NetworkServer.Spawn(RecruitsInstance, player.connectionToClient);
            }

            GameObject Healer = Instantiate(factory.GetUnitPrefab(Macros.Units.HEALER).gameObject, startinPoint, Quaternion.identity);

            // Spawn the player on server.
            NetworkServer.Spawn(Healer, player.connectionToClient);
        }

    }







    public void ShowPreparationPhase()
    {
        print("Changing server");

        NetworkManager.singleton.ServerChangeScene("Playground");   
    }

    public void ShowBattlefieldPhase()
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
