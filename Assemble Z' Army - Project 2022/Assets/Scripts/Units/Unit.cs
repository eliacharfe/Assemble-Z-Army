using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Utilities;

using Char.CharacterStat;

public class Unit : MonoBehaviour
{
    private bool selectable;
    public Building recrutingBuilding = null;

    private NavMeshAgent agent;
    private Animator myAnimator;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private Targeter targeter;

    [SerializeField] private UnityEvent onSelected = null;
    [SerializeField] private UnityEvent onDeselected = null;

    public static event Action<Unit> OnUnitSpawned;
    public static event Action<Unit> OnDeUnitSpawned;

    public Macros.Units id;
    private Vector3 destination;

    UnitMovement move = new UnitMovement(); 

    // StatModifier mod1, mod2;

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
    { }

    private void Update()
    {
        if (!agent.hasPath)
            return;

        if (agent.remainingDistance > agent.stoppingDistance)
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
        move.Move(this, agent, dest);
    }


    public bool ReachedDestination()
    {
        if (!agent.pathPending)
        {
            if (agent && agent.remainingDistance <= agent.stoppingDistance)
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
        if (recrutingBuilding)
        {
            recrutingBuilding.RemoveUnitFromWaitingList(this);
            recrutingBuilding = null;
        }
    }

    public Targeter GetTargeter()
    {
        return targeter;
    }

    public void Equip(Stat stat)
    {
        // We need to store our modifiers in variables before adding them to the stat.
        stat.Attack.AddModifier(new StatModifier(10, StatModType.Flat, this));
        stat.Attack.AddModifier(new StatModifier(0.1f, StatModType.PercentMult, this));
    }

    public void Unequip(Stat stat)
    {
        // Here we need to use the stored modifiers in order to remove them.
        // Otherwise they would be "lost" in the stat forever.
        stat.Attack.RemoveAllModifiersFromSource(this);
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