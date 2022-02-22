using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 
 
 */
public class BuildingsFactory : MonoBehaviour
{
    [SerializeField]GameObject[] buildings;

    public GameObject GetBuildingById(int id)
    {
        foreach(GameObject building in buildings)
        {
            if(building.GetComponent<Building>().GetBuildingId() == id )
            {
                return building;
            }
        }

        return null;
    }
}
