using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Steamworks;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject landingPagePanel = null;

    // We can choose use steam or mirror Transport
    [SerializeField] private bool useSteam = false;

    // Built in steam call backs

    // Steam callback when lobby is created.
    protected Callback<LobbyCreated_t> lobbyCreated;

    // Steam callback when requasted to join.
    protected Callback<GameLobbyJoinRequested_t> gameLobbyJoinRequasted;

    // Steam callback when player entered lobby.
    protected Callback<LobbyEnter_t> lobbyEntered;

    //Consts
    private string STEAM_HOST_KEY = "HostAddress";

    AudioPlayer audioPlayer;

    private void Start()
    {
        if (!useSteam) { return; }

        lobbyCreated = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
        gameLobbyJoinRequasted = Callback<GameLobbyJoinRequested_t>.Create(OnGameLobbyJoinRequasted);
        lobbyEntered = Callback<LobbyEnter_t>.Create(OnLobbyEntered);
    }

    public void HostLobby()
     {
        FindObjectOfType<AudioPlayer>().PlayBtnClickClip();

        landingPagePanel.SetActive(false);

        if (useSteam)
        {
            SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, 4);
            return;
        }

        NetworkManager.singleton.StartHost();

    }


    // When lobby is created , start hosting the game.
    private void OnLobbyCreated(LobbyCreated_t callback)
    {
        if (callback.m_eResult != EResult.k_EResultOK)
        {
            landingPagePanel.SetActive(true);
            return;
        }

        NetworkManager.singleton.StartHost();
        SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), "HostAddress", SteamUser.GetSteamID().ToString());

    }


    // When player join the game.
    private void OnGameLobbyJoinRequasted(GameLobbyJoinRequested_t callback)
    {
        SteamMatchmaking.JoinLobby(callback.m_steamIDLobby);
    }

    // When player entering the lobby add client to network manager.
    private void OnLobbyEntered(LobbyEnter_t callback)
    {
        if (NetworkServer.active) { return; }

        string hostAddress = SteamMatchmaking.GetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), "HostAddress");

        NetworkManager.singleton.networkAddress = hostAddress;
        NetworkManager.singleton.StartClient();

        landingPagePanel.SetActive(false);
    }



}
