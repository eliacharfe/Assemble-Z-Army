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
        GetComponent<Animator>().SetBool("isAttacking", false);
    }

    public override void Attack()
    {
        Vector3 targetPos = target.transform.position;
        Vector3 targPos = target.transform.position;

        Vector3 objectPos = transform.position;
        targetPos.x = targetPos.x - objectPos.x;
        targetPos.y = targetPos.y - objectPos.y;

        Vector2 dir = new Vector2(targetPos.x, targetPos.y);

        GetComponent<Animator>().SetBool("isAttacking", true);

        float angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
        
        GameObject arrow = Instantiate(arrowPrefab, gameObject.transform.position, Quaternion.identity);
        Physics2D.IgnoreCollision(arrow.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        float radius = Vector3.Distance(transform.position, targPos) / 2f;
        Vector3 middle = new Vector3((gameObject.transform.position.x + targPos.x) / 2,
                                     (gameObject.transform.position.y + targPos.y) / 2, 0f);

        arrow.GetComponent<Projectile>().rad = radius;
        arrow.GetComponent<Projectile>().rotationCenter = middle;
        arrow.GetComponent<Projectile>().teamNumber = gameObject.GetComponent<Targetable>().teamNumber;
        arrow.GetComponent<Projectile>().targetPosition = targPos;

        arrow.GetComponent<Rigidbody2D>().velocity = dir.normalized * arrowSpeed;
    }
}
