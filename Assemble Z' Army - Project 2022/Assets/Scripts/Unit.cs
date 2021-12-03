using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    private bool selectable;

    private NavMeshAgent agent;

    private Animator myAnimator;

    private void Start()
    {
        selectable = true;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        myAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
      // FlipSideSprite();
    }

    public void MoveTo(Vector3 dest)
    {
        myAnimator.SetBool("isRunning", true);

        Debug.Log("dest:" + dest);
        dest.z = 0;
        agent.SetDestination(dest);
        
    }

    public Vector3 getDest(){
        return agent.destination;
    }

    public void Stop(){
        myAnimator.SetBool("isRunning", false);
    }

    public bool isSelectable(){
        return selectable;
    }

    // public void FlipSideSprite(){
    //     transform.localScale = new Vector3 (Mathf.Sign(agent.velocity.x), 1f);
    // }
}
