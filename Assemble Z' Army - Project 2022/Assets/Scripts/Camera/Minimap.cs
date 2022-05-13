using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Minimap : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    [SerializeField] private Transform cameraTransform = null;

    [SerializeField] private RectTransform minimapRect = null;
    [SerializeField] private float mapScale = 150;
    [SerializeField] private float offset = -6f;

    private Transform playerCameraTransform;

    private void Update()
    {
        if (playerCameraTransform != null)
        {
            return;
        }

        // if (NetworkClient.connection.identity == null)
        // {
        //     return;
        // }

        // playerCameraTransform = NetworkClient.connection.identity.GetComponent<RTSPlayer>().GetCameraTransform();

        playerCameraTransform = cameraTransform;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        MoveCamera();
    }

    public void OnDrag(PointerEventData eventData)
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        Vector2 mousPos = Mouse.current.position.ReadValue();

        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
            minimapRect, mousPos, null, out Vector2 localPoint))
        {
            return;
        }

        Vector2 lerp = new Vector2(
            (localPoint.x - minimapRect.rect.x) / minimapRect.rect.width,
            (localPoint.y - minimapRect.rect.y) / minimapRect.rect.height);

        Vector3 newCameraPos = new Vector3(

            Mathf.Lerp(-mapScale, mapScale, lerp.x),
            Mathf.Lerp(-mapScale, mapScale, lerp.y),
            -10f
        );

        playerCameraTransform.position = newCameraPos;
    }
}
