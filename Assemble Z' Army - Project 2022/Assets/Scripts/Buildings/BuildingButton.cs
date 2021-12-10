using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingButton : MonoBehaviour
{
    public Building building = null;


    public void spawnBuilding()
    {
        Instantiate(building, Camera.main.transform.position, Quaternion.identity);
    }

}
