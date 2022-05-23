using UnityEngine;
using Mirror;

// Abstract class for multiple attacks types.
public abstract class Attacker : NetworkBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private float attackTime = 1f;
    [SerializeField] private float range = 1f;
    [SerializeField] protected float damage = 5;
    [SerializeField] protected float defense = 5;

    public Targetable target = null;
    protected UnitMovement movement = null;
    protected bool reachedTarget = false;
 
    // Is currently attacking target
    protected bool isInAttackMode = false;
 
    // Has already detected target with circle detection
    private bool isInModeAttackAutomated = false;

    private float time = 0;

    private void Start()
    {
        movement = GetComponent<UnitMovement>();
        attackTime = GetComponent<Unit>().SpeedAttack.BaseValue;
        damage = GetComponent<Unit>().Attack.BaseValue;
        range = GetComponent<Unit>().ReachDistance.BaseValue;
        defense = GetComponent<Unit>().Defense.BaseValue;
    }

    #region server

    [ServerCallback]
    private void Update()
    {
        if (GetComponent<Unit>().isDead) { return; }

        if (!target || target.IsDead() || GetComponent<Unit>().moveToDir)
        {
            isInModeAttackAutomated = false;

            StopAttackAnime();
            target = null;
            reachedTarget = false;
            isInAttackMode = false;
            return;
        }

        if (Vector2.Distance(gameObject.transform.position, this.target.transform.position) < range)
        {
            GetComponent<Unit>().moveToDir = false;
            GetComponent<Unit>().StopMove();
            if (time < attackTime)
            {
                time += Time.deltaTime;
            }
            else
            {
                time = 0;
                reachedTarget = true;
                Attack();
            }
        }
        else
        {
            reachedTarget = false;
            StopAttackAnime();
            if (target && movement)
            {
                movement.Move(this.target.transform.position);
            }
        }
    }

    [Command]
    public void CmdSetTargetable(Targetable target)
    {
        GetComponent<Unit>().moveToDir = false;
        this.target = target;
    }
    #endregion
 
    public abstract void StopAttackAnime();

    public abstract void Attack();

    public bool isInModeAttackAutomate()
    {
        return isInModeAttackAutomated;
    }

    public bool IsAttacking()
    {
        return isInAttackMode;
    }

    public void SetAutomateAttack()
    {
        isInModeAttackAutomated = true;
    }

    public void SetTargetable(Targetable target)
    {
        this.target = target;
    }

    public void SetAttackMode()
    {
        isInAttackMode = true;
    }
}
