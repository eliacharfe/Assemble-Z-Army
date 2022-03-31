using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CBProjectile : MonoBehaviour
{
    [Header("CB Projectile Settings")]
    [SerializeField] private float destroyAfterSeconds;

    public int teamNum;
    public float damage;

    private float time;

    private void Start()
    {
        time = 0;
        destroyAfterSeconds = 0.5f;
    }

    private void Update()
    {
        if (time < destroyAfterSeconds)
        {
            time += Time.deltaTime;
        }
        else
        {
            Debug.Log("destroy");
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health target = collision.GetComponent<Health>();
        int collisionTeamNumber = GetComponent<RtsNetworkManager>().Players.Count ;
        if (target)
        {  // check case collides with a world object thet is static object (like water) that dont have Targetable
            collisionTeamNumber  = collision.gameObject.GetComponent<Targetable>().teamNumber;
        }        
      
        if (target && collisionTeamNumber != teamNum)
        {
            target.DealDamage(damage);
        }
        Destroy(gameObject);
    }
}
