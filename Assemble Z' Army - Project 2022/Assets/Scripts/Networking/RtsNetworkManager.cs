using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class RtsNetworkManager : NetworkManager
{
    [SerializeField] GameObject spawnerPrefab = null;

    public List<RTSPlayer> players = new List<RTSPlayer>();
    //Temporary
    

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        base.OnServerAddPlayer(conn);

        RTSPlayer player = conn.identity.GetComponent<RTSPlayer>();

        players.Add(player);
        
        GameObject baseInstance = Instantiate(spawnerPrefab,
               GetStartPosition().position, Quaternion.identity);

        NetworkServer.Spawn(baseInstance, player.connectionToClient);

        player.SetPartyOwner(players.Count == 1);

        player.SetCameraPosition(base.transform.position);
    }
    

    public override void OnServerChangeScene(string newSceneName)
    {

        Debug.Log(newSceneName + " !!" + SceneManager.GetActiveScene().name);

        if (newSceneName == "Battlefield")
        {
            Debug.Log("Changing to battlefield scene");
            foreach(RTSPlayer player in players)
            {
                player.HideUnits();
            }

        }

    }


    public void ShowBattleField()
    {
        NetworkManager.singleton.ServerChangeScene("Battlefield");
    }

}
