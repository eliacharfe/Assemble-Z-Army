using UnityEngine;

public class BuildingsFactory : MonoBehaviour
{
    // Avaible buildings.
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
