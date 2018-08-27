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

    // Use this for initialization
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        leftMove = Input.GetKey("q");
        rightMove = Input.GetKey("d");

        if (leftMove && rightMove)
            move.x = 0;
        else if (rightMove)
            move.x = 1;
        else if (leftMove)
            move.x = -1;

        if (grounded)
            doubleJump = false;

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
}