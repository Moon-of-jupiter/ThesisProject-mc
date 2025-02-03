using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Animator animator;
    [SerializeField] ShootProjectile shooter;
    [SerializeField] float acceleration;
    [SerializeField] float xdrag;
    [SerializeField] float jumpStrength;
    [SerializeField] float fallStrength;
    [SerializeField] float jumpGroundThreshhold;
    [SerializeField] int extraJumps;
    [SerializeField] float sideJumpDirectionFraction;
    

    public UnityEvent Event_Jump = new UnityEvent();

    public RaycastHit playerStepHit;

    private bool isJumping;

    private int jumpCounter;

    private Rigidbody rb;
    public float leftrRightInput;

    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnMove(InputValue input)
    {
        if (input != null)
        {
            leftrRightInput = input.Get<Vector2>().x;
        }

        animator.SetBool("isRunning", true);
        
    }
    private void OnMoveStop(InputValue input)
    {
        leftrRightInput = 0;
        animator.SetBool("isRunning", false);
    }

    private void OnJump(InputValue input)
    {
        

        if (isGrounded)
        {
            jumpCounter = extraJumps;
            Jump(leftrRightInput, 1, sideJumpDirectionFraction);
            isJumping = true;
        }
        else if(jumpCounter > 0)
        {
            Jump(leftrRightInput, 1, sideJumpDirectionFraction);
            isJumping = true;
            jumpCounter--;
        }



    }

    private void OnAttack(InputValue input)
    {
        shooter.Hitscan(new Projectile() { range = 10, damage = 5, hitColor = new Color(255,0,100) });

    }

    private void OnJumpStop(InputValue input)
    {
        isJumping = false;
        if(rb.linearVelocity.y > 0)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f, rb.linearVelocity.z);

        }
    }

    private void Move()
    {
        if(leftrRightInput != 0 && isGrounded)
        {
            
            rb.AddForce(new Vector3(acceleration * leftrRightInput, 0, 0), ForceMode.Force);
            rb.AddForce(new Vector3(-rb.linearVelocity.x * xdrag,0,0));
        }
        
    }

    private void Jump(float xJump, float yJump, float sideFraction)
    {
        float clampedXVelocity = rb.linearVelocity.x;
        if (xJump != 0)
        {
            if(math.abs(xJump) * math.abs(clampedXVelocity) / clampedXVelocity != 1)
            {
                clampedXVelocity = 0;
            }
            
        }

        Vector3 direction;

        if (xJump != 0)
        {
            float dX = sideFraction * xJump;
            float dY = (1 - sideFraction) * yJump;

            direction = new Vector2 (dX, dY);
        }
        else
        {
            direction = new Vector2(xJump, yJump);
        }

        

        Debug.Log(clampedXVelocity);

        Vector3 directionN = direction.normalized;
        rb.linearVelocity = new Vector3(clampedXVelocity, 0, rb.linearVelocity.z);
        rb.AddForce(new Vector3(directionN.x, directionN.y,0) * jumpStrength, ForceMode.VelocityChange);

        Event_Jump.Invoke();
    }

    private void HandleJumpFall()
    {
        
        if( rb.linearVelocity.y < 0)
        {
            isJumping = false;
            rb.AddRelativeForce(new Vector3(0, -9.82f, 0) * fallStrength);
        }
    }

    private void CheckGround()
    {
        bool hitGround = Physics.Raycast(new Ray(transform.position, new Vector3(0, -1, 0)), out playerStepHit, jumpGroundThreshhold);

        if(hitGround && !isGrounded)
        {
            Event_Jump.Invoke();
        }

        isGrounded = hitGround;
    }

    // Update is called once per frame
    void Update()
    {
        CheckGround();
        Move();
        HandleJumpFall();
    }
}


