using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Projectile : MonoBehaviour
{
    [Header("Projectile Settings")]
    [SerializeField] private int damageToDeal = 20;
    //[SerializeField] private float destroyAfterSeconds = 5;

    public Vector3 rotationCenter, targetPosition, archerPosition;
    public float radius;
    private float posX, posY, angle, angleEnd;
    public int teamNumber;

    Transform targetTransform;
    Quaternion archerRotation = Quaternion.Euler(0, 0, 0);

    [SerializeField] private float AngularSpeed;

    private Vector3 arrowPos;

    UnitMovement move;

    private void Start()
    {
        Quaternion lookRot = Quaternion.LookRotation(targetPosition - archerPosition);
        Quaternion relativeRot = Quaternion.Inverse(archerRotation) * lookRot;
        Matrix4x4 m = Matrix4x4.Rotate(relativeRot);
        Vector4 mForward = m.GetColumn(2);
        Vector2 vec = new Vector2(mForward.x, mForward.y);
        float angleTarget = Mathf.Atan2(vec.y, vec.x);

        if (archerPosition.y < targetPosition.y)
        {
            if (transform.position.x < targetPosition.x)
            { // target in 1
                angle = angleTarget + Mathf.PI;
                angleEnd = angleTarget;
            }
            else
            { // target in 2
                angle = angleTarget - Mathf.PI;
                angleEnd = angleTarget;
            }
        }
        else
        {
            if (transform.position.x < targetPosition.x)
            { // target in 4
                angle = angleTarget - Mathf.PI;
                angleEnd = angleTarget - 2 * Mathf.PI;
            }
            else
            { // target in 3
                angle = angleTarget + Mathf.PI;
                angleEnd = angleTarget + 2 * Mathf.PI;
            }
        }

        AngularSpeed = 0.3f;
    }
    //--------------------------------
    private void Update()
    {
        posX = rotationCenter.x + Mathf.Cos(angle) * radius;
        posY = rotationCenter.y + Mathf.Sin(angle) * radius;

        if (archerPosition.x < targetPosition.x)
        {
            if (angle >= angleEnd)
                transform.position = new Vector3(posX, posY, 0f);
            else
                Destroy(gameObject);

            angle -= 0.05f + Time.deltaTime * AngularSpeed;
        }
        else
        {
            if (angle <= angleEnd)
                transform.position = new Vector3(posX, posY, 0f);
            else
                Destroy(gameObject);

            angle += 0.05f + Time.deltaTime * AngularSpeed;
        }
    }
    //----------------------------------------
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // arrowPos = transform.position;
        Health target = collision.GetComponent<Health>();
        int collisionTeamNumber = collision.gameObject.GetComponent<Targetable>().teamNumber;

        if (target && collisionTeamNumber != teamNumber)
        {
            target.DealDamage(damageToDeal);
            Destroy(gameObject);
        }
    }
}
