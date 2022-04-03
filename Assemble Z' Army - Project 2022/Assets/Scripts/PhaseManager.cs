using System.Collections;
using System.Collections.Generic;
using Macros;
using UnityEngine;
using TMPro;
using Mirror;

public class PhaseManager : NetworkBehaviour
{
    [SyncVar] public float timer = 1f;
    [SyncVar] public int currentPhase = 1;
    [SyncVar] private bool startTimer = false;

    [SerializeField] GameObject BuildingCanvasPanel = null;
    [SerializeField] RtsNetworkManager rtsNetworkManager;

    public TextMeshProUGUI timerText;


    public override void OnStartServer()
    {
        base.OnStartServer();
    }
        

    public void SetTimer(bool value)
    {
        startTimer = value;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

       // Update is called once per frame
    void Update()
    {
        if (isServer && startTimer)
        {
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

        timerText.text = Mathf.Floor(timer) + "";

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
        timer = 100;
        ((RtsNetworkManager)NetworkManager.singleton).SetPhaseTwo();
        startTimer = true;
        RemoveBuildinPanelRPC();
    }

    public void SetPhaseThree()
    {
        startTimer = false;

        ((RtsNetworkManager)NetworkManager.singleton).ShowBattlefieldPhase();
    }

    public void SetPhaseFour()
    {

    }

    [ClientRpc]
    public void RemoveBuildinPanelRPC()
    {
        BuildingCanvasPanel.SetActive(false);
    }
}
