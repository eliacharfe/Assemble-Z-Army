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
    //[SerializeField] private UnitMovement unitMovement = null;


    private void Start()
    {
        selectable = true;
        gameObject.GetComponent<NavMeshAgent>().enabled = true;

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        myAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!agent.hasPath)
        {
            return;
        }

        if (agent.remainingDistance > agent.stoppingDistance)
        {
            return;
        }

        agent.ResetPath();
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

    public void Move()
    {
        MoveTo(Utils.GetMouseWorldPosition());
        // Camera.main.ScreenToWorldPoint(
        //     new Vector2(Utils.GetMouseWorldPosition().x, 
        //                 Utils.GetMouseWorldPosition().y));
    }


}
