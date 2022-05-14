using System.Collections.Generic;
using UnityEngine;
using Utilities;
using UnityEngine.InputSystem;

public class RTSController : MonoBehaviour
{
    [SerializeField] private Transform selectionAreaTransform;
    private Camera mainCamera;
    private List < Unit > selectedUnits;
    private Vector3 startPos;
    private Vector2 startPosition;
    private int LayerMaskDetectionArea;
    private List<Macros.Units> idsUnits;

    private void Awake()
    {
        selectedUnits = new List<Unit>();
        selectionAreaTransform.gameObject.SetActive(false);
        mainCamera = Camera.main;
        Unit.AuthortyOnUnitDeSpawned += HandleDeSpawnUnit;
        LayerMaskDetectionArea = LayerMask.GetMask("DetectionAttackArea");
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
            GiveMovmentCommand();
        }
    }

    // Give a command accordingly the unit type and location clicked
    private void GiveMovmentCommand()
    {
        BuilidingConstruction buildingToConstruct = null;
        Building building = null;
        Targetable targetable = null;

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition),
                                       Vector2.zero, 0, ~LayerMaskDetectionArea);
        if (hit.collider)
        {
            hit.collider.gameObject.TryGetComponent<BuilidingConstruction>(out buildingToConstruct);
            hit.collider.gameObject.TryGetComponent<Building>(out building);
            hit.collider.gameObject.TryGetComponent<Targetable>(out targetable);
        }

        if (buildingToConstruct && buildingToConstruct.enabled && buildingToConstruct.hasAuthority)
            SendToBuild(buildingToConstruct, hit);

        else if (building && building.enabled && building.hasAuthority)
            SendToRecruit(building, hit);

        else if (targetable)
        {
            AttackUnit(targetable, hit);

        }
        else
            MoveUnits();
    }

    // Attack the relevent unit
    private void AttackUnit(Targetable targetable, RaycastHit2D hit)
    {
        foreach (Unit unit in selectedUnits)
        {
            if(!unit || !unit.GetComponent<Attacker>())
            {
                continue;
            }
            unit.GetComponent<Attacker>().setAttackMode();

            if (unit.id != Macros.Units.WORKER && !targetable.hasAuthority)
            {
                print("Attack unit");
                unit.GetComponent<Attacker>().CmdSetTargetable(targetable);
            }

            if (unit.id == Macros.Units.HEALER && targetable.hasAuthority)
            {
                if (unit.GetComponent<Mana>().canHeal)
                {
                    //audioPlayer.PlayHealingClip();
                    unit.GetComponent<Mana>().CmdUseHeal();
                    targetable.GetComponent<Health>().CmdHeal(100);
                    //unit.GetComponent<Mana>().RpcPlayManaEffect();
                }
            
            }


        }
    }

    // Send for recruitment.
    private void SendToRecruit(Building building, RaycastHit2D hit)
    {
        foreach (Unit unit in selectedUnits)
        {
            if (unit && unit.id != Macros.Units.WORKER && building.isMatchedUnit(unit))
            {
                var tempBuilding = unit.GetBuildingRecruiting();
                if(tempBuilding)
                {
                    tempBuilding.RemoveUnitFromWaitingList(unit);
                }

                unit.SetBuildingRecruiting(building);

                unit.MoveTo(building.EnterWaitingRecruitment(unit));
            }
        }
    }

    // Send workers only to construct the building.
    private void SendToBuild(BuilidingConstruction building, RaycastHit2D hit)
    {
        foreach (Unit unit in selectedUnits)
        {
            if (unit && unit.id == Macros.Units.WORKER)
            {
                (unit.GetComponent<Worker>() as Worker).SetBuildingTarget(building);
                unit.MoveTo(hit.point);
            }
        }
        
    }

    // Move units to position clicked on.
    private void MoveUnits()
      {
        Vector3 moveToPos = Utils.GetMouseWorldPosition();
        List<Vector3> targetPosList = Utils.GetCircleForamtionPosList(moveToPos, new float[] {5, 10, 15}, new int[] {5, 10, 15});

        int targetPosIndex = 0;

        if(selectedUnits.Count > 0)
        {
            FindObjectOfType<AudioPlayer>().PlayStepClip();
        }

        foreach (Unit unit in selectedUnits)
        {
            if (!unit)
            {
                continue;
            }

            ClearPreviousCommands(unit);

            unit.MoveTo(targetPosList[targetPosIndex]);

            targetPosIndex = (targetPosIndex + 1) % targetPosList.Count;

            if (unit.id == Macros.Units.WORKER)
            {
                (unit.GetComponent<Worker>() as Worker).ResetBuildingTarget();
            }
        }
    }

    private void ClearPreviousCommands(Unit unit)
    {
        if(!unit) { return; }

        unit.RemoveBuildingRecruiting();

        if (unit.GetComponent<Attacker>())
            unit.GetComponent<Attacker>().CmdSetTargetable(null);
    }

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
    public List<Macros.Units> GetIdsUnits()
    {
        idsUnits = new List<Macros.Units>();

        foreach (Unit unit in selectedUnits)
        {
            idsUnits.Add(unit.id);
        }

        return idsUnits;
    }

    public bool HasWorkers()
    {
        return selectedUnits.Find(unit => unit.id == Macros.Units.WORKER);
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
