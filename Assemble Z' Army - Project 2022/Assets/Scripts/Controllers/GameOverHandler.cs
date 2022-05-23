using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class GameOverHandler : NetworkBehaviour
{
    public static event Action ServerOnGameOver;
    public static event Action<string> ClientOnGameOver;

    #region Server
    public override void OnStartServer()
    {
        Unit.ServerOnUnitDeSpawned += ServerHandleUnitDeSpawned;
        RTSPlayer.PlayerLostAllUnits += ServerHandlePlayerLost;
    }

    public override void OnStopServer()
    {
        Unit.ServerOnUnitDeSpawned -= ServerHandleUnitDeSpawned;
        RTSPlayer.PlayerLostAllUnits -= ServerHandlePlayerLost;
    }

    private void ServerHandleUnitDeSpawned(Unit unit)
    {
        ServerOnGameOver?.Invoke();
    }

    private void ServerHandlePlayerLost(int playerId)
    {
        // Check if one player left in the game with units,if does Rpc winner player name.
        List<RTSPlayer> players = ((RtsNetworkManager)NetworkManager.singleton).players;
        if (players.FindAll(player => player.isPlayerLost).Count == players.Count-1)
        {
            string winnerId = players.Find(player => !player.isPlayerLost).GetDisplayName();
            RpcGameOver("The Winner is: " + winnerId);
        }

    }
    #endregion

    #region Client
    // Notify all players the game is over.
    [ClientRpc]
    private void RpcGameOver(string winner)
    {
        ClientOnGameOver?.Invoke(winner);
        AudioPlayer audioPlayer = FindObjectOfType<AudioPlayer>();

        if (audioPlayer)
        {
            audioPlayer.StopMusic();
            audioPlayer.PlayEndGameSound();
        }
    }

    #endregion
}
