using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Mirror;
using Utilities;

using Char.CharacterStat;
using Macros;

public class Unit : NetworkBehaviour
{
    private bool selectable;
    private Building recrutingBuilding = null;

    private NavMeshAgent agent;
    private Animator myAnimator;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private UnityEvent onSelected = null;
    [SerializeField] private UnityEvent onDeselected = null;

    [SerializeField] private SpriteRenderer selectionCircle = null;


    // Server Unit spawned event.
    public static event Action<Unit> ServerOnUnitSpawned;
    // Server Unit despawned event.
    public static event Action<Unit> ServerOnUnitDeSpawned;

    // Authorty Unit spawned event. 
    public static event Action<Unit> AuthortyOnUnitSpawned;
    // Authorty Unit despawned event. 
    public static event Action<Unit> AuthortyOnUnitDeSpawned;

    public Macros.Units id;
    private Vector3 destination;
    UnitMovement move;

    private CapsuleCollider2D myBoxCollider = null;

    [SyncVar]public bool isDead;

    public CharacterStat Speed;
    public CharacterStat Attack;
    public CharacterStat Defense;
    public CharacterStat ReachDistance;
    public CharacterStat SpeedAttack;

    // StatModifier mod1, mod2;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        agent.enabled = false;
        agent.enabled = true;
        gameObject.GetComponent<NavMeshAgent>().enabled = true;
        agent.updateRotation = agent.updateUpAxis = false;

        agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance; // setting Quality avoidance to none

        selectable = true;
        isDead = false;

        myAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        move = GetComponent<UnitMovement>();

        myBoxCollider = GetComponent<CapsuleCollider2D>();

        InitStats(id);

        if (selectionCircle)
        {
            selectionCircle.color = getTeamColor();
        }

