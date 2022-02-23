using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;

public class PhaseManager : NetworkBehaviour
{
    [SyncVar] public float timer = 30f;
    public TextMeshProUGUI timerText;
    [SerializeField] RtsNetworkManager rtsNetworkManager;


    public override void OnStartServer()
    {
        base.OnStartServer();
        Debug.Log("Loaded the phases manager");
        //Invoke("ChangePhase", timer);
    }

    IEnumerator SetTimer()
    {
        timerText.text = Mathf.Floor(timer) + "";
        yield return new WaitForSeconds(timer);
        ChangePhase();
    }
        /*
        // Start is called before the first frame update
        void Start()
        {
            DontDestroyOnLoad(gameObject);

            Invoke("ChangePhase", timer);
        }

        // Update is called once per frame
        void Update()
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;

            }

            
        }*/

        public void ChangePhase()
    {
        // rtsNetworkManager.ShowBattleField();
    }
}
