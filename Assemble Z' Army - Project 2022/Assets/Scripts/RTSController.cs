using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using UnityEngine.InputSystem;
using System;

public class RTSController : MonoBehaviour
{
    private List<Unit> selectedUnits;
    private Vector3 startPos;
    [SerializeField] private LayerMask layerMask = new LayerMask();
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
        selectedUnits = new List<Unit>();
    }

    private void Update()
    {
        // if (Input.GetMouseButtonDown(0))
        // {
        //     selectedUnits.Clear();
        //     startPos = Utils.GetMouseWorldPosition();
        // }

        if (Input.GetMouseButtonUp(0))
        {
            Collider2D[] inChosenArea = Physics2D.OverlapAreaAll(startPos, Utils.GetMouseWorldPosition());
            foreach (Collider2D obj in inChosenArea)
            {
                Unit unit = obj.GetComponent<Unit>();
                if (unit != null && unit.isSelectable())
                {
                    selectedUnits.Add(unit);
                    ///  unit.SetColorSelcted();
                    unit.Select();
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            foreach (Unit unit in selectedUnits)
            {
                unit.MoveTo(Utils.GetMouseWorldPosition());
                unit.ResetColor();
            }

        }

        foreach (Unit unit in selectedUnits)
        {
            // Debug.Log("unit pos: " + unit.transform.position);
            //  Debug.Log("dest========== " + unit.getDest());
            if (unit.transform.position.x == unit.getDest().x
            && unit.transform.position.y == unit.getDest().y)
            {
                //  Debug.Log("true");
                unit.Stop();
            }

        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            StartSelectionArea();

        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            // ClearSelectionArea();
        }
    }
//-------------------------------------
    private void StartSelectionArea()
    {
        foreach (Unit selectedUnit in selectedUnits)
        {
            selectedUnit.Deselect();
        }

        selectedUnits.Clear();
        startPos = Utils.GetMouseWorldPosition();
    }

    // private void ClearSelectionArea()
    // {
    //     throw new NotImplementedException();
    // }
}
