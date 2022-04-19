using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;

public class BuilidingConstruction : NetworkBehaviour
{
    [SerializeField] private CostumeSlider buldingConstructionSlider = null;
    public float constructionTime = 5f;
    [SerializeField] [SyncVar(hook =nameof(HandleConstructionUpdated))] float timePassed = 0;

    public event Action<float, float> ClientOnConstructionUpdated;

    public Texture2D hammerCursor;
    public Texture2D regularCursor;

    private void Start()
    {
        buldingConstructionSlider.resetSlider();
    }

    // Todo - increase time by worker.
    private void Update()
    {
        if(buldingConstructionSlider.FillAmount() >= 1f) {
            FinishConstruction();
        }
    }

    private void OnMouseEnter()
    {
        //Cursor.SetCursor(hammerCursor, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseExit()
    {
        //Cursor.SetCursor(regularCursor, Vector2.zero, CursorMode.Auto);
    }

    #region Server

    // Add building time.
    [Command]
    public void CmdIncreasingBuildingTime(float value)
    {
        timePassed += value / constructionTime;
    }
    #endregion


    // Enable building functionallity and disable construction
    // elements.
    private void FinishConstruction()
    {
        GetComponent<Building>().enabled = true;
        buldingConstructionSlider.gameObject.SetActive(false);
        this.enabled = false;
    }




    // Get building time passed.
    private float GetBuildingConstructionTime()
    {
        return buldingConstructionSlider.FillAmount();
    }


    private void HandleConstructionUpdated(float oldTime,float newTime)
    {
        buldingConstructionSlider.setValue(newTime);
    }

}
