using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Unit : MonoBehaviour
{
    private bool selectable;

    private NavMeshAgent agent;
    private Animator myAnimator;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private UnityEvent onSelected = null;
    [SerializeField] private UnityEvent onDeselected = null;


    private void Start()
    {
        selectable = true;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        myAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();


    }

    private void Update()
    {

    }

    public void MoveTo(Vector3 dest)
    {
        myAnimator.SetBool("isRunning", true);

        // Debug.Log("dest:" + dest);
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
