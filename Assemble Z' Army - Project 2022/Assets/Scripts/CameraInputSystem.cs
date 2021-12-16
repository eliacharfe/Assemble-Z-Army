using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;

public class CameraInputSystem : MonoBehaviour
{
    [SerializeField] private Transform playerCameraTransform = null;
    [SerializeField] private float speed = 20.0f;
    [SerializeField] private float screenBorderThikeness = 10f;
    [SerializeField] private Vector2 screenXLimits = Vector2.zero;
    [SerializeField] private Vector2 screenZLimits = Vector2.zero;

    private Controls controls;
    private Vector2 prevInput;


    void Start()
    {
        playerCameraTransform.gameObject.SetActive(true);
        controls = new Controls();
        controls.Player.MoveCamera.performed += SetPrevInput;
        controls.Player.MoveCamera.canceled += SetPrevInput;

        controls.Enable();
    }

    void Update()
    {
        MoveCamera();
    }

    private void SetPrevInput(InputAction.CallbackContext ctx)
    {
        prevInput = ctx.ReadValue<Vector2>();
    }

    public void MoveCamera()
    {
        Vector3 pos = playerCameraTransform.position;

        if (prevInput == Vector2.zero) // if mouse
        {
            Vector3 cursorMovement = Vector3.zero;
            Vector2 cursorPosition = Mouse.current.position.ReadValue();

            if (cursorPosition.y >= Screen.height - screenBorderThikeness)
                cursorMovement.z += 1;
            else if (cursorPosition.y <= screenBorderThikeness)
                cursorMovement.z -= 1;

            if (cursorPosition.x >= Screen.width - screenBorderThikeness)
                cursorMovement.x += 1;
            else if (cursorPosition.x <= screenBorderThikeness)
                cursorMovement.x -= 1;

            pos += cursorMovement.normalized * speed * Time.deltaTime;
        }
        else
        {  // if keyboard
            pos += new Vector3(prevInput.x, 0f, prevInput.y) * speed * Time.deltaTime;
        }

        pos.x = Mathf.Clamp(pos.x, screenXLimits.x, screenXLimits.y);
        pos.z = Mathf.Clamp(pos.z, screenXLimits.x, screenXLimits.y);

        playerCameraTransform.position = pos;

    }
}




        // if (Keyboard.current.rightArrowKey.isPressed)
        // {
        //     transform.position = new Vector3(speed * Time.deltaTime, 0, 0);
        // }
        // if (Keyboard.current.leftArrowKey.isPressed)
        // {
        //     transform.position = new Vector3(-speed * Time.deltaTime,0,0);
        // }
        // if (Keyboard.current.downArrowKey.isPressed)
        // {
        //     transform.position = new Vector3(0,-speed * Time.deltaTime,0);
        // }
        // if (Keyboard.current.upArrowKey.isPressed)
        // {
        //     transform.position = new Vector3(0,speed * Time.deltaTime,0);
        // }