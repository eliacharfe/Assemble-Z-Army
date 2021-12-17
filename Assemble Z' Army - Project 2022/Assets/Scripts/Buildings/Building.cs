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

    [SerializeField]UnitSlider timeSlider = null;

    private bool inProgess = false;
    private float timeLeft = 0;

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
        if(inProgess)
        {
            if (timeLeft / spawnTime < 1)
            {
                timeSlider.setFill(timeLeft / spawnTime);
                timeLeft += Time.deltaTime;
            }
            else
            {
                timeLeft = 0f;
                spawnNewUnit();
                inProgess = false;
                timeSlider.resetSlider();
            }
        }

   


    }

    public Sprite GetBuildingSprite()
    {
        return GetComponent<Sprite>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Unit unit = collision.gameObject.GetComponent<Unit>();
        Debug.Log(inProgess);
        if (!unit || inProgess)
        {
            return;
        }

        // Compare the building unit prefab tag is equal the incoming unit tag.
        if (unit.waitingToBeRecruited && unit.tag == unitInPrefab.tag)
        {
            inProgess = true;
            // Todo - destroy unit and add it to player list.
            Destroy(unit.gameObject);
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
