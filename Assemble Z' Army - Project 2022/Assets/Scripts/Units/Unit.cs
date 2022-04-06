using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Utilities;

using Char.CharacterStat;
using Macros;

public class Unit : MonoBehaviour
{
    private bool selectable;
    public Building recrutingBuilding = null;

    private NavMeshAgent agent;
    private Animator myAnimator;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private UnityEvent onSelected = null;
    [SerializeField] private UnityEvent onDeselected = null;
    [SerializeField] private GameObject selectionAreaCircle = null;

    public static event Action<Unit> OnUnitSpawned;
    public static event Action<Unit> OnDeUnitSpawned;

    public Macros.Units id;
    private Vector3 destination;
    UnitMovement unitMovement;

    private CapsuleCollider2D myCapsuleCollider = null;

    [SerializeField] private CircleCollider2D myCircleDetectionAttackArea = null;

    private bool isDead;

    public CharacterStat Speed;
    public CharacterStat Attack;
    public CharacterStat Defense;
    public CharacterStat ReachDistance;
    public CharacterStat SpeedAttack;

    // StatModifier mod1, mod2;

    AudioPlayer audioPlayer;

    [SerializeField] private GameObject halo;
    [SerializeField] private GameObject haloBack;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        agent.enabled = false;
        agent.enabled = true;
        gameObject.GetComponent<NavMeshAgent>().enabled = true;
        agent.updateRotation = agent.updateUpAxis = false;

        agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance; // setting Quality avoidance to none

        OnUnitSpawned?.Invoke(this);

        selectable = true;
        isDead = false;

        audioPlayer = FindObjectOfType<AudioPlayer>();

        myAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        unitMovement = GetComponent<UnitMovement>();

        myCapsuleCollider = GetComponent<CapsuleCollider2D>();

        InitStats(id);

        InitColor();

        selectionAreaCircle.GetComponent<SpriteRenderer>().color = getTeamColor();

