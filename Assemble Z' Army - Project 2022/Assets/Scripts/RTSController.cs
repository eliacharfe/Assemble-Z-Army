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
    //    [SerializeField] private LayerMask layerMask = new LayerMask();
    [SerializeField] private RectTransform unitSelectionArea = null;

    private void Awake()
    {
        movement = GameObject.FindGameObjectWithTag("UnitMove").GetComponent<UnitMove>();
        mainCamera = Camera.main;
        VCam = GetComponent<CinemachineVirtualCamera>();
        selectedUnits = new List<Unit>();
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
            if (unit.transform.position.x == unit.getDest().x
            && unit.transform.position.y == unit.getDest().y)
            {
                unit.Stop(); // when the unit get to its destination will stop change the animation
            }
        }
    }

    private void MoveUnits()
    {
        foreach (Unit unit in selectedUnits)
        {
            unit.Move();
           // movement.MoveU(unit);

           // Movement.MoveUnit(unit);
           // tFollowTarget = unit.transform;
            // VCam.Follow = tFollowTarget;
        }
    }

    //-------------------------------------
    private void StartSelectionArea()
    {

        if (!Keyboard.current.leftShiftKey.isPressed)
        {
            Debug.Log("shift key is not pressed");
            foreach (Unit selectedUnit in selectedUnits)
            {
                selectedUnit.Deselect();
            }
            selectedUnits.Clear();
        }

        startPos = Utils.GetMouseWorldPosition();
        unitSelectionArea.gameObject.SetActive(true);
        startPosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1.0f));

        //  new Vector2(Utils.GetMouseWorldPosition().x ,
        //                          Utils.GetMouseWorldPosition().y );
        // startPosition = Mouse.current.position.ReadValue();

        UpdateSelectionArea();
    }
    //--------------------------------
    private void ClearSelectionArea()
    {
        unitSelectionArea.gameObject.SetActive(false);

        Collider2D[] inChosenArea = Physics2D.OverlapAreaAll(startPos, Utils.GetMouseWorldPosition());
        foreach (Collider2D obj in inChosenArea)
        {
             
            Unit unit = obj.GetComponent<Unit>();
            if (selectedUnits.Contains(unit)) { continue; }

            if (unit != null && unit.isSelectable())
            {
                selectedUnits.Add(unit);
                ///  unit.SetColorSelcted();
                unit.Select();
            }
        }
    }

    //--------------------------------
    private void UpdateSelectionArea()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();

        float areaWidth = mousePosition.x - startPosition.x;
        float areaHeight = mousePosition.y - startPosition.y;

        unitSelectionArea.sizeDelta = new Vector2(Mathf.Abs(areaWidth), Mathf.Abs(areaHeight));
        unitSelectionArea.anchoredPosition = startPosition + new Vector2(areaWidth / 2, areaHeight / 2);

    }
    //-----------------------------------
    public List<Unit> GetMyUnits()
    {
        return selectedUnits;
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
