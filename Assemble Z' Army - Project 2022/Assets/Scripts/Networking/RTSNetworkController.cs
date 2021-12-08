using System.Collections.Generic;
using UnityEngine;
using Utilities;
using Mirror;

public class RTSNetworkController : NetworkBehaviour
{
    private void Awake()
    {
        selectedUnits = new List<Unit>();
    }

    private void Update()
    {

        if (!isLocalPlayer) { return; }

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
                Unit unit = obj.GetComponent<Unit>();
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
            foreach (Unit unit in selectedUnits){
                unit.MoveTo(Utilities.Utils.GetMouseWorldPosition());
                unit.ResetColor();
            }
        
        }

        foreach (Unit unit in selectedUnits)
        {
           // Debug.Log("unit pos: " + unit.transform.position);
          //  Debug.Log("dest========== " + unit.getDest());
            if (unit.transform.position.x == unit.getDest().x 
            && unit.transform.position.y == unit.getDest().y){
              //  Debug.Log("true");
                unit.Stop();
            }
                
        } 

    }
    private List<Unit> selectedUnits;
    private Vector3 startPos;
}
