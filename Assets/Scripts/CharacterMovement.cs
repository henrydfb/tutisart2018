using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : PhysicsObject
{
    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 70;

    private SpriteRenderer spriteRenderer;
    private bool doubleJump = false;

    private bool leftMove = false;
    private bool rightMove = false;

    private bool idling = false;
    private bool jumping = false;
    private bool falling = false;
    private bool walking = false;
    Vector3 prevPos;
    GameObject container;
    PlayerController playerController;

    // Use this for initialization
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        container = transform.Find("Container").gameObject;
        playerController = GetComponent<PlayerController>();
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        leftMove = Input.GetAxisRaw("Horizontal") < 0; //Input.GetKey("q");
        rightMove = Input.GetAxisRaw("Horizontal") > 0; //Input.GetKey("d");

        if (leftMove && rightMove)
        {
            move.x = 0;
            if (!idling)
            {
                if (!jumping && !playerController.IsSpawningCloud())
                   container.GetComponent<Animator>().SetTrigger("Idle");
                idling = true;
            }
        }
        else if (rightMove)
        {
            move.x = 1;
            if(!jumping && !walking && !playerController.IsSpawningCloud())
                container.GetComponent<Animator>().SetTrigger("Walk");
            transform.localScale = new Vector3(1, 1, 1);
            idling = false;
            walking = true;
        }
        else if (leftMove)
        {
            move.x = -1;
            if (!jumping && !walking && !playerController.IsSpawningCloud())
                container.GetComponent<Animator>().SetTrigger("Walk");
            transform.localScale = new Vector3(-1, 1, 1);
            idling = false;
            walking = true;
        }
        else
        {
            if (!idling)
            {
                if (!jumping && !playerController.IsSpawningCloud())
                    container.GetComponent<Animator>().SetTrigger("Idle");
                idling = true;
                walking = false;
            }
        }

        if (grounded)
            doubleJump = false;

        if (Input.GetButtonDown("Jump") && grounded)
            container.GetComponent<Animator>().SetTrigger("Jump");
        else if (Input.GetButtonDown("Jump") && !doubleJump)
        {
            velocity.y = jumpTakeOffSpeed * 1.5f;
            doubleJump = true;
            container.GetComponent<Animator>().SetTrigger("DirectJump");
        }
        else if (Input.GetButtonUp("Jump"))
        {
            if (velocity.y > 0)
                velocity.y = velocity.y * 0.5f;
        }

        targetVelocity = move * maxSpeed;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        
        if (!falling && jumping)
        {
            if (prevPos.y > transform.position.y)
            {
                container.GetComponent<Animator>().SetTrigger("Fall");
                falling = true;
            }
        }

        if ((jumping || falling) && grounded)
        {
            container.GetComponent<Animator>().SetTrigger("Land");
            jumping = false;
            falling = false;
            container.transform.localEulerAngles = new Vector3(0, 0, 0);
        }

        prevPos = transform.position;
    }
    
    public void StartJumpEnded()
    {
        velocity.y = jumpTakeOffSpeed;
        jumping = true;
        falling = false;
        walking = false;
    }
}