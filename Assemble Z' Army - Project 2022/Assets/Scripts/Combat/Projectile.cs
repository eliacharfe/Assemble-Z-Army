using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Projectile : MonoBehaviour
{
    [Header("Projectile Settings")]
    [SerializeField] private int damageToDeal = 20;
    [SerializeField] private float destroyAfterSeconds = 5;

    public Vector3 rotationCenter, targetPosition;
    public float radius;
    private float posX, posY, angle, angleEnd;
    public int teamNumber;

    [SerializeField] private float AngularSpeed;

    private Vector3 arrowPos;

    UnitMovement move;

    private void Start()
    {
        // Debug.Log("radius: " + rad);
        // Debug.Log("mid: " + rotationCenter);

        // Debug.Log("Pos = "+ transform.position);
        //  Debug.Log("targPos = "+ targetPosition);

        // if (transform.position.y > targetPosition.y)
        // {
        //     Vector3 dest = new Vector3(transform.position.x, targetPosition.y, targetPosition.z);
        //     Debug.Log("dest: "+ dest);
        //     move.Move(GetComponent<NavMeshAgent>(), dest);
        // }

        // ToDo : need to calculate the correct angle start and end
        if (transform.position.x < targetPosition.x)
        {
            angle = Mathf.PI;
            angleEnd = 0f;
        }
        else
        {
            angle = 0f;
            angleEnd = Mathf.PI;
        }

        AngularSpeed = 1f;
    }
    //--------------------------------
    private void Update()
    {
        // if (transform.position.y > targetPosition.y)
        //     return;

        posX = rotationCenter.x + Mathf.Cos(angle) * radius;
        posY = rotationCenter.y + Mathf.Sin(angle) * radius;

        if (transform.position.x < targetPosition.x)
        {
            if (angle >= angleEnd)
                transform.position = new Vector3(posX, posY, 0f);
            else
                Destroy(gameObject);

            angle -= 0.1f + Time.deltaTime * AngularSpeed;
        }
        else
        {
            if (angle <= angleEnd)
                transform.position = new Vector3(posX, posY, 0f);
            else
                Destroy(gameObject);

            angle += 0.1f + Time.deltaTime * AngularSpeed;
        }
    }
    //----------------------------------------
    private void OnTriggerEnter2D(Collider2D collision)
    {
        arrowPos = transform.position;
        Health target = collision.GetComponent<Health>();
        int collisionTeamNumber = collision.gameObject.GetComponent<Targetable>().teamNumber;

        if (target && collisionTeamNumber != teamNumber
         && Vector3.Distance(arrowPos, targetPosition) < 7f)
        {
            target.DealDamage(damageToDeal);
        }

        if (collisionTeamNumber != teamNumber
         && Vector3.Distance(arrowPos, targetPosition) < 7f)
            Destroy(gameObject);
    }
}
