using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyMenu : MonoBehaviour
{
    [SerializeField] private GameObject lobbyUI = null;
    [SerializeField] private Button startGameButton = null;
    [SerializeField] private TMP_Text[] playersNamesText = new TMP_Text[4];

    AudioPlayer audioPlayer;


    private void Start()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        RtsNetworkManager.ClientOnConnected += HandleClientConnected;
        RTSPlayer.AuthorityOnPartyOwnerStateUpdated += AuthorityHandlePartyOwnerStateUpdated;
        RTSPlayer.ClientOnInfoUpdated += ClientHandleInfoUpdated;
    }

    private void OnDestroy()
    {
        RtsNetworkManager.ClientOnConnected -= HandleClientConnected;
        RTSPlayer.AuthorityOnPartyOwnerStateUpdated -= AuthorityHandlePartyOwnerStateUpdated;
        RTSPlayer.ClientOnInfoUpdated -= ClientHandleInfoUpdated;
    }

    private void HandleClientConnected()
    {
        lobbyUI.SetActive(true);
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 100, 100), "SOME");
    }

    private void AuthorityHandlePartyOwnerStateUpdated(bool state)
    {
        startGameButton.gameObject.SetActive(state);
    }

    private void ClientHandleInfoUpdated()
    {
       List<RTSPlayer> players = ((RtsNetworkManager)NetworkManager.singleton).players;

        for (int i = 0; i < players.Count; i++)
        {
            playersNamesText[i].text = players[i].GetDisplayName();
        }

        for (int i = players.Count; i < playersNamesText.Length; i++)
        {
            playersNamesText[i].text = "Waiting For Player...";
        }

        startGameButton.interactable = players.Count >= 1;
        startGameButton.gameObject.SetActive(true);
    }

    public void StartGame()
    {
        //  audioPlayer.PlayBtnClickClip();
        print("Is player null?" + NetworkClient.connection.identity.GetComponent<RTSPlayer>());
        NetworkClient.connection.identity.GetComponent<RTSPlayer>().CmdStartGame();
    }

    public void LeaveLobby()
    {
         audioPlayer.PlayBtnClickClip();

        if (NetworkServer.active && NetworkClient.isConnected)
        {
            NetworkManager.singleton.StopHost();
        }
        else
        {
            NetworkManager.singleton.StopClient();

            SceneManager.LoadScene(0);
        }
    }
}
