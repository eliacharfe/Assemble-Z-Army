using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BowAttack : Attacker
{
    [Header("Projectile Settings")]
    [SerializeField] private GameObject arrowPrefab = null;
    [SerializeField] private float arrowSpeed = 10f;

    [SerializeField] GameObject shootStartPoint;

    AudioPlayer audioPlayer;

    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    public override void StopAttackAnime()
    {
        GetComponent<Animator>().SetBool("isArcherAttacking", false);
    }

    public override void Attack()
    {
        GetComponent<Animator>().SetBool("isArcherAttacking", true);

    }

    [Command]
    public void realeseArrow()
    {
        if (!target)
            return;

        //RpcPlayBowSound();

        Vector3 targetPos = target.transform.position;
        Vector3 targPos = target.transform.position;
        Vector3 archerPos = gameObject.transform.position;

        Vector3 objectPos = transform.position;
        targetPos.x = targetPos.x - objectPos.x;
        targetPos.y = targetPos.y - objectPos.y;

        Vector2 dir = new Vector2(targetPos.x, targetPos.y);
        float angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;

        shootStartPoint.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        GameObject arrow = Instantiate(arrowPrefab, shootStartPoint.transform.position, shootStartPoint.transform.rotation);
        NetworkServer.Spawn(arrow, connectionToClient);

        Physics2D.IgnoreCollision(arrow.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        //audioPlayer.PlayShootingClip();

        FlipSideSprite(targPos);

        Vector3 middle = new Vector3((shootStartPoint.transform.position.x + targPos.x) / 2,
                                     (shootStartPoint.transform.position.y + targPos.y) / 2, 0f);

        arrow.GetComponent<Projectile>().rotationCenter = middle;
        arrow.GetComponent<Projectile>().radius = Vector3.Distance(shootStartPoint.transform.position, targPos) / 2f; ;
        //arrow.GetComponent<Projectile>().teamNumber = gameObject.GetComponent<Targetable>().teamNumber;
        arrow.GetComponent<Projectile>().targetPosition = targPos;
        arrow.GetComponent<Projectile>().archerPosition = archerPos;

        arrow.GetComponent<Projectile>().damage = (int)GetComponent<Unit>().Attack.BaseValue;

    }


    [ClientRpc]
    void RpcPlayBowSound()
    {
        audioPlayer.PlayShootingClip();
    }

    private void FlipSideSprite(Vector3 target)
    {
        Transform tr = gameObject.transform;

        if (target.x < tr.position.x && tr.localScale.x < Mathf.Epsilon)
        {
            tr.localScale = new Vector3(-tr.localScale.x, tr.localScale.y, tr.localScale.z);
        }
        else if (target.x > tr.position.x && tr.localScale.x > Mathf.Epsilon)
        {
            tr.localScale = new Vector3(-tr.localScale.x, tr.localScale.y, tr.localScale.z);
        }
    }
}
