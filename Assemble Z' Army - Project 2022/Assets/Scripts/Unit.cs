using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    private bool selectable;

    private NavMeshAgent agent;


    private void Start()
    {
        selectable = true;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update()
    {
    }

    public void MoveTo(Vector3 dest)
    {
        Debug.Log("dest:" + dest);
        dest.z = 0;
        agent.SetDestination(dest);
    }

    public bool isSelectable(){
        return selectable;
    }
}
