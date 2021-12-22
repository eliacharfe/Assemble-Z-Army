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

    public static event Action<Unit> OnUnitSpawned;
    public static event Action<Unit> OnDeUnitSpawned;

    public Macros.Units id;

    private Vector3 destination;

    private void Start()
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
        // Get the position mouse is clicked on by using ray.
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        MoveTo(hit.point);
    }

    public void MoveTo(Vector3 dest)
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
         dest = hit.point;
        //   MoveTo(hit.point);
        myAnimator.SetBool("isRunning", true);
        dest.z = 0;
        agent.SetDestination(dest);

        FlipSideSprite(dest);
    }

    public bool ReachedDestination()
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance < agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                   return true;
                }
            }
        }
        return false;
    }

    // public Vector3 getDestinaion()
    // {
    //     return destination;
    // }

    // public Vector3 getPosition()
    // {
    //     return agent.destination;// transform.position;
    // }

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