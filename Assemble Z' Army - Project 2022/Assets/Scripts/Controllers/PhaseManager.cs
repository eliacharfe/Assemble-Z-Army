using Macros;
using UnityEngine;
using TMPro;
using Mirror;
using System;


// Handle phases timers and UI canvases 
public class PhaseManager : NetworkBehaviour
{
    [SyncVar] public float currentTime = 0f;
    [SyncVar] public float timer = 1f;
    [SyncVar] public int currentPhase = 1;
    [SyncVar] private bool startTimer = false;
    [SerializeField] private float phaseOneTime = 5f;
    [SerializeField] private float phaseTwoTime = 5f;

    // Canvas
    [SerializeField] private GameObject phaseOneStartingCanvas = null;
    [SerializeField] private GameObject BuildingPanelCanvas = null;
    [SerializeField] private GameObject phaseOneEndedCanvas = null;
    [SerializeField] private GameObject phaseTwoEndedCanvas = null;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI currentPhaseDisplay;

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
            currentPhaseDisplay.SetText(Phases.BATTLEFIELD_PHASE_NAME);
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
                startTimer = false;
                ChangePhase();
            }
        }

        if (timerText)
        {
            // Show time in format of 00:00
            TimeSpan timeSpan = TimeSpan.FromSeconds(Mathf.Floor(Mathf.Max(timer - 5, 0)));
            timerText.text = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
        }

    }

    #region server
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
    #endregion

    #region client
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
        currentPhaseDisplay.SetText(Phases.PREPERATION_PHASE_NAME);
    }

    [ClientRpc]
    public void RpcRemoveBuildinPanel()
    {
        BuildingPanelCanvas.SetActive(false);
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
    #endregion

    public void SetTimer(bool value)
    {
        startTimer = value;
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
}