        if (gameObject.GetComponent<Targetable>() && gameObject.GetComponent<Targetable>().teamNumber != 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y,
            transform.localScale.z);
        }
    }


    //--------------------------
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
        if (myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Water")))
        {
            agent.speed = Speed.BaseValue / 3; // when collide with water
        }
        if (myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Mountain")))
        {
            agent.speed = Speed.BaseValue / 4; // when collide with mountain

            if (id == Units.ARCHER)
            {
                ReachDistance.BaseValue = 200f;
            }
        }

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Water")))
        {
            agent.speed = Speed.BaseValue / 3; // inside water
        }
        if (myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Mountain")))
        {
            agent.speed = Speed.BaseValue / 4; // on a mountain

            if (id == Units.ARCHER)
            {
                ReachDistance.BaseValue = 200f;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        agent.speed = Speed.BaseValue; // exit from trigger

        if (id == Units.ARCHER)
        {
            ReachDistance.BaseValue = 100f;
        }
    }


    //---------------------
    private void OnDestroy()
    {
        OnDeUnitSpawned?.Invoke(this);
    }
    //----------------------------
    public void MoveTo(Vector3 dest)
    {
        myAnimator.SetBool("isRunning", true);
        unitMovement.Move(agent, dest);
    }
    //------------------------------
    public bool ReachedDestination()
    {
        if (isDead)
        {
            return false;
        }

        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance ||
            Vector3.Distance(agent.destination, agent.transform.position) <= agent.stoppingDistance)
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
        GetComponent<UnitMovement>().isMoving = false;
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
        GetComponent<UnitMovement>().isMoving = false;
        agent.velocity = Vector3.zero;
        StopAnimation();
        agent.ResetPath();
    }
    //---------------------------
    public void SetDead()
    {
        isDead = true;
        myAnimator.SetBool("isDead", true);

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

    // Todo- delete later.
    private Color getTeamColor()
    {
        if (gameObject.GetComponent<Targetable>())
        {
            switch (gameObject.GetComponent<Targetable>().teamNumber)
            {
                case 0: return Color.green;
                case 1: return Color.red;
                case 2: return Color.blue;
                case 3: return Color.cyan;
            }


            // return gameObject.GetComponent<Targetable>().teamNumber == 0 ? Color.red : Color.green;
        }
        return Color.green;
    }


    //--------------
    public void StopHeal()
    {
        myAnimator.SetBool("isHealing", false);
    }
    //----------------
    public void StopConfusion()
    {
        myAnimator.SetBool("gotHit", false);
    }
    //-------------
    public void HorseGallop()
    {
        audioPlayer.PlayHorseGallopClip();
    }
    //---------------------------
    public void StopHorseGallop()
    {
        audioPlayer.StopHorseGallopClip();
    }
    //--------------------------
    public void Step()
    {
        audioPlayer.PlayStepClip();
    }

    //-------------------
    private void InitColor()
    {
        if (gameObject.GetComponent<Targetable>())
        {
            Color tempColor = getTeamColor();
            tempColor.a = 0.2f;
            halo.gameObject.GetComponent<SpriteRenderer>().color = tempColor;
            tempColor.a = 0.1f;
            haloBack.gameObject.GetComponent<SpriteRenderer>().color = tempColor;
        }
    }

    //-------------------
    private void InitStats(Units id)
    {
        switch (id)
        {
            case Units.SWORDMAN:
                {
                    Speed.BaseValue = agent.speed = 30f;
                    Attack.BaseValue = 15f;
                    Defense.BaseValue = 5f;
                    ReachDistance.BaseValue = 10f;
                    SpeedAttack.BaseValue = 1f;
                    break;
                };
            case Units.ARCHER:
                {
                    Speed.BaseValue = agent.speed = 30f;
                    Attack.BaseValue = 18f;
                    Defense.BaseValue = 5f;
                    ReachDistance.BaseValue = 100f;
                    SpeedAttack.BaseValue = 1.4f;
                    break;
                };

            case Units.CROSSBOW:
                {
                    Speed.BaseValue = agent.speed = 30f;
                    Attack.BaseValue = 12f;
                    Defense.BaseValue = 5f;
                    ReachDistance.BaseValue = 70f;
                    SpeedAttack.BaseValue = 1f;
                    break;
                };
            case Units.SWORD_KNIGHT:
                {
                    Speed.BaseValue = agent.speed = 20f;
                    Attack.BaseValue = 20f;
                    Defense.BaseValue = 11f;
                    ReachDistance.BaseValue = 15f;
                    SpeedAttack.BaseValue = 1.4f;
                    break;
                };
            case Units.SIMPLE_HORSE:
                {
                    Speed.BaseValue = agent.speed = 65f;
                    Attack.BaseValue = 10f;
                    Defense.BaseValue = 5f;
                    ReachDistance.BaseValue = 10f;
                    SpeedAttack.BaseValue = 2f;
                    break;
                };
            case Units.SWORD_HORSE:
                {
                    Speed.BaseValue = agent.speed = 60f;
                    Attack.BaseValue = 25f;
                    Defense.BaseValue = 9f;
                    ReachDistance.BaseValue = 12f;
                    SpeedAttack.BaseValue = 1.5f;
                    break;
                };
            case Units.SWORD_HORSE_KNIGHT:
                {
                    Speed.BaseValue = agent.speed = 50f;
                    Attack.BaseValue = 30f;
                    Defense.BaseValue = 12f;
                    ReachDistance.BaseValue = 15f;
                    SpeedAttack.BaseValue = 1.7f;
                    break;
                };
            case Units.ARCHER_HORSE:
                {
                    Speed.BaseValue = agent.speed = 60f;
                    Attack.BaseValue = 15f;
                    Defense.BaseValue = 10f;
                    ReachDistance.BaseValue = 100f;
                    SpeedAttack.BaseValue = 1.4f;
                    break;
                };
            case Units.WORKER:
                {
                    Speed.BaseValue = agent.speed = 25f;
                    Attack.BaseValue = 5f;
                    Defense.BaseValue = 0f;
                    ReachDistance.BaseValue = 10f;
                    SpeedAttack.BaseValue = 2f;
                    break;
                };
            case Units.RECRUIT:
                {
                    Speed.BaseValue = agent.speed = 35f;
                    Attack.BaseValue = 5f;
                    Defense.BaseValue = 0f;
                    ReachDistance.BaseValue = 5f;
                    SpeedAttack.BaseValue = 1f;
                    break;
                };
            case Units.SPEARMAN:
                {
                    Speed.BaseValue = agent.speed = 20f;
                    Attack.BaseValue = 10f;
                    Defense.BaseValue = 5f;
                    ReachDistance.BaseValue = 15f;
                    SpeedAttack.BaseValue = 1.5f;
                    break;
                }
            case Units.SPEAR_KNIGHT:
                {
                    Speed.BaseValue = agent.speed = 15f;
                    Attack.BaseValue = 15f;
                    Defense.BaseValue = 10f;
                    ReachDistance.BaseValue = 20f;
                    SpeedAttack.BaseValue = 1.5f;
                    break;
                }
            case Units.SPEAR_HORSE:
                {
                    Speed.BaseValue = agent.speed = 55f;
                    Attack.BaseValue = 20f;
                    Defense.BaseValue = 5f;
                    ReachDistance.BaseValue = 20f;
                    SpeedAttack.BaseValue = 1.8f;
                    break;
                };
            case Units.SPEAR_HORSE_KNIGHT:
                {
                    Speed.BaseValue = agent.speed = 50f;
                    Attack.BaseValue = 25f;
                    Defense.BaseValue = 15f;
                    ReachDistance.BaseValue = 25f;
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
                    Speed.BaseValue = agent.speed = 40f;
                    Attack.BaseValue = 5f;
                    Defense.BaseValue = 0f;
                    ReachDistance.BaseValue = 10f;
                    SpeedAttack.BaseValue = 1.5f;
                    break;
                };
        }
    }
}
