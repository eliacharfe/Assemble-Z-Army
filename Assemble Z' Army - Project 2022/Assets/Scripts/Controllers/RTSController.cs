using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using UnityEngine.InputSystem;

public class RTSController : MonoBehaviour
{
    private Camera mainCamera;
    private List < Unit > selectedUnits;
    private Vector3 startPos;
    private Vector2 startPosition;

    [SerializeField] private Transform selectionAreaTransform;

    private void Awake()
    {
        selectedUnits = new List<Unit>();
        selectionAreaTransform.gameObject.SetActive(false);
        mainCamera = Camera.main;
        Unit.AuthortyOnUnitDeSpawned += HandleDeSpawnUnit;
    }

    

    //------------------------
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
            GiveMovmentCommand();
        }

        foreach (Unit unit in selectedUnits)
        {
            if (unit.ReachedDestination())
                unit.StopAnimation();
        }
    }

    //--------------------------------- 
    private void GiveMovmentCommand()
    {
        BuilidingConstruction buildingToConstruct = null;
        Building building = null;
        Targetable targetable = null;

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if(hit.collider)
        {
            hit.collider.gameObject.TryGetComponent<BuilidingConstruction>(out buildingToConstruct);
            hit.collider.gameObject.TryGetComponent<Building>(out building);
            hit.collider.gameObject.TryGetComponent<Targetable>(out targetable);
        }

        if (buildingToConstruct && buildingToConstruct.enabled)
            SendToBuild(buildingToConstruct, hit);

        else if (building && building.enabled && building.hasAuthority)
            SendToRecruit(building,hit);

        else if(targetable && !targetable.hasAuthority)
            AttackUnit(targetable, hit);

        else 
            MoveUnits();
    }

    //--------------------------------------
    private void AttackUnit(Targetable targetable, RaycastHit2D hit)
    {

        foreach (Unit unit in selectedUnits)
        {
            if (unit.id != Macros.Units.WORKER)
            {
                if(targetable.teamNumber == unit.GetComponent<Targetable>().teamNumber)
                {
                    return;
                }

                unit.GetComponent<Attacker>().SetTargetable(targetable);
            }
        }
    }

    // Send unit to building for recruitment.
    private void SendToRecruit(Building building, RaycastHit2D hit)
    {
        foreach (Unit unit in selectedUnits)
        {
            if (unit.id != Macros.Units.WORKER)
            {
                unit.SetBuildingRecruiting(building);

                unit.MoveTo(building.EnterWaitingRecruitment(unit));
            }
        }
    }


    // Send workers to construct the building.
    private void SendToBuild(BuilidingConstruction building, RaycastHit2D hit)
    {
        foreach (Unit unit in selectedUnits)
        {
            if (unit.id == Macros.Units.WORKER)
            {
                (unit.GetComponent<ConstructBuilding>() as ConstructBuilding).SetBuildingTarget(building);
                unit.MoveTo(hit.point);
            }
        }
        
    }


    // Move units to position clicked on.
    private void MoveUnits()
      {
        Vector3 moveToPos = Utils.GetMouseWorldPosition();
        List<Vector3> targetPosList = GetPosListAround(moveToPos, new float[] {10, 20, 30}, new int[] {5, 10, 20});

        int targetPosIndex = 0;
        foreach (Unit unit in selectedUnits)
        {
            ClearPreviousCommands(unit);
            unit.MoveTo(targetPosList[targetPosIndex]);

            targetPosIndex = (targetPosIndex + 1) % targetPosList.Count;

            if (unit.id == Macros.Units.WORKER)
            {
                (unit.GetComponent<ConstructBuilding>() as ConstructBuilding).ResetBuildingTarget();
            }
        }
    }


    // Clear previous commands such as attack target or recruit.
    private void ClearPreviousCommands(Unit unit)
    {
        unit.RemoveBuildingRecruiting();

        if (unit.GetComponent<Attacker>())
            unit.GetComponent<Attacker>().SetTargetable(null);
    }


    // Get positions around the point given.
    private List<Vector3> GetPosListAround(Vector3 startPos, float[] ringDistanceArr, int[] ringPosCountArr)
    {
           List<Vector3> posList = new List<Vector3>();
           posList.Add(startPos);
           for(int i = 0; i < ringPosCountArr.Length; i++){
               posList.AddRange(GetPosListAround(startPos, ringDistanceArr[i], ringPosCountArr[i]));
           }
           return posList;
    }

    //---------------------------------------------
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

    //-------------------------------------------
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

            if (unit != null && unit.isSelectable() && unit.hasAuthority)
            {
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
