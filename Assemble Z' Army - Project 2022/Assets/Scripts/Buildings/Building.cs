using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.EventSystems;

public class Building : MonoBehaviour
{
    [SerializeField] Unit unitInPrefab = null;
    [SerializeField] Unit unitOutPrefab = null;
    [SerializeField] float spawnTime = 5f;
    [SerializeField] Transform spawnPoint = null;
    [SerializeField] GameObject token = null;

    private bool inProgess = false;

    // Start is called before the first frame update
    void Start()
    {
        //Set the building accoridng to the existed navmesh z position.
        Vector3 pos = FindObjectOfType<NavMeshScript>().transform.position;
        gameObject.transform.position =  new Vector3
            (gameObject.transform.position.x, gameObject.transform.position.y,pos.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Sprite GetBuildingSprite()
    {
        return GetComponent<Sprite>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Unit unit = collision.gameObject.GetComponent<Unit>();

        if (!unit)
        {
            return;
        }

        // Compare the building unit prefab tag is equal the incoming unit tag.
        if (unit.waitingToBeRecruited && unit.tag == unitInPrefab.tag)
        {
            // Todo - destroy unit and add it to player list.
            Destroy(unit.gameObject);
            spawnNewUnit();
        }
        else
        {
            Debug.Log("Unit can not be recruited here(Check the tag fit the building unit)." + unit.name);
        }
    }

    private void spawnNewUnit()
    {
        Unit unit = Instantiate(unitOutPrefab, spawnPoint.position, Quaternion.identity) as Unit;
    }

}
