using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Mirror;

public class JoinLobbyMenu : MonoBehaviour
{
    [SerializeField] private GameObject landingPagePanel = null;
    [SerializeField] private TMP_InputField addressInput = null;
    [SerializeField] private Button joinButton = null;

    AudioPlayer audioPlayer;

    private void OnEnable()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        RtsNetworkManager.ClientOnConnected += HandleClientConnected;
        RtsNetworkManager.ClientOnDisConnected += HandleClientDisconnected;
    }

    private void OnDisable()
    {
        RtsNetworkManager.ClientOnConnected -= HandleClientConnected;
        RtsNetworkManager.ClientOnDisConnected -= HandleClientDisconnected;
    }

    public void Join()
    {
        audioPlayer.PlayBtnClickClip();

        string address = addressInput.text;

        NetworkManager.singleton.networkAddress = address;
        NetworkManager.singleton.StartClient();

        joinButton.interactable = false;
    }

    private void HandleClientConnected()
    {
        audioPlayer.PlayBtnClickClip();

        joinButton.interactable = true;

        gameObject.SetActive(false);
        landingPagePanel.SetActive(false);
    }

    private void HandleClientDisconnected()
    {
        joinButton.interactable = true;
    }

    public void PlaySoundClick()
    {
        audioPlayer.PlayBtnClickClip();
    }
}
