using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowAttack : Attacker
{
    [Header("Projectile Settings")]
    [SerializeField] private GameObject arrowPrefab = null;
    [SerializeField] private float arrowSpeed = 10f;

    public override void StopAttack()
    {
        GetComponent<Animator>().SetBool("isArcherAttacking", false);
    }

    public override void Attack()
    {
        GetComponent<Animator>().SetBool("isArcherAttacking", true); // the animation will call realseArrow func
    }

    public void realeseArrow(){
        Vector3 targetPos = target.transform.position;
        Vector3 targPos = target.transform.position;
        Vector3 archerPos = gameObject.transform.position;

        Vector3 objectPos = transform.position;
        targetPos.x = targetPos.x - objectPos.x;
        targetPos.y = targetPos.y - objectPos.y;

        Vector2 dir = new Vector2(targetPos.x, targetPos.y);
        
        float angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;

        Vector3 startPosArrow;
        if (archerPos.x < targPos.x)
          startPosArrow = new Vector3(archerPos.x + 12f, archerPos.y, archerPos.z);
        else startPosArrow = new Vector3(archerPos.x - 12f, archerPos.y, archerPos.z);

        GameObject arrow = Instantiate(arrowPrefab, startPosArrow, Quaternion.identity);
        Physics2D.IgnoreCollision(arrow.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (archerPos.y < targPos.y)
          arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 30, angle));
           

        Vector3 middle = new Vector3((startPosArrow.x + targPos.x) / 2,
                                     (startPosArrow.y + targPos.y) / 2, 0f);

        arrow.GetComponent<Projectile>().rotationCenter = middle;
        arrow.GetComponent<Projectile>().radius = Vector3.Distance(startPosArrow, targPos) / 2f; ;
        arrow.GetComponent<Projectile>().teamNumber = gameObject.GetComponent<Targetable>().teamNumber;
        arrow.GetComponent<Projectile>().targetPosition = targPos;

        arrow.GetComponent<Rigidbody2D>().velocity = dir.normalized * arrowSpeed;

    }
}
