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
        //UnitBase.ServerOnBaseDeSpawned -= ServerHandleBaseDeSpawned;
    }


    private void ServerHandleBaseSpawned(GameObject unitBase)
    {
        Debug.Log("Base Added ");
        // bases.Add(unitBase);

    }


    private void ServerHandleUnitDeSpawned(Unit unit)
    {
        ServerOnGameOver?.Invoke();
    }


    private void ServerHandlePlayerLost(int playerId)
    {

        print("The player with connection:" + playerId + "has lost the game");
        //ClientOnGameOver?.Invoke("The playr won is:" + playerId);

        RpcGameOver("The playr won is:" + playerId);
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
