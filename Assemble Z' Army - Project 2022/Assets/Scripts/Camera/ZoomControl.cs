using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class ZoomControl : MonoBehaviour
{
    [SerializeField] private float zoomSize = 100f;

    CinemachineVirtualCamera virtualCamera = null;

    private const float MAX_ZOOM_IN = 30f;
    private const float MAX_ZOOM_OUT= 130f;

    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        if (Mouse.current.scroll.ReadValue().y > 0)
        {
            if (zoomSize >= MAX_ZOOM_IN)
            {
                zoomSize -= 5f;
            }
        }

        if (Mouse.current.scroll.ReadValue().y < 0)
        {
            if (zoomSize <= MAX_ZOOM_OUT)
            {
                zoomSize += 5f;
            }
        }

        virtualCamera.m_Lens.OrthographicSize = zoomSize;

    }
}
