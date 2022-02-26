using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;

public class PhaseManager : NetworkBehaviour
{
    [SyncVar] public float timer = 30f;
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
        DontDestroyOnLoad(gameObject);

        Invoke("ChangePhase", timer);
    }

       // Update is called once per frame
    void Update()
    {

        if (isServer && startTimer)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                startTimer = false;
                //ChangePhase();
            }

        }

        //timerText.text = Mathf.Floor(timer) + "";

    }

        public void ChangePhase()
        {
            rtsNetworkManager.ShowBattleField();
        }
}
