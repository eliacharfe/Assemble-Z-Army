using System.Collections.Generic;
using UnityEngine;
using Utilities;
using Mirror;

public class RTSNetworkController : NetworkBehaviour
{
    private List<UnitNetwork> selectedUnits;
    private Vector3 startPos;

    private void Awake()
    {
        selectedUnits = new List<UnitNetwork>();
    }

    private void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            selectedUnits.Clear();
            startPos = Utilities.Utils.GetMouseWorldPosition();
        }

        if (Input.GetMouseButtonUp(0))
        {
            Collider2D[] inChosenArea = Physics2D.OverlapAreaAll(startPos, Utilities.Utils.GetMouseWorldPosition());
            foreach (Collider2D obj in inChosenArea)
            {
                UnitNetwork unit = obj.GetComponent<UnitNetwork>();
                if (unit != null && unit.isSelectable())
                {
                    selectedUnits.Add(unit);
                    unit.SetColorSelcted();
                    // Debug.Log(unit.transform.position);
                }
                // else {
                //     foreach (Unit un in selectedUnits)
                //        un.ResetColor();
                // }
            }
            // Debug.Log("RTS says hi");
        }

        if (Input.GetMouseButtonDown(1))
        {
            foreach (UnitNetwork unit in selectedUnits)
            {
                unit.MoveTo(Utilities.Utils.GetMouseWorldPosition());
                unit.ResetColor();
            }

        }

        foreach (UnitNetwork unit in selectedUnits)
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
    }

    
}
