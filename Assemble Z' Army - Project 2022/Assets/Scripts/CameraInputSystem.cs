using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraInputSystem : MonoBehaviour
{
    public float speed = 5.0f;

    void Start()
    {
       // PlayerInputActions playerInputActions = new PlayerInputActions();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MoveCamera()
    {
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            transform.position = new Vector3(speed * Time.deltaTime, 0, 0);
        }
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            transform.position = new Vector3(speed * Time.deltaTime, 0, 0);
        }
        if (Keyboard.current.downArrowKey.isPressed)
        {
            transform.position = new Vector3(0, speed * Time.deltaTime, 0);
        }
        if (Keyboard.current.upArrowKey.isPressed)
        {
            transform.position = new Vector3(0, speed * Time.deltaTime, 0);
        }
    }
}
