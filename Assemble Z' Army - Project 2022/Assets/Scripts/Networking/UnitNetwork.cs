using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Mirror;
using System;

public class UnitNetwork : NetworkBehaviour
{
    private bool selectable;

    private NavMeshAgent agent;

    private Animator myAnimator;
    private SpriteRenderer spriteRenderer;

    // Server Unit spawned event.
    public static event Action<UnitNetwork> ServerOnUnitSpawned;
    // Server Unit despawned event.
    public static event Action<UnitNetwork> ServerOnUnitDeSpawned;

    // Authorty Unit spawned event. 
    public static event Action<UnitNetwork> AuthortyOnUnitSpawned;
    // Authorty Unit despawned event. 
    public static event Action<UnitNetwork> AuthortyOnUnitDeSpawned;

    private void Start()
    {
        selectable = true;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        myAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    #region Server
    public override void OnStartServer()
    {
        base.OnStartServer();

        ServerOnUnitSpawned?.Invoke(this);
    }

    public override void OnStopServer()
    {
        base.OnStopAuthority();

        ServerOnUnitDeSpawned?.Invoke(this);
    }

    #endregion


    #region Client
    public override void OnStartAuthority()
    {
        base.OnStartAuthority();

        AuthortyOnUnitSpawned?.Invoke(this);   
    }

    public override void OnStopAuthority()
    {
        base.OnStopAuthority();

        AuthortyOnUnitDeSpawned?.Invoke(this);
    }

    #endregion

    public void MoveTo(Vector3 dest)
    {
        myAnimator.SetBool("isRunning", true);

       // Debug.Log("dest:" + dest);
        dest.z = 0;
        agent.SetDestination(dest);
    }

    public Vector3 getDest(){
        return agent.destination;
    }

    public void Stop(){
        myAnimator.SetBool("isRunning", false);
    }

    public void SetColorSelcted(){
        spriteRenderer.color = new Color(1f,0f,0f,1f);
    }

    public void ResetColor(){
        spriteRenderer.color = new Color(1f,1f,1f,1f);
    }

    public bool isSelectable(){
        return selectable;
    }

}
