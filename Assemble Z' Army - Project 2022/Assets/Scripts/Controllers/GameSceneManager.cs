using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager:MonoBehaviour
{
    const int
        MENU_SCENE = 0,
        NETWORK_SCENE_PLAYGROUND = 1,
        OFFLINE_SCENE_PLAYGROUND =2;

    // TODO - For testing only.
    public void LoadNetworkScene()
    {
        SceneManager.LoadScene(NETWORK_SCENE_PLAYGROUND);
    }

    // TODO - For testing only.
    public void LoadOfflineScene()
    {
        SceneManager.LoadScene(OFFLINE_SCENE_PLAYGROUND);
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene(MENU_SCENE);
    }
    

    public void ExitGame()
    {
        Application.Quit();
    }


}
