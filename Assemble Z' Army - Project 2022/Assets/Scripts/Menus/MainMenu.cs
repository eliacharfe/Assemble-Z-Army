using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject landingPagePanel = null;

      AudioPlayer audioPlayer;

     public void HostLobby()
     {
         FindObjectOfType<AudioPlayer>().PlayBtnClickClip();

         landingPagePanel.SetActive(false);

         NetworkManager.singleton.StartHost();

     }


}
