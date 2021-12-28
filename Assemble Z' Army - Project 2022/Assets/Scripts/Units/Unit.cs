using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Utilities;

public class Unit : MonoBehaviour
{
    private bool selectable;
    public  Building recrutingBuilding = null;

    private NavMeshAgent agent;
    private Animator myAnimator;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private UnityEvent onSelected = null;
    [SerializeField] private UnityEvent onDeselected = null;

    public static event Action<Unit> OnUnitSpawned;
    public static event Action<Unit> OnDeUnitSpawned;

    public Macros.Units id;

    private Vector3 destination;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        agent.enabled = false;
        agent.enabled = true;

        gameObject.GetComponent<NavMeshAgent>().enabled = true;
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        OnUnitSpawned?.Invoke(this);

        selectable = true;

        myAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {}

    private void Update()
    {
        if (!agent.hasPath)
            return;

        if (agent.remainingDistance  > agent.stoppingDistance )
            return;

        agent.ResetPath();
        StopAnimation();

    }

    private void OnDestroy()
    {
        OnDeUnitSpawned?.Invoke(this);
    }

    public void MoveTo(Vector3 dest)
    {    
        myAnimator.SetBool("isRunning", true);
        dest.z = 0;
        agent.SetDestination(dest);

        FlipSideSprite(dest);
    }

    public bool ReachedDestination()
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                       agent.ResetPath();
                   return true;
                }
            }
        }
        return false;
    }

    private void FlipSideSprite(Vector3 dest)
    {
        if (dest.x < transform.position.x && transform.localScale.x > Mathf.Epsilon)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y,
            transform.localScale.z);
        }
        else if (dest.x > transform.position.x && transform.localScale.x < Mathf.Epsilon)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y,
                        transform.localScale.z);
        }
    }

    public void StopAnimation()
    {
        myAnimator.SetBool("isRunning", false);
    }

    public void SetColorSelcted()
    {
        spriteRenderer.color = new Color(1f, 0f, 0f, 1f);
    }

    public void ResetColor()
    {
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
    }

    public bool isSelectable()
    {
        return selectable;
    }

    public void Select()
    {
        onSelected?.Invoke();
    }

    public void Deselect()
    {
        onDeselected?.Invoke();
    }

    public void StopMove()
    {
        agent.ResetPath();
    }

    public void ContinutMove()
    {
        agent.isStopped = false;
    }

    public bool isMoving()
    {
        return !agent.isStopped;
    }

    public void SetBuildingRecruiting(Building building)
    {
        recrutingBuilding = building;
    }

    public void RemoveBuildingRecruiting()
    {
        if(recrutingBuilding)
        {
            Debug.Log("removed from current building");
            recrutingBuilding.RemoveUnitFromWaitingList(this);
            recrutingBuilding = null;
        }
    }

}





//  NavMeshHit closestHit;
// if (NavMesh.SamplePosition(gameObject.transform.position, out closestHit, 500f, NavMesh.AllAreas))
//     gameObject.transform.position = closestHit.position;
// else
// {
//   //   Debug.LogError("Could not find position on NavMesh!");
//     Debug.Log("in err: " + transform.position);
//     transform.position = new Vector3(transform.position.x, transform.position.y, 0);
//     Debug.Log("After: " + transform.position);
//     agent.enabled = false;
//     agent.enabled = true;
// }