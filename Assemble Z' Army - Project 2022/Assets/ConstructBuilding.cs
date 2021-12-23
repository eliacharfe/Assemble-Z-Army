using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for worker allowing to build required building.
public class ConstructBuilding : MonoBehaviour
{
    public BuilidingConstruction building;
    public float time = 0;

    private Unit unit = null;

    private void Start()
    {
        unit = gameObject.GetComponent<Unit>() as Unit;
    }


    // Update is called once per frame
    void Update()
    {
        if (!building) { return; }

        if (!TryToBuild()) { return; }

        IncreaseTimeBuilding();
    }


    // Increase the building time left.
    private void IncreaseTimeBuilding()
    {
        unit.StopMove(); // Stop when reached the building needed to be constructed.

        if (!building.enabled)
        {
            ResetBuildingTarget();
            return;
        }

        if (time <= 0.1f)
        {
            time += Time.deltaTime;
        }
        else
        {
            building.IncreasingBuildingTime(0.1f);
            time = 0;
        }
    }

    // Todo - set distance according the building boundry.
    // Check worker and building at minimum distance.
    private bool TryToBuild()
    {
        return Vector2.Distance(building.transform.position, transform.position) < 25f;
    }


    // Intlize target building and allowing worker to move.
    public void ResetBuildingTarget()
    {
        building = null;
        time = 0;
        Unit unit = gameObject.GetComponent<Unit>() as Unit;
        unit.ContinutMove();
    }


    // Set building needed to be constructed.
    public void SetBuildingTarget(BuilidingConstruction building)
    {
        this.building = building;
    }
}
