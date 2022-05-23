using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for worker allowing to build required building.
public class Worker : MonoBehaviour
{
    public float time = 0;
    private BuilidingConstruction buildingTarget;
    private Unit unit = null;
    bool isBuilding = false;

    private void Start()
    {
        unit = gameObject.GetComponent<Unit>() as Unit;
    }

    // Update is called once per frame
    void Update()
    {
        if (!buildingTarget) {
            return; 
        }

        if(!isBuilding) {
            GetComponent<Animator>().SetBool("isAttacking", false);
            return; 
        }

        IncreaseTimeBuilding();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BuilidingConstruction tempBuild = collision.GetComponent<BuilidingConstruction>();
        if (tempBuild && this.buildingTarget == tempBuild)
        {
            isBuilding = true;
            unit.CmdStopMove();
            unit.CmdBuildAnimation();
            GetComponent<Animator>().SetBool("isAttacking", true);
        }
    }

    // Increase the building time left.
    private void IncreaseTimeBuilding()
    {
        // Stop when reached the building needed to be constructed.
        if (!buildingTarget.enabled)
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
            buildingTarget.CmdIncreasingBuildingTime(0.1f);
            time = 0;
        }
    }

    // Check worker and building at minimum distance.
    private bool TryToBuild()
    {
        return Vector2.Distance(buildingTarget.transform.position, transform.position) < 50f;
    }


    // Intlize target building and allowing worker to move.
    public void ResetBuildingTarget()
    {
        buildingTarget = null;
        isBuilding = false;
        time = 0;
        Unit unit = gameObject.GetComponent<Unit>() as Unit;
        unit.ContinutMove();
        GetComponent<Animator>().SetBool("isAttacking", false);
    }


    public BuilidingConstruction GetBuildingTarget()
    {
        return buildingTarget;
    }

    // Set building needed to be constructed.
    public void SetBuildingTarget(BuilidingConstruction building)
    {
        if (this.buildingTarget != building)
        {
            GetComponent<Animator>().SetBool("isAttacking", false);

            this.buildingTarget = building;
        }
    }

    public void BuildingClip()
    {
        FindObjectOfType<AudioPlayer>().PlaySpawnBuilding();
    }

}
