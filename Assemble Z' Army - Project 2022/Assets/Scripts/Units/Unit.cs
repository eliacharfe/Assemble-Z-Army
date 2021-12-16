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
    public bool waitingToBeRecruited = true;

    private NavMeshAgent agent;
    private Animator myAnimator;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private UnityEvent onSelected = null;
    [SerializeField] private UnityEvent onDeselected = null;

    public static event Action<Unit> OnDeUnitSpawned;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        agent.enabled = false;
        agent.enabled = true;

        gameObject.GetComponent<NavMeshAgent>().enabled = true;
        agent.updateRotation = false;
        agent.updateUpAxis = false;

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

        selectable = true;

        myAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!agent.hasPath)
            return;

        if (agent.remainingDistance > agent.stoppingDistance)
            return;

        agent.ResetPath();
    }

    private void OnDestroy()
    {
        OnDeUnitSpawned?.Invoke(this);
    }

    public void Move()
    {
        MoveTo(Utils.GetMouseWorldPosition());
    }

    public void MoveTo(Vector3 dest)
    {
        myAnimator.SetBool("isRunning", true);
        dest.z = 0;
        agent.SetDestination(dest);
    }

    public Vector3 getDest()
    {
        return agent.destination;
    }

    public void Stop()
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


}
