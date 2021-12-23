using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using UnityEngine.InputSystem;
// using UnitMove;
using Cinemachine;

public class RTSController : MonoBehaviour
{
    UnitMove movement;
    private Camera mainCamera;
    public CinemachineVirtualCamera VCam;
    public Transform tFollowTarget;

    private List<Unit> selectedUnits;
    private Vector3 startPos;
    private Vector2 startPosition;

    [SerializeField] private Transform selectionAreaTransform;

    private void Awake()
    {
        selectionAreaTransform.gameObject.SetActive(false);

        movement = GameObject.FindGameObjectWithTag("UnitMove").GetComponent<UnitMove>();
        mainCamera = Camera.main;
        VCam = GetComponent<CinemachineVirtualCamera>();
        selectedUnits = new List<Unit>();

        Unit.OnDeUnitSpawned += HandleDeSpawnUnit;
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            StartSelectionArea();
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            ClearSelectionArea();
        }
        else if (Mouse.current.leftButton.isPressed)
        {
            UpdateSelectionArea();
        }

        if (Mouse.current.rightButton.wasReleasedThisFrame)
        {
            MoveUnits();
        }

        foreach (Unit unit in selectedUnits)
        {
            if (unit.ReachedDestination())
                unit.StopAnimation();
        }
    }

    //-------------
     private void MoveUnits()
    {
        Vector3 moveToPos = Utils.GetMouseWorldPosition();

        List<Vector3> targetPosList = GetPosListAround(moveToPos, new float[] {10, 20, 30}, new int[] {5, 10, 20});

        int targetPosIndex = 0;
        foreach (Unit unit in selectedUnits)
        {
            unit.MoveTo(targetPosList[targetPosIndex]);
            targetPosIndex = (targetPosIndex + 1) % targetPosList.Count;
        }
    }

    private List<Vector3> GetPosListAround(Vector3 startPos, float[] ringDistanceArr, int[] ringPosCountArr)
    {
           List<Vector3> posList = new List<Vector3>();
           posList.Add(startPos);
           for(int i = 0; i < ringPosCountArr.Length; i++){
               posList.AddRange(GetPosListAround(startPos, ringDistanceArr[i], ringPosCountArr[i]));
           }
           return posList;
    }

    private List<Vector3> GetPosListAround(Vector3 startPostion, float distance, int posCount)
    {
        List<Vector3> posList = new List<Vector3>();
        for (int i = 0; i < posCount; i++)
        {
            float angle = i * (360f / posCount);
            Vector3 direction = ApplyRotationToVec(new Vector3(1, 0), angle);
            Vector3 position = startPostion + direction * distance;
            posList.Add(position);
        }
        return posList;
    }

    private Vector3 ApplyRotationToVec(Vector3 vec, float angle)
    {
        return Quaternion.Euler(0, 0, angle) * vec;
    }


    //-------------------------------------
    private void StartSelectionArea()
    {
        startPosition = Utils.GetMouseWorldPosition();
        selectionAreaTransform.gameObject.SetActive(true);

        if (!Keyboard.current.leftShiftKey.isPressed)
        {
            foreach (Unit selectedUnit in selectedUnits)
            {
                selectedUnit.Deselect();
            }
            selectedUnits.Clear();
        }

        startPos = Utils.GetMouseWorldPosition();
        startPosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1.0f));

        UpdateSelectionArea();
    }
    //--------------------------------
    private void ClearSelectionArea()
    {
        selectionAreaTransform.gameObject.SetActive(false);

        Collider2D[] inChosenArea = Physics2D.OverlapAreaAll(startPos, Utils.GetMouseWorldPosition());
        foreach (Collider2D obj in inChosenArea)
        {

            Unit unit = obj.GetComponent<Unit>();
            if (selectedUnits.Contains(unit)) { continue; }

            if (unit != null && unit.isSelectable())
            {
                Debug.Log("unitPos: " + unit.transform.position);
                selectedUnits.Add(unit);
                unit.Select();
            }
        }
    }

    //--------------------------------
    private void UpdateSelectionArea()
    {
        Vector3 currMousePos = Utils.GetMouseWorldPosition();
        Vector3 buttomLeft = new Vector3(
            Mathf.Min(startPosition.x, currMousePos.x),
            Mathf.Min(startPosition.y, currMousePos.y));
        Vector3 topRight = new Vector3(
            Mathf.Max(startPosition.x, currMousePos.x),
            Mathf.Max(startPosition.y, currMousePos.y));

        selectionAreaTransform.position = buttomLeft;
        selectionAreaTransform.localScale = topRight - buttomLeft;
    }
    //-----------------------------------
    // public List<Unit> GetMyUnits()
    // {
    //     return selectedUnits;
    // }

    private void HandleDeSpawnUnit(Unit unit)
    {
        selectedUnits.Remove(unit);
    }
}







// public List<Unit> GetMyUnits()
// {
//     return selectedUnits;
// }





// unitSelectionArea.gameObject.SetActive(false);
// if (unitSelectionArea.sizeDelta.magnitude == 0)
// {
//     Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
//     if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask)) { return; }
//     if (!hit.collider.TryGetComponent<Unit>(out Unit unit)) { return; }
//      selectedUnits.Add(unit);

//     foreach (Unit selectedUnit in selectedUnits)
//     {
//         selectedUnit.Select();
//     }

//     return;
// }

// Vector2 min = unitSelectionArea.anchoredPosition - (unitSelectionArea.sizeDelta / 2);
// Vector2 max = unitSelectionArea.anchoredPosition + (unitSelectionArea.sizeDelta / 2);

// foreach (Unit unit in player.GetMyUnits())
// {
//     Vector3 screenPosition = mainCamera.WorldToScreenPoint(unit.transform.position);

//     if (screenPosition.x > min.x && screenPosition.x < max.x &&
//         screenPosition.y > min.y && screenPosition.y < max.y)
//     {
//         selectedUnits.Add(unit);
//         unit.Select();
//     }
// }
