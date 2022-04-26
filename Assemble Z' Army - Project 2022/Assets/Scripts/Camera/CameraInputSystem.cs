using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine.Experimental.Rendering.Universal;

public class CameraInputSystem : NetworkBehaviour
{
    [SerializeField] private Transform playerCameraTransform = null;
    [SerializeField] private float speed = 20.0f;
    [SerializeField] private float screenBorderThikeness = 10f;
    [SerializeField] private Vector2 screenXLimits = Vector2.zero;
    [SerializeField] private Vector2 screenZLimits = Vector2.zero;
    [SerializeField] private float confinerBoundY= 60;
    [SerializeField] private float confinerBoundX = 60;

    private Controls controls;
    private Vector2 prevInput;

    [SerializeField] private GameObject gameObj;
    [SerializeField] public new UnityEngine.Experimental.Rendering.Universal.Light2D light;

    //[SerializeField] private Light2D light;
    // UnityEngine.Experimental.Rendering.LWRP.Light2D m_Light2D = null;
    //private float scrollSpeed = 40f;

    private Vector3 startCameraPos, minCam, maxCam;
    float leftLimit, rightLimit, topLimit, buttomLimit;

    public override void OnStartAuthority()
    {
        startCameraPos = playerCameraTransform.position;
        screenXLimits = new Vector3(startCameraPos.x - confinerBoundX, startCameraPos.x + confinerBoundX, transform.position.z);
        screenZLimits = new Vector3(startCameraPos.y - confinerBoundY, startCameraPos.y + confinerBoundY, transform.position.z);

        playerCameraTransform.gameObject.SetActive(true);

        controls = new Controls();

        controls.Player.MoveCamera.performed += SetPrevInput;
        controls.Player.MoveCamera.canceled += SetPrevInput;

        controls.Enable();
    }


    public void OnStartingGame()
    {
        startCameraPos = playerCameraTransform.position;
        screenXLimits = new Vector3(startCameraPos.x - confinerBoundX, startCameraPos.x + confinerBoundX, transform.position.z);
        screenZLimits = new Vector3(startCameraPos.y - confinerBoundY, startCameraPos.y + confinerBoundY, transform.position.z);
    }

    public void OnChangePhaseThreeMap()
    {
        startCameraPos = playerCameraTransform.position;

        confinerBoundX = 120;
        confinerBoundY = 120;
        screenXLimits = new Vector3(0 - confinerBoundX, 0 + confinerBoundX, transform.position.z);
        screenZLimits = new Vector3(0 - confinerBoundY, 0 + confinerBoundY, transform.position.z);
    }

    public override void OnStopAuthority()
    {
        Camera.main.transform.position = new Vector3(0, 0, 0);
    }

    [ClientCallback]
    void Update()
    {
        if (!hasAuthority || !Application.isFocused) { return; }

        MoveCamera();
    }

    private void SetPrevInput(InputAction.CallbackContext ctx)
    {
        prevInput = ctx.ReadValue<Vector2>();
    }

    public void MoveCamera()
    {
        Vector3 pos = playerCameraTransform.position;

        // if (prevInput == Vector2.zero) // if mouse dragged
        // {
        //     Vector3 cursorMovement = Vector3.zero;
        //     Vector2 cursorPosition = Mouse.current.position.ReadValue();

        //     if (cursorPosition.y >= Screen.height - screenBorderThikeness)
        //         cursorMovement.y += 1;
        //     else if (cursorPosition.y <= screenBorderThikeness)
        //         cursorMovement.y -= 1;

        //     if (cursorPosition.x >= Screen.width - screenBorderThikeness)
        //         cursorMovement.x += 1;
        //     else if (cursorPosition.x <= screenBorderThikeness)
        //         cursorMovement.x -= 1;

        //     pos += cursorMovement.normalized * speed * Time.deltaTime;
        // }
        // else
        {  // if keyboard
           
        }

        pos += new Vector3(prevInput.x, prevInput.y, 0f) * speed * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, screenXLimits.x, screenXLimits.y);
        pos.y = Mathf.Clamp(pos.y, screenZLimits.x, screenZLimits.y);

        playerCameraTransform.position = pos;
        /*
        playerCameraTransform.position = new Vector3(

        Mathf.Clamp(playerCameraTransform.position.z, minCam.z, maxCam.z)); // boundies to camera
        */
    }
}




// pos.x = Mathf.Clamp(pos.x, screenXLimits.x, screenXLimits.y);
// pos.z = Mathf.Clamp(pos.z, screenXLimits.x, screenXLimits.y);

// float scroll = Input.GetAxis("Mouse ScrollWheel");
// pos.y += scroll * scrollSpeed * 200f * Time.deltaTime;