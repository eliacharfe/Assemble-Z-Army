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
    [SerializeField]private int amountOfWorkers = 10;

    [Header("Recruits")]
    [SerializeField]private int amountOfRecruits = 10;

    [SerializeField]private GameObject gameOverHandler = null;
    [SerializeField]public List<RTSPlayer> players = new List<RTSPlayer>();

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

        player.SetTeamColor(Constents.teamColors[players.Count-1]);

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
        if(sceneName == Scenes.PREPERATION_SCENE)
        {
            SetPhaseOne();
        }

        if (sceneName == Scenes.BATTLEFIELD_SCENE)
        {
            SetPhaseThree();
        }
    }

    // Set workers units.
    public void SetPhaseOne()
    {
        UnitsFactory factory = FindObjectOfType<UnitsFactory>();
        FindObjectOfType<PhaseManager>().SetTimer(true);

        foreach (RTSPlayer player in players)
        {
            var startPos = GetStartPosition().position;
            List<Vector3> posList = Utilities.Utils.GetCircleForamtionPosList(startPos, new float[] { 5, 10, 15 }, new int[] { 5, 10, 15 });
            int posIndex = 0;
            var pos = Utilities.Utils.ChangeZAxis(startPos, -5);

            player.transform.position = startPos;
            player.phaseThreePos = pos;

            for (int i = 0; i < amountOfWorkers; i++)
            {
                GameObject workerInstance = Instantiate(factory.GetUnitPrefab(Units.WORKER).gameObject, posList[posIndex], Quaternion.identity);
                posIndex = (posIndex +1) % posList.Count;
                NetworkServer.Spawn(workerInstance, player.connectionToClient);
                player.m_workers.Add(workerInstance);
            }
        }
    }

    // Set recruits units.
    public void SetPhaseTwo()
    {
        UnitsFactory factory = FindObjectOfType<UnitsFactory>();

        foreach (RTSPlayer player in players)
        {
            Vector3 startinPoint = player.transform.position;
            player.removeWorkers();
            List<Vector3> posList = Utilities.Utils.GetCircleForamtionPosList(startinPoint, new float[] { 5, 10, 15 }, new int[] { 5, 10, 15 });
            int posIndex = 0;

            for (int i = 0; i < amountOfRecruits; i++)
            {
                GameObject RecruitsInstance = Instantiate(factory.GetUnitPrefab(Units.RECRUIT).gameObject, posList[posIndex], Quaternion.identity);
                NetworkServer.Spawn(RecruitsInstance, player.connectionToClient);
                posIndex = (posIndex + 1) % posList.Count;
            }
        }

    }

    void spawnTemp(Macros.Units name, UnitsFactory factory, RTSPlayer player, Vector3 pos)
    {
        GameObject workerInstance = Instantiate(factory.GetUnitPrefab(name).gameObject, pos, Quaternion.identity);

        // Spawn the player on server.
        NetworkServer.Spawn(workerInstance, player.connectionToClient);
    }

    // Spawn gathered units in battlefield.
    public void SetPhaseThree()
    {
        GameObject EndGameHandler = Instantiate(gameOverHandler);
        NetworkServer.Spawn(EndGameHandler);

        foreach (RTSPlayer player in players)
        {
            var startPos = GetStartPosition().position;
            var pos = Utilities.Utils.ChangeZAxis(startPos, -5);
            // Update the player camera position.
            player.phaseThreePos = pos;
            player.SpawnRecruitedUnit();
        }
    }

    // Phase One and Two.
    public void ShowPreparationPhase()
    {
        NetworkManager.singleton.ServerChangeScene(Scenes.PREPERATION_SCENE);   
    }

    // Phase Three.
    public void ShowBattlefieldPhase()
    {
        NetworkManager.singleton.ServerChangeScene(Scenes.BATTLEFIELD_SCENE);
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



}
