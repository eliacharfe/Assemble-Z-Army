using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
using UnityEngine.AI;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    [SerializeField] Unit unitPrefab = null;
    [SerializeField] float spawnTime = 5f;
    [SerializeField] Transform spawnPoint = null;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnMouseDown()
    {

        Unit unit = Instantiate(unitPrefab, spawnPoint.position,Quaternion.identity) as Unit;

    }


    public Sprite GetBuildingSprite()
    {
        return GetComponent<Sprite>();
    }



}
