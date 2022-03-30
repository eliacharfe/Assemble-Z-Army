using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject landingPagePanel = null;

      AudioPlayer audioPlayer;

    // private void Awake()
    // {
    //     audioPlayer = FindObjectOfType<AudioPlayer>();
    // }
 

     public void HostLobby()
     {
         audioPlayer = FindObjectOfType<AudioPlayer>();
        audioPlayer.PlayBtnClickClip();

         landingPagePanel.SetActive(false);

         NetworkManager.singleton.StartHost();

     }


}
