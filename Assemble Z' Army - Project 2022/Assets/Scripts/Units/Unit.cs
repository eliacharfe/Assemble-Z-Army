using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Mirror;

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

    public static event Action<Unit> ServerOnUnitSpawned;
    public static event Action<Unit> ServerOnUnitDeSpawned;
    public static event Action<Unit> AuthortyOnUnitSpawned;
    public static event Action<Unit> AuthortyOnUnitDeSpawned;

    public Units id;
    UnitMovement move;

    private CapsuleCollider2D myBoxCollider = null;

    [SyncVar]public bool isDead;
    [SyncVar]public bool moveToDir;

    public CharacterStat Speed;
    public CharacterStat Attack;
    public CharacterStat Defense;
    public CharacterStat ReachDistance;
    public CharacterStat SpeedAttack;

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

        if (gameObject.GetComponent<Targetable>())
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y,
            transform.localScale.z);
        }
    }

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
        GetComponent<Animator>().SetBool("isAttacking", true);
    }

    [ServerCallback]
    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

        if (!agent.hasPath)
            return;

        if (agent.remainingDistance > agent.stoppingDistance)
            return;

        agent.ResetPath();
        StopAnimation();
        moveToDir = false;
    }

    [Command]
    public void CmdStopMove()
    {
        agent.velocity = Vector3.zero;

        StopAnimation();

        agent.ResetPath();

        GetComponent<UnitMovement>().isMoving = false;

        moveToDir = false;
    }

    [Command]
    public void CmdSetDead()
    {
        isDead = true;
    }

    [Command]
    void CmdOrderToMove()
    {
        moveToDir = true;
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

    public override void OnStopClient()
    {
        Destroy(this);
    }
    #endregion

    public void SetPostion(Vector3 pos)
    {
        gameObject.transform.position = pos;
    }

    public void ReintilizeNavMesh()
    {
        agent.enabled = false;
    }

    public void MoveTo(Vector3 dest)
    {
        CmdOrderToMove();
        move.CmdMove(dest);
    }

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
        agent.velocity = Vector3.zero;

        StopAnimation();

        agent.ResetPath();

        GetComponent<UnitMovement>().isMoving = false;
    }

    public void SetDead()
    {
        isDead = true;
    }

    public void ContinutMove()
    {
        agent.isStopped = false;
    }

    public bool isMoving()
    {
        return !agent.isStopped;
    }

    public Building GetBuildingRecruiting()
    {
        return recrutingBuilding;
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

    private void InitStats(Units id)
    {
        switch (id)
        {
            case Units.SWORDMAN:
                {
                    Speed.BaseValue = agent.speed = 10f;
                    Attack.BaseValue = 15f;
                    Defense.BaseValue = 5f;
                    ReachDistance.BaseValue = 5f;
                    SpeedAttack.BaseValue = 1f;
                    break;
                };
            case Units.ARCHER:
                {
                    Speed.BaseValue = agent.speed = 10f;
                    Attack.BaseValue = 10f;
                    Defense.BaseValue = 0f;
                    ReachDistance.BaseValue = 18f;
                    SpeedAttack.BaseValue = 1.4f;
                    break;
                };

            case Units.CROSSBOW:
                {
                    Speed.BaseValue = agent.speed = 10f;
                    Attack.BaseValue = 12f;
                    Defense.BaseValue = 5f;
                    ReachDistance.BaseValue = 15f; 
                    SpeedAttack.BaseValue = 1f;
                    break;
                };
            case Units.SWORD_KNIGHT:
                {
                    Speed.BaseValue = agent.speed = 7f;
                    Attack.BaseValue = 20f;
                    Defense.BaseValue = 8f;
                    ReachDistance.BaseValue = 6f;
                    SpeedAttack.BaseValue = 1.4f;
                    break;
                };
            case Units.SIMPLE_HORSE:
                {
                    Speed.BaseValue = agent.speed = 15f;
                    Attack.BaseValue = 10f;
                    Defense.BaseValue = 5f;
                    ReachDistance.BaseValue = 4f;
                    SpeedAttack.BaseValue = 2f;
                    break;
                };
            case Units.SWORD_HORSE:
                {
                    Speed.BaseValue = agent.speed = 15f;
                    Attack.BaseValue = 15f;
                    Defense.BaseValue = 5f;
                    ReachDistance.BaseValue = 5f;
                    SpeedAttack.BaseValue = 1.5f;
                    break;
                };
            case Units.SWORD_HORSE_KNIGHT:
                {
                    Speed.BaseValue = agent.speed = 12f;
                    Attack.BaseValue = 20f;
                    Defense.BaseValue = 8f;
                    ReachDistance.BaseValue = 7f;
                    SpeedAttack.BaseValue = 1.7f;
                    break;
                };
            case Units.ARCHER_HORSE:
                {
                    Speed.BaseValue = agent.speed = 15f;
                    Attack.BaseValue = 10f;
                    Defense.BaseValue = 5f;
                    ReachDistance.BaseValue = 20f;
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
                    Attack.BaseValue = 10f;
                    Defense.BaseValue = 0f;
                    ReachDistance.BaseValue = 5f;
                    SpeedAttack.BaseValue = 1f;
                    break;
                };
            case Units.SPEARMAN:
                {
                    Speed.BaseValue = agent.speed = 10f;
                    Attack.BaseValue = 10f;
                    Defense.BaseValue = 5f;
                    ReachDistance.BaseValue = 5f;
                    SpeedAttack.BaseValue = 1.5f;
                    break;
                }
            case Units.SPEAR_KNIGHT:
                {
                    Speed.BaseValue = agent.speed = 7f;
                    Attack.BaseValue = 15f;
                    Defense.BaseValue = 7f;
                    ReachDistance.BaseValue = 6f;
                    SpeedAttack.BaseValue = 1.5f;
                    break;
                }
            case Units.SPEAR_HORSE:
                {
                    Speed.BaseValue = agent.speed = 15f;
                    Attack.BaseValue = 15f;
                    Defense.BaseValue = 5f;
                    ReachDistance.BaseValue = 6f;
                    SpeedAttack.BaseValue = 1.8f;
                    break;
                };
            case Units.SPEAR_HORSE_KNIGHT:
                {
                    Speed.BaseValue = agent.speed = 10f;
                    Attack.BaseValue = 15f;
                    Defense.BaseValue = 7f;
                    ReachDistance.BaseValue = 6f;
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