        if (gameObject.GetComponent<Targetable>())
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y,
            transform.localScale.z);
        }
    }

    #region server
    public override void OnStartServer()
    {
        ServerOnUnitSpawned?.Invoke(this);
    }

    public override void OnStopServer()
    {
        ServerOnUnitDeSpawned?.Invoke(this);

        Destroy(this);
    }

    [Command]
    public void CmdBuildAnimation()
    {
        print("Start Build animation in command");
        GetComponent<Animator>().SetBool("isAttacking", true);
    }
    #endregion


    #region Authority
    public override void OnStartAuthority()
    {
        if (!hasAuthority) return;
        AuthortyOnUnitSpawned?.Invoke(this);

    }

    public override void OnStopAuthority()
    {
        if (!hasAuthority) return;
       AuthortyOnUnitDeSpawned?.Invoke(this);
       Destroy(this);
    }
    #endregion

    #region client
    public override void OnStartClient()
    {
    }

    public override void OnStopClient()
    {
        Destroy(this);
    }
    #endregion

    //--------------------------------------
    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

        if (!agent.hasPath)
            return;

        if (agent.remainingDistance > agent.stoppingDistance)
            return;

        agent.ResetPath();
        StopAnimation();
    }
    //-------------------------------------
    void OnTriggerEnter2D(Collider2D other)
    {
        if (myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Water")))
            agent.speed = Speed.BaseValue / 3; // when collide with water
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Water")))
            agent.speed = Speed.BaseValue / 3; // inside water
    }

    void OnTriggerExit2D(Collider2D other)
    {
        agent.speed = Speed.BaseValue; // exit from trigger
    }
    //----------------------------
    public void SetPostion(Vector3 pos)
    {
        gameObject.transform.position = pos;
    }

    //----------------------------
    public void ReintilizeNavMesh()
    {
        agent.enabled = false;
    }

    //----------------------------
    public void MoveTo(Vector3 dest)
    {
        move.CmdMove(dest);
    }
    //------------------------------
    public bool ReachedDestination()
    {
        if (isDead)
        {
            return false;
        }

        if (agent && !agent.enabled)
        {
            agent.enabled = true;
            return false;
        }


        if (agent && !agent.pathPending)
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

    //----------------------------
    public void StopAnimation()
    {
        myAnimator.SetBool("isRunning", false);
    }

    //----------------------------
    public void SetColorSelcted()
    {
        spriteRenderer.color = new Color(1f, 0f, 0f, 1f);
    }

    //----------------------------
    public void ResetColor()
    {
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
    }

    //----------------------------
    public bool isSelectable()
    {
        return selectable;
    }

    //----------------------------
    public void Select()
    {
        onSelected?.Invoke();
    }

    //----------------------------
    public void Deselect()
    {
        onDeselected?.Invoke();
    }
    //----------------------------
    public void StopMove()
    {
        agent.velocity = Vector3.zero;

        StopAnimation();

        agent.ResetPath();

        GetComponent<UnitMovement>().isMoving = false;
    }


    [Command]
    public void CmdStopMove()
    {
        agent.velocity = Vector3.zero;

        StopAnimation();

        agent.ResetPath();

        GetComponent<UnitMovement>().isMoving = false;
    }

    //---------------------------
    [Command]
    public void SetDead()
    {
        print("Unit " + id + " belong to " + connectionToClient + " is dead");
        isDead = true;
    }
    //----------------------------
    public void ContinutMove()
    {
        agent.isStopped = false;
    }
    //----------------------------
    public bool isMoving()
    {
        return !agent.isStopped;
    }
    //----------------------------
    public void SetBuildingRecruiting(Building building)
    {
        recrutingBuilding = building;
    }
    //----------------------------
    public void RemoveBuildingRecruiting()
    {
        if (recrutingBuilding)
        {
            recrutingBuilding.RemoveUnitFromWaitingList(this);
            recrutingBuilding = null;
        }
    }
    //----------------------------
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

    // Todo- delete later.
    private Color getTeamColor()
    {
        if (gameObject.GetComponent<Targetable>())
        {
            //return gameObject.GetComponent<Targetable>().teamNumber == 0 ? Color.red : Color.green;
        }
        return Color.green;
    }

    //-------------------
    private void InitStats(Units id)
    {
        switch (id)
        {
            case Units.SWORDMAN:
                {
                    Speed.BaseValue = agent.speed = 10f;
                    Attack.BaseValue = 15f;
                    Defense.BaseValue = 5f;
                    ReachDistance.BaseValue = 2f;
                    SpeedAttack.BaseValue = 1f;
                    break;
                };
            case Units.ARCHER:
                {
                    Speed.BaseValue = agent.speed = 10f;
                    Attack.BaseValue = 18f;
                    Defense.BaseValue = 5f;
                    ReachDistance.BaseValue = 50f;
                    SpeedAttack.BaseValue = 1.4f;
                    break;
                };

            case Units.CROSSBOW:
                {
                    Speed.BaseValue = agent.speed = 10f;
                    Attack.BaseValue = 12f;
                    Defense.BaseValue = 5f;
                    ReachDistance.BaseValue = 150f; // to change
                    SpeedAttack.BaseValue = 1f;
                    break;
                };
            case Units.SWORD_KNIGHT:
                {
                    Speed.BaseValue = agent.speed = 5f;
                    Attack.BaseValue = 20f;
                    Defense.BaseValue = 11f;
                    ReachDistance.BaseValue = 15f;
                    SpeedAttack.BaseValue = 1.4f;
                    break;
                };
            case Units.SIMPLE_HORSE:
                {
                    Speed.BaseValue = agent.speed = 20f;
                    Attack.BaseValue = 10f;
                    Defense.BaseValue = 5f;
                    ReachDistance.BaseValue = 4f;
                    SpeedAttack.BaseValue = 2f;
                    break;
                };
            case Units.SWORD_HORSE:
                {
                    Speed.BaseValue = agent.speed = 20f;
                    Attack.BaseValue = 25f;
                    Defense.BaseValue = 9f;
                    ReachDistance.BaseValue = 5f;
                    SpeedAttack.BaseValue = 1.5f;
                    break;
                };
            case Units.SWORD_HORSE_KNIGHT:
                {
                    Speed.BaseValue = agent.speed = 10f;
                    Attack.BaseValue = 30f;
                    Defense.BaseValue = 12f;
                    ReachDistance.BaseValue = 7f;
                    SpeedAttack.BaseValue = 1.7f;
                    break;
                };
            case Units.ARCHER_HORSE:
                {
                    Speed.BaseValue = agent.speed = 20f;
                    Attack.BaseValue = 15f;
                    Defense.BaseValue = 10f;
                    ReachDistance.BaseValue = 44f;
                    SpeedAttack.BaseValue = 1.4f;
                    break;
                };
            case Units.WORKER:
                {
                    Speed.BaseValue = agent.speed = 10f;
                    Attack.BaseValue = 5f;
                    Defense.BaseValue = 0f;
                    ReachDistance.BaseValue = 10f;
                    SpeedAttack.BaseValue = 2f;
                    break;
                };
            case Units.RECRUIT:
                {
                    Speed.BaseValue = agent.speed = 10f;
                    Attack.BaseValue = 5f;
                    Defense.BaseValue = 0f;
                    ReachDistance.BaseValue = 5f;
                    SpeedAttack.BaseValue = 1f;
                    break;
                };
            case Units.SPEARMAN:
                {
                    Speed.BaseValue = agent.speed = 5f;
                    Attack.BaseValue = 10f;
                    Defense.BaseValue = 5f;
                    ReachDistance.BaseValue = 7f;
                    SpeedAttack.BaseValue = 1.5f;
                    break;
                }
            case Units.SPEAR_KNIGHT:
                {
                    Speed.BaseValue = agent.speed = 5f;
                    Attack.BaseValue = 15f;
                    Defense.BaseValue = 10f;
                    ReachDistance.BaseValue = 12f;
                    SpeedAttack.BaseValue = 1.5f;
                    break;
                }
            case Units.SPEAR_HORSE:
                {
                    Speed.BaseValue = agent.speed = 20f;
                    Attack.BaseValue = 20f;
                    Defense.BaseValue = 5f;
                    ReachDistance.BaseValue = 12f;
                    SpeedAttack.BaseValue = 1.8f;
                    break;
                };
            case Units.SPEAR_HORSE_KNIGHT:
                {
                    Speed.BaseValue = agent.speed = 10f;
                    Attack.BaseValue = 25f;
                    Defense.BaseValue = 15f;
                    ReachDistance.BaseValue = 12f;
                    SpeedAttack.BaseValue = 2f;
                    break;
                };
            case Units.SCOUT:
                {
                    Speed.BaseValue = agent.speed = 80f;
                    Attack.BaseValue = 5f;
                    Defense.BaseValue = 0f;
                    ReachDistance.BaseValue = 10f;
                    SpeedAttack.BaseValue = 1f;
                    break;
                };
            case Units.HEALER:
                {
                    Speed.BaseValue = agent.speed = 10f;
                    Attack.BaseValue = 5f;
                    Defense.BaseValue = 0f;
                    ReachDistance.BaseValue = 10f;
                    SpeedAttack.BaseValue = 1.5f;
                    break;
                };
        }
    }


}




    // private float GetSpeed(Units id)
    // {
    //     switch (id)
    //     {
    //         case Units.SWORDMAN: return 30f;
    //         case Units.ARCHER: return 30f;
    //         case Units.SWORD_KNIGHT: return 20f;
    //         case Units.SIMPLE_HORSE: return 65f;
    //         case Units.SWORD_HORSE: return 60f;
    //         case Units.SWORD_HORSE_KNIGHT: return 50f;
    //         case Units.WORKER: return 25f;
    //         case Units.SPEARMAN: return 20f;
    //             // ...
    //     }
    //     return 30f;
    // }

    // private float GetAttack(Units id)
    // {
    //     switch (id)
    //     {
    //         case Units.SWORDMAN: return 15f;
    //         case Units.ARCHER: return 10f;
    //         case Units.SWORD_KNIGHT: return 20f;
    //         case Units.SIMPLE_HORSE: return 10f;
    //         case Units.SWORD_HORSE: return 25f;
    //         case Units.SWORD_HORSE_KNIGHT: return 30f;
    //         case Units.WORKER: return 5f;
    //         case Units.SPEARMAN:
    //             {
    //                 // add power 25 against horses
    //                 return 10f;
    //             }
    //             // ...
    //     }
    //     return 10f;
    // }

    // private float GetDefense(Units id)
    // {
    //     switch (id)
    //     {
    //         case Units.SWORDMAN: return 5f;
    //         case Units.ARCHER: return 5f;
    //         case Units.SWORD_KNIGHT: return 15f;
    //         case Units.SIMPLE_HORSE: return 5f;
    //         case Units.SWORD_HORSE: return 10f;
    //         case Units.SWORD_HORSE_KNIGHT: return 20f;
    //         case Units.WORKER: return 0f;
    //         case Units.SPEARMAN: return 5f;
    //             // ...
    //     }
    //     return 5f;
    // }

    // private float GetReachedDistance(Units id)
    // {
    //     switch (id)
    //     {
    //         case Units.SWORDMAN: return 10f;
    //         case Units.ARCHER: return 50f;
    //         case Units.SWORD_KNIGHT: return 15f;
    //         case Units.SIMPLE_HORSE: return 10f;
    //         case Units.SWORD_HORSE: return 15f;
    //         case Units.SWORD_HORSE_KNIGHT: return 20f;
    //         case Units.WORKER: return 5f;
    //         case Units.SPEARMAN: return 20f;
    //             // ...
    //     }
    //     return 10f;
    // }

    // private float GetSpeedAttack(Units id)
    // {
    //     switch (id)
    //     {
    //         case Units.SWORDMAN: return 1f;
    //         case Units.ARCHER: return 1f;
    //         case Units.SWORD_KNIGHT: return 1f;
    //         case Units.SIMPLE_HORSE: return 2f;
    //         case Units.SWORD_HORSE: return 1.5f;
    //         case Units.SWORD_HORSE_KNIGHT: return 1.5f;
    //         case Units.WORKER: return 1.5f;
    //         case Units.SPEARMAN: return 1.5f;
    //             // ...
    //     }
    //     return 1f;
    // }
