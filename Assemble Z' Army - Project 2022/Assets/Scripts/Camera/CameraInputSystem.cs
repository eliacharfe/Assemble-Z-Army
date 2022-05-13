using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;
using Cinemachine;

public class CameraInputSystem : NetworkBehaviour
{
    [SerializeField] private Transform playerCameraTransform = null;
    [SerializeField] private float speed = 20.0f;
    [SerializeField] private float screenBorderThikeness = 10f;
    [SerializeField] private Vector2 screenXLimits = Vector2.zero;
    [SerializeField] private Vector2 screenZLimits = Vector2.zero;
    [SerializeField] private float confinerBoundY= 60;
    [SerializeField] private float confinerBoundX = 60;
    [SerializeField] private float MAX_ZOOM_IN = 25f;
    [SerializeField] private float MAX_ZOOM_OUT = 35;
    [SerializeField] private float zoomSize = 100f;
    CinemachineVirtualCamera virtualCamera = null;

    private Controls controls;
    private Vector2 prevInput;
    private Vector3 startCameraPos;

    private void Start()
    {
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
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

        confinerBoundX = 118;
        confinerBoundY = 118;
        screenXLimits = new Vector3(0 - confinerBoundX, 0 + confinerBoundX, transform.position.z);
        screenZLimits = new Vector3(0 - confinerBoundY, 0 + confinerBoundY, transform.position.z);
    }

    #region server

    #endregion

    #region client

    [ClientCallback]
    void Update()
    {
        if (!hasAuthority || !Application.isFocused) { return; }

        MoveCamera();

        if (virtualCamera)
            ZoomInOutCamera();
    }

    public override void OnStartAuthority()
    {
        startCameraPos = playerCameraTransform.position;

        // Set boundries
        screenXLimits = new Vector3(startCameraPos.x - confinerBoundX, startCameraPos.x + confinerBoundX, transform.position.z);
        screenZLimits = new Vector3(startCameraPos.y - confinerBoundY, startCameraPos.y + confinerBoundY, transform.position.z);

        playerCameraTransform.gameObject.SetActive(true);

        controls = new Controls();

        controls.Player.MoveCamera.performed += SetPrevInput;
        controls.Player.MoveCamera.canceled += SetPrevInput;

        controls.Enable();
    }

    public override void OnStopAuthority()
    {
        Camera.main.transform.position = new Vector3(0, 0, 0);
    }
    #endregion


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
    }

    public void setPlayerCameraTransform(Transform cameraTransform)
    {
        print(playerCameraTransform.position);
        playerCameraTransform.position = cameraTransform.position;
    }

    void ZoomInOutCamera()
    {
        if (Mouse.current.scroll.ReadValue().y > 0)
        {
            if (zoomSize >= MAX_ZOOM_IN)
            {
                zoomSize -= 1f;
                screenXLimits.x -= 1.8f;
                screenXLimits.y += 1.8f;

                screenZLimits.x -= 0.8f;
                screenZLimits.y += 0.8f;
            }
        }

        if (Mouse.current.scroll.ReadValue().y < 0)
        {
            if (zoomSize <= MAX_ZOOM_OUT)
            {
                zoomSize += 1f;
                screenXLimits.x += 1.8f;
                screenXLimits.y -= 1.8f;

                screenZLimits.x += 0.8f;
                screenZLimits.y -= 0.8f;
            }
        }

        virtualCamera.m_Lens.OrthographicSize = zoomSize;
    }
}