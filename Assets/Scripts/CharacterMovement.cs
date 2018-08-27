using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : PhysicsObject
{
    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;

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

        bool wasGoingLeft = false;

        float size = GetComponent<PlayerController>().GetWaterAmount() / 100f;
        if (leftMove && rightMove)
        {
            move.x = 0;
            GetComponent<Animator>().SetTrigger("Idle");
            transform.localScale = new Vector3(size, size, 1);
        }
        else if (rightMove)
        {
            move.x = 1;
            GetComponent<Animator>().SetTrigger("Walk");
            transform.localScale = new Vector3(size, size, 1);
            wasGoingLeft = false;
        }
        else if (leftMove)
        {
            move.x = -1;
            GetComponent<Animator>().SetTrigger("Walk");
            transform.localScale = new Vector3(-size, size, 1);
            wasGoingLeft = true;
        }
        else
        {
            if(!leftMove && !rightMove)
                GetComponent<Animator>().SetTrigger("Idle");

            if(wasGoingLeft)
             transform.localScale = new Vector3(-size, size, 1);
            else
             transform.localScale = new Vector3(size, size, 1);
        }

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

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject child = transform.Find("camFocus").gameObject;
        if (collision.tag == "beginTrigger")
        {
            if (transform.position.x > collision.transform.position.x)
            {
                child.SetActive(true);
                Camera.main.GetComponent<cameraScript>().isInLevel = true;
                Camera.main.GetComponent<cameraScript>().isInBeginning = false;
                // Camera.main.GetComponent<cameraScript>().Offset = Camera.main.transform.position - child.transform.position;
                Camera.main.GetComponent<cameraScript>().Target = child.transform;
            }
            else
            {
                child.SetActive(false);
                Camera.main.GetComponent<cameraScript>().isInLevel = false;
                Camera.main.GetComponent<cameraScript>().isInBeginning = true;
                // Camera.main.GetComponent<cameraScript>().Offset = new Vector3(0, 0, 0);
            }

        }
        if (collision.tag == "endTrigger")
        {
            if (transform.position.x >= collision.transform.position.x)
            {
                child.SetActive(false);
                Camera.main.GetComponent<cameraScript>().isInLevel = false;
                Camera.main.GetComponent<cameraScript>().isInEnd = true;
                // Camera.main.GetComponent<cameraScript>().Offset = new Vector3(0, 0, 0);
            }
            else
            {
                child.SetActive(true);
                Camera.main.GetComponent<cameraScript>().isInLevel = true;
                Camera.main.GetComponent<cameraScript>().isInEnd = false;
                //Camera.main.GetComponent<cameraScript>().Offset = Camera.main.transform.position - child.transform.position;
                Camera.main.GetComponent<cameraScript>().Target = child.transform;
            }

        }
    }
}