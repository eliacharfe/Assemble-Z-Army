using System.Collections;
using System.Collections.Generic;
using Macros;
using UnityEngine;
using TMPro;
using Mirror;
using System;

public class PhaseManager : NetworkBehaviour
{
    [SyncVar] public float currentTime = 0f;
    [SyncVar] public float timer = 1f;
    [SyncVar] public int currentPhase = 1;
    [SyncVar] private bool startTimer = false;
    [SerializeField] private float phaseOneTime = 5f;
    [SerializeField] private float phaseTwoTime = 5f;

    //Canvas
    [SerializeField] GameObject phaseOneStartingCanvas = null;
    [SerializeField] GameObject BuildingCanvasPanel = null;
    [SerializeField] GameObject phaseOneEndedCanvas = null;
    [SerializeField] GameObject phaseTwoEndedCanvas = null;

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI currentPhaseDisplay;


    public override void OnStartServer()
    {
        base.OnStartServer();
        timer = phaseOneTime;
        currentTime = phaseOneTime;
    }

    public override void OnStopServer()
    {
        base.OnStopServer();
        Destroy(this);
    }


    public void SetTimer(bool value)
    {
        startTimer = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (phaseOneStartingCanvas)
            phaseOneStartingCanvas.SetActive(true);

        if (phaseOneEndedCanvas)
            phaseOneEndedCanvas.SetActive(false);

        if (phaseTwoEndedCanvas)
            phaseTwoEndedCanvas.SetActive(false);

        if(currentPhase == Constents.PHASE_THREE)
        {
            currentPhaseDisplay.SetText("Battle Phase");
            startTimer = false;
        }
    }

       // Update is called once per frame
    void Update()
    {
        if (isServer && startTimer)
        {
            if(timer < currentTime - 5)
            {
                ShowStartPhaseCanvas();
            }

            if (timer < 5)
            {
                ShowEndPhaseCanvas();
            }

            if (timer > 1)
            {
                timer -= Time.deltaTime;
            }
            else 
            {
                print("Time is up, show battlefield");
                startTimer = false;
                ChangePhase();
            }

        }

        if(timerText)
            timerText.text = Mathf.Floor(Mathf.Max(timer - 5,0)) + "";

    }

    private void ShowStartPhaseCanvas()
    {
        switch (currentPhase)
        {
            case Constents.PHASE_ONE:
                RpcRemovePhaseOneStartCanvas();
                break;
            case Constents.PHASE_TWO:
                break;
            case Constents.PHASE_THREE:
                break;
            default:
                break;
        }
    }

    private void ShowEndPhaseCanvas()
    {
        switch (currentPhase)
        {
            case Constents.PHASE_ONE:
                RpcShowPhaseOneEndCanvas();
                break;
            case Constents.PHASE_TWO:
                RpcShowPhaseTwoEndCanvas();
                break;
            case Constents.PHASE_THREE:
                break;
            default:
                break;
        }
    }



    public void ChangePhase()
    {

        switch (currentPhase)
        {
            case Constents.PHASE_ONE:
                currentPhase = Constents.PHASE_TWO;
                SetPhaseTwo();
                break;
            case Constents.PHASE_TWO:
                currentPhase = Constents.PHASE_THREE;
                SetPhaseThree();
                break;
            case Constents.PHASE_THREE:
                currentPhase = Constents.PHASE_FOUR;
                SetPhaseFour();
                break;


            default:
                break;
        }
    }


    public void SetPhaseTwo()
    {
        timer = phaseTwoTime;
        currentTime = phaseTwoTime;
        ((RtsNetworkManager)NetworkManager.singleton).SetPhaseTwo();
        startTimer = true;
        RpcRemovePhaseOneEndCanvas();
        RpcRemoveBuildinPanel();

    }

    public void SetPhaseThree()
    {
        startTimer = false;

        RpcRemovePhaseTwoEndCanvas();

        ((RtsNetworkManager)NetworkManager.singleton).ShowBattlefieldPhase();
    }

    public void SetPhaseFour()
    {

    }

    [ClientRpc]
    public void RpcRemovePhaseOneStartCanvas()
    {
        phaseOneStartingCanvas.SetActive(false);
    }

    [ClientRpc]
    public void RpcShowPhaseOneEndCanvas()
    {
        phaseOneEndedCanvas.SetActive(true);
    }

    [ClientRpc]
    public void RpcRemovePhaseOneEndCanvas()
    {
        phaseOneEndedCanvas.SetActive(false);
        currentPhaseDisplay.SetText("Preperation Phase");
    }

    [ClientRpc]
    public void RpcRemoveBuildinPanel()
    {
        BuildingCanvasPanel.SetActive(false);
    }

    [ClientRpc]
    private void RpcShowPhaseTwoEndCanvas()
    {
        phaseTwoEndedCanvas.SetActive(true);
    }

    [ClientRpc]
    public void RpcRemovePhaseTwoEndCanvas()
    {
        phaseTwoEndedCanvas.SetActive(false);
    }
}
