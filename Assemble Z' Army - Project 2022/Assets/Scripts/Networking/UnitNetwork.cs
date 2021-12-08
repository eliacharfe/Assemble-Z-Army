using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Mirror;

public class UnitNetwork : NetworkBehaviour
{
    private bool selectable;

    private NavMeshAgent agent;

    private Animator myAnimator;
    private SpriteRenderer spriteRenderer;

    // private bool unitSelected;
    // public static bool dragSelectedUnitAllowed, mouseOverUnit;
    // private Vector2 mousePos;
    // private float dragOffsetX, dragOffsetY;

    private void Start()
    {
        selectable = true;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        myAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // unitSelected = false;
        // dragSelectedUnitAllowed = false;
        // mouseOverUnit = false;
    }

    private void Update()
    {
        // if (Input.GetMouseButtonDown(0)){
        //     dragOffsetX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
        //     dragOffsetY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;
        // }

        // if(Input.GetMouseButton(0))
        //     mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // if (unitSelected && dragSelectedUnitAllowed)
        //     transform.position = new Vector2(mousePos.x - dragOffsetX, mousePos.y - dragOffsetY);

        // if (Input.GetMouseButtonDown(1)){
        //     unitSelected = false;
        //     dragSelectedUnitAllowed = false;
        //     spriteRenderer.color = new Color(1f,1f,1f,1f);
        // }
      // FlipSideSprite();
    }

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

    // private void OnTriggerEnter2D(Collider2D collision){
    //     if (collision.gameObject.GetComponent<BoxCollection>()){
    //         spriteRenderer.color = new Color(1f,0f,0f,1f);
    //         unitSelected = true;
    //     }
    // }

    // private void OnMouseDown(){
    //     mouseOverUnit = true;
    // }

    //  private void OnMouseUp(){
    //     mouseOverUnit = false;
    //     dragSelectedUnitAllowed = false;
    // }

    // private void OnMouseDrag(){
    //     dragSelectedUnitAllowed = true;

    //     if (!unitSelected){
    //         dragSelectedUnitAllowed = false;
    //     }
      
    //   mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //   transform.position = new Vector2(mousePos.x - dragOffsetX, mousePos.y - dragOffsetY);
    // }


    // public void FlipSideSprite(){
    //     transform.localScale = new Vector3 (Mathf.Sign(agent.velocity.x), 1f);
    // }
}
