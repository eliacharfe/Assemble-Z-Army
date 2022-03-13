using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;

public class PhaseManager : NetworkBehaviour
{
    [SyncVar] public float timer = 1f;
    [SyncVar]bool startTimer = false;
    public TextMeshProUGUI timerText;
    [SerializeField] RtsNetworkManager rtsNetworkManager;
   


    public override void OnStartServer()
    {
        base.OnStartServer();
        Debug.Log("Loaded the phases manager");
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

        if (hasAuthority && startTimer)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                print("Time is up, show battlefield");
                startTimer = false;
                //ChangePhase();
            }

        }

        //timerText.text = Mathf.Floor(timer) + "";

    }

    public void ChangePhase()
    {
        rtsNetworkManager.ShowBattleField();

        testingClientRPC();
    }

    [ClientRpc]
    public void testingClientRPC()
    {
        print("Client recived this rpc");
    }
}
