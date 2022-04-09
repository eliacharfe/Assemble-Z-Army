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


    private void ServerHandleBaseSpawned(GameObject unitBase){}


    private void ServerHandleUnitDeSpawned(Unit unit)
    {
        ServerOnGameOver?.Invoke();
    }


    private void ServerHandlePlayerLost(int playerId)
    {
        var players = ((RtsNetworkManager)NetworkManager.singleton).players;

        print("Players lost amount " + players.FindAll(player => player.isPlayerLost).Count);

        if (players.FindAll(player => player.isPlayerLost).Count == players.Count-1)
        {
            var winnerId = players.Find(player => !player.isPlayerLost).GetDisplayName();

            RpcGameOver("The Winner is:" + winnerId);
        }

    }

    #endregion


    #region Client

    // Notify all players the game is over.
    [ClientRpc]
    private void RpcGameOver(string winner)
    {

        print(winner);

        ClientOnGameOver?.Invoke(winner);
    }

    #endregion
}
