using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TeamColorSetter : NetworkBehaviour
{
    // Store the renderes which need to be colored.
    [SerializeField] private SpriteRenderer[] colorRendereres = new SpriteRenderer[0];

    // Update the team color.
    [SyncVar(hook = nameof(HandleTeamColorUpdated))]
    private Color teamColor = new Color();


    #region Server 
    public override void OnStartServer()
    {
        RTSPlayer player = connectionToClient.identity.GetComponent<RTSPlayer>();
        teamColor = player.GetTeamColor();
    }

    #endregion


    #region Client
    // Update the color.
    private void HandleTeamColorUpdated(Color oldColor, Color newColor)
    {
        foreach (SpriteRenderer renderer in colorRendereres)
        {
            renderer.color = newColor;
        }

    }

    #endregion
}
