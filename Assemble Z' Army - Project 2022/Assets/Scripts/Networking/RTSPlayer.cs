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

    public static event Action<bool> AuthorityOnPartyOwnerStateUpdated;

    // Start is called before the first frame update
    void Start()
    {
        // buildingsFactory = FindObjectOfType<BuildingsFactory>();
    }

    // Update is called once per frame
    void Update()
    {

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
    public void SetPartyOwner(bool state)
    {
        isPartyOwner = state;
    }



    #endregion



    #region Client

    public override void OnStartClient()
    {
        if (NetworkServer.active) { return; }

           ((RtsNetworkManager)NetworkManager.singleton).Players.Add(this);
    }

    public override void OnStopClient()
    {
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



    #endregion


}
