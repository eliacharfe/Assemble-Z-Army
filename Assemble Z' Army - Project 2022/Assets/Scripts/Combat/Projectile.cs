using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Projectile : MonoBehaviour
{
    [Header("Projectile Settings")]
    public Vector3 rotationCenter, targetPosition, archerPosition;
    public float radius;
    private float posX, posY, angle, angleEnd;
    public int teamNumber;
    public float damage;

    Quaternion archerRotation = Quaternion.Euler(0, 0, 0);
    [SerializeField] private float AngularSpeed;
    private Vector3 arrowPos;

    private void Start()
    {
        Quaternion lookRot = Quaternion.LookRotation(targetPosition - archerPosition);
        Quaternion relativeRot = Quaternion.Inverse(archerRotation) * lookRot;
        Matrix4x4 matrix = Matrix4x4.Rotate(relativeRot);
        Vector4 mForward = matrix.GetColumn(2);
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
        Health target = collision.GetComponent<Health>();
        if (!target)
        {
            return;
        }

        int collisionTeamNumber = collision.gameObject.GetComponent<Targetable>().teamNumber;

        if (collisionTeamNumber != teamNumber)
        {
            target.DealDamage(damage);
        }

        Destroy(gameObject);
    }
}
