using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;


public class RTSPlayer : NetworkBehaviour
{
    [SyncVar(hook = nameof(AuthorityHandlePartyOwnerStateUpdated))]
    private bool isPartyOwner = false;

    public event Action<int> ClientOnResourcesUpdated;

  public static event Action ClientOnInfoUpdated;
    public static event Action<bool> AuthorityOnPartyOwnerStateUpdated;

    [SyncVar(hook = nameof(ClientHandleDisplayNameUpdated))]
    private string displayName;

    public string GetDisplayName()
    {
        return displayName;
    }

    public bool GetIsPartyOwner()
    {
        return isPartyOwner;
    }

    [Command]
    public void CmdStartGame()
    {
        if (!isPartyOwner) { return; }

            ((RtsNetworkManager)NetworkManager.singleton).StartGame();
    }


    #region Server

    [Server]
    public void SetDisplayName(string displayName)
    {
        this.displayName = displayName;
    }


    [Server]
    public void SetPartyOwner(bool state)
    {
        isPartyOwner = state;
    }

    public override void OnStartServer()
    {


        DontDestroyOnLoad(gameObject);
    }


    #endregion



    #region Client

    public override void OnStartClient()
    {
        if (NetworkServer.active) { return; }

        DontDestroyOnLoad(gameObject);

        ((RtsNetworkManager)NetworkManager.singleton).Players.Add(this);
    }

    public override void OnStopClient()
    {
         ClientOnInfoUpdated?.Invoke();

        if (!isClientOnly || !hasAuthority) { return; }
        if (!isClientOnly) { return; }

       ((RtsNetworkManager)NetworkManager.singleton).Players.Remove(this);

        if (!hasAuthority) { return; }
    }


    private void AuthorityHandlePartyOwnerStateUpdated(bool oldState, bool newState)
    {
        if (!hasAuthority) { return; }

        AuthorityOnPartyOwnerStateUpdated?.Invoke(newState);
    }


    public void ClientHandleDisplayNameUpdated(string oldDisplayName, string newDisplayName)
    {
         ClientOnInfoUpdated?.Invoke();
    }

    #endregion


}
