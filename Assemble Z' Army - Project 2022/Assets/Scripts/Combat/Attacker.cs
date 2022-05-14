using UnityEngine;
using Mirror;

// Abstract class for multiple attacks types.
public abstract class Attacker : NetworkBehaviour
{
    public Targetable target = null;
    protected bool reachedTarget = false;
    protected bool isInAttackMode = false;
    protected UnitMovement movement = null;
    private bool isInModeAttackAutomated = false;

    [Header("Attack Settings")]
    [SerializeField] private float attackTime = 1f;
    [SerializeField] private float range = 1f;
    [SerializeField] protected float damage = 5;
    [SerializeField] protected float defense = 5;

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
        print("Have targer");
        GetComponent<Unit>().moveToDir = false;
        this.target = target;
    }
    #endregion


    public void SetAutomateAttack()
    {
        isInModeAttackAutomated = true;
    }

    public bool isInModeAttackAutomate()
    {
        return isInModeAttackAutomated;
    }

    public void SetTargetable(Targetable target)
    {
        this.target = target;
    }

    public abstract void StopAttackAnime();

    public abstract void Attack();

    public bool IsAttacking()
    {
        return isInAttackMode;
    }

    public void setAttackMode()
    {
        isInAttackMode = true;
    }
}
