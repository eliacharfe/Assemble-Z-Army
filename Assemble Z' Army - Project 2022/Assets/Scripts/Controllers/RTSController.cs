using System.Collections.Generic;
using UnityEngine;
using Utilities;
using UnityEngine.InputSystem;
using System.Collections;

public class RTSController : MonoBehaviour
{
    [SerializeField] private Transform selectionAreaTransform;
    [SerializeField] private GameObject pointerDown = null;
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

        // Clicked on building in construction
        if (buildingToConstruct && buildingToConstruct.enabled && buildingToConstruct.hasAuthority)
            SendToBuild(buildingToConstruct, hit);

        // Clicked on building
        else if (building && building.enabled && building.hasAuthority)
            SendToRecruit(building, hit);

        // Clicked on targetable unit
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
            unit.GetComponent<Attacker>().SetAttackMode();

            if (unit.id != Macros.Units.WORKER && !targetable.hasAuthority)
            {
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
                Building tempBuilding = unit.GetBuildingRecruiting();
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
            var worker = (unit.GetComponent<Worker>() as Worker);
            if (unit && unit.id == Macros.Units.WORKER)
            {
                // Building is certain not null
                if (building != worker.GetBuildingTarget() && building.HasFreeSpace())
                {
                        worker.SetBuildingTargetPosition(building);
                }
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
            if (!unit) { continue; }
            ClearPreviousCommands(unit);

            Vector3 wantedPosition = targetPosList[targetPosIndex];
            unit.MoveTo(wantedPosition);
            targetPosIndex = (targetPosIndex + 1) % targetPosList.Count;

            // Position effect
            GameObject newObj = Instantiate(pointerDown, wantedPosition,pointerDown.transform.rotation) as GameObject;
            StartCoroutine(FadeOut(newObj));

            if (unit.id == Macros.Units.WORKER)
            {
                var worker = (unit.GetComponent<Worker>() as Worker);
                worker.ResetBuildingTarget();
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

    public bool HasUnits()
    {
        return selectedUnits.Count > 0;
    }

    private IEnumerator FadeOut(GameObject gameObj)
    {
        SpriteRenderer spRenderer = gameObj.GetComponent<SpriteRenderer>();
        for (float f = 1f; f >= -0.01; f -= 0.01f)
        {
            Color color = spRenderer.material.color;
            color.a = f;
            spRenderer.material.color = color;
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(gameObj.gameObject);
    }
}
