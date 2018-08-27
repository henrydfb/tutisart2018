using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : PhysicsObject
{
    public float maxSpeed = 7f;
    public float jumpTakeOffSpeed = 7f;
    public float walkingSpeed = 1f;
    public float runningSpeed = 3f;

    private SpriteRenderer spriteRenderer;
    private bool doubleJump = false;

    private bool leftMove = false;
    private bool rightMove = false;
    private bool isRunning = false;

    // Use this for initialization
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        UpdateKey();

        if (leftMove && rightMove)
            move.x = 0;
        else if (rightMove)
        {
            if(isRunning)
                move.x = runningSpeed;
            else
                move.x = walkingSpeed;
        }
        else if (leftMove)
        {
            if (isRunning)
                move.x = -runningSpeed;
            else
                move.x = -walkingSpeed;
        }

        if (Input.GetButtonDown("Jump") && grounded)
            velocity.y = jumpTakeOffSpeed;
        else if (Input.GetButtonDown("Jump") && !doubleJump)
        {
            velocity.y = jumpTakeOffSpeed * 1.5f;
            doubleJump = true;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            if (velocity.y > 0)
                velocity.y = velocity.y * 0.5f;
        }

        targetVelocity = move * maxSpeed;
    }

    private void UpdateKey()
    {
        leftMove = Input.GetKey("q");
        rightMove = Input.GetKey("d");
        

        Debug.Log(Input.GetKey(KeyCode.LeftShift));

        if (grounded)
        {
            isRunning = Input.GetKey(KeyCode.LeftShift);
            doubleJump = false;
        }
    }
}