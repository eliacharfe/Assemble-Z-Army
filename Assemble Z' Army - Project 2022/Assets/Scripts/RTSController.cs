using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class RTSController : MonoBehaviour
{
    private void Awake()
    {
       selectedUnits = new List<Unit>(); 
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            selectedUnits.Clear();
            startPos = Utils.GetMouseWorldPosition();
        }

        if(Input.GetMouseButtonUp(0)){
            Collider2D[] inChosenArea = Physics2D.OverlapAreaAll(startPos, Utils.GetMouseWorldPosition());
            foreach(Collider2D obj in inChosenArea){
                Unit unit = obj.GetComponent<Unit>();
                if(unit != null && unit.isSelectable()){
                    selectedUnits.Add(unit);
                    Debug.Log(unit.transform.position);
                }
            }
            Debug.Log("RTS says hi");
        }
        
        if(Input.GetMouseButtonDown(1)){
                foreach(Unit unit in selectedUnits)
                    unit.MoveTo(Utils.GetMouseWorldPosition());
        }
    }
    private List<Unit> selectedUnits;
    private Vector3 startPos;
}
