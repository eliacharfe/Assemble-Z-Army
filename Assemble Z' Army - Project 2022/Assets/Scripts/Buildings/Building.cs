using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEditor;

public class Building : MonoBehaviour
{
    [SerializeField] Unit unitInPrefab = null;
    [SerializeField] Unit unitOutPrefab = null;
    [SerializeField] float spawnTime = 5f;
    [SerializeField] Transform spawnPoint = null;

    private bool inProgess = false;


    // Start is called before the first frame update
    void Start()
    {
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

        if (unit.waitingToBeRecruited && unit.name == unitInPrefab.name)
        {
            // Todo - destroy unit and add it to player list.
            unit.gameObject.SetActive(false);
            spawnNewUnit();
        }
        else
        {
            Debug.Log("Unit can not be recruited here." + unit.name);
        }
    }

    private void spawnNewUnit()
    {
        Unit unit = Instantiate(unitOutPrefab, spawnPoint.position, Quaternion.identity) as Unit;
    }



}
