using UnityEngine;
using UnityEngine.AI;
using Mirror;

public class UnitMovement : NetworkBehaviour
{
    private NavMeshAgent agent = null;
    private Unit unit = null;
    public bool isMoving = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        unit = GetComponent<Unit>();
    }

    #region server
    [Command]
    public void CmdMove(Vector3 dest)
    {
        dest.z = 0;
        if (!unit || unit.isDead || Vector3.Distance(unit.transform.position, dest) <= 4f)
        {
            return;
        }
        GetComponent<Animator>().SetBool("isRunning", true);
        agent.SetDestination(dest);
        FlipSideSprite(dest);
        isMoving = true;
    }
    #endregion

    public void Move(Vector3 dest)
    {
        dest.z = 0;
        GetComponent<Animator>().SetBool("isRunning", true);
        agent.SetDestination(dest);
        FlipSideSprite(dest);
    }

    // Flip the sprite render to the relevent direction
    public void FlipSideSprite(Vector3 dest)
    {
        Transform tr = gameObject.transform;
        Transform healthBar = transform.Find("Health Bar Canvas");
        Transform manaBar = transform.Find("Mana Bar Canvas");

        if (dest.x < tr.position.x && tr.localScale.x < Mathf.Epsilon)
        {
            tr.localScale = new Vector3(-tr.localScale.x, tr.localScale.y,                             tr.localScale.z);
            healthBar.localScale = new Vector3(-healthBar.localScale.x, healthBar.localScale.y, healthBar.localScale.z);
            if (manaBar)
            {
                manaBar.localScale = new Vector3(-manaBar.localScale.x, manaBar.localScale.y, manaBar.localScale.z);
            }
        }
        else if (dest.x > tr.position.x && tr.localScale.x > Mathf.Epsilon)
        {
            tr.localScale = new Vector3(-tr.localScale.x, tr.localScale.y,                            tr.localScale.z);
            healthBar.localScale = new Vector3(-healthBar.localScale.x, healthBar.localScale.y, healthBar.localScale.z);
            if (manaBar)
            {
                manaBar.localScale = new Vector3(-manaBar.localScale.x, manaBar.localScale.y, manaBar.localScale.z);
            }
        }
    }
    public bool IsMoving()
    {
        return agent.hasPath || isMoving;
    }

    public void PlayFootStepsSound()
    {
        FindObjectOfType<AudioPlayer>().PlayStepClip();
    }

    public void PlayHorseStepsSound()
    {
        FindObjectOfType<AudioPlayer>().PlayHorseGallopClip();
    }
}
