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
        if (playerController == null)
            Debug.Log("NOOOONNNNN");
    }

    public bool IsWalking()
    {
        return walking;
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;
        float size = playerController.GetWaterAmount() / PlayerController.MAX_WATER;

        leftMove = Input.GetAxisRaw("Horizontal") < 0; //Input.GetKey("q");
        rightMove = Input.GetAxisRaw("Horizontal") > 0; //Input.GetKey("d");

        if (leftMove && rightMove)
        {
            move.x = 0;
            if (!idling)
            {
                if (!jumping && !playerController.IsSpawningCloud() && !playerController.IsHit())
                   container.GetComponent<Animator>().SetTrigger("Idle");
                idling = true;
            }
        }
        else if (rightMove)
        {
            move.x = 1;
            if(!jumping && !walking && !playerController.IsSpawningCloud() && !playerController.IsHit())
                container.GetComponent<Animator>().SetTrigger("Walk");

            transform.localScale = new Vector3(size, size, 1);

            idling = false;
            walking = true;
        }
        else if (leftMove)
        {
            move.x = -1;
            if (!jumping && !walking && !playerController.IsSpawningCloud() && !playerController.IsHit())
                container.GetComponent<Animator>().SetTrigger("Walk");

            transform.localScale = new Vector3(-size, size, 1);

            idling = false;
            walking = true;
        }
        else
        {
            if (!idling)
            {
                if (!jumping && !playerController.IsSpawningCloud() && !playerController.IsHit())
                    container.GetComponent<Animator>().SetTrigger("Idle");
                idling = true;
                walking = false;
            }
        }

        if (grounded)
            doubleJump = false;

        if (!playerController.IsHit())
        {
            if (Input.GetButtonDown("Jump") && grounded)
            {
                container.GetComponent<Animator>().SetTrigger("DirectJump");
                velocity.y = jumpTakeOffSpeed;
                jumping = true;
                falling = false;
                walking = false;
            }
            else if (Input.GetButtonDown("Jump") && !doubleJump)
            {
                velocity.y = jumpTakeOffSpeed * 1.5f;
                doubleJump = true;
                container.GetComponent<Animator>().SetTrigger("DirectJump");
                jumping = true;
                falling = false;
                walking = false;
            }
            else if (Input.GetButtonUp("Jump"))
            {
                if (velocity.y > 0)
                    velocity.y = velocity.y * 0.5f;
            }
        }

        targetVelocity = move * maxSpeed;
    }
    
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        
        if (!falling && jumping && !playerController.IsHit())
        {
            if (prevPos.y > transform.position.y)
            {
                container.GetComponent<Animator>().SetTrigger("Fall");
                falling = true;
            }
        }

        if ((jumping || falling) && grounded && !playerController.IsHit())
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
                //Camera.main.GetComponent<cameraScript>().Target = child.transform;
            }
            else
            {
                child.SetActive(false);
                Camera.main.GetComponent<cameraScript>().isInLevel = false;
                Camera.main.GetComponent<cameraScript>().isInBeginning = true;
            }

        }
        if (collision.tag == "endTrigger")
        {
            if (transform.position.x >= collision.transform.position.x)
            {
                child.SetActive(false);
                Camera.main.GetComponent<cameraScript>().isInLevel = false;
                Camera.main.GetComponent<cameraScript>().isInEnd = true;
            }
            else
            {
                child.SetActive(true);
                Camera.main.GetComponent<cameraScript>().isInLevel = true;
                Camera.main.GetComponent<cameraScript>().isInEnd = false;
                //Camera.main.GetComponent<cameraScript>().Target = child.transform;
            }

        }
    }
}