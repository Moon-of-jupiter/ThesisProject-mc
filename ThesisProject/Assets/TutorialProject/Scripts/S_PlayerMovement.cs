using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class S_PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    [SerializeField] float PlayerSpeed;
    [SerializeField] float PlayerMaxSpeed;

    [SerializeField] float JumpThreshold;
    [SerializeField] float JumpForce;
    
    [SerializeField] float InputLerpValue;
    [SerializeField] int  AirJumpsMax;


    protected Vector3 MovementInput;
    protected Vector3 normalizedInput;
    protected Vector3 oldMovementImput;

    protected Vector3 movementVector;

    protected float jolt;
    
    protected Rigidbody rb;

    protected Collider rbColider;

    
    protected int airJumpCounter;


    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rbColider = GetComponent<Collider>();

        
    }

    void OnMove(InputValue input)
    {
        MovementInput = new Vector3( input.Get<Vector2>().x,0, input.Get<Vector2>().y);
        normalizedInput = MovementInput.normalized;
    }

    private void OnMoveStop(InputValue input)
    {
        movementVector = Vector3.zero;
        MovementInput = Vector3.zero;
        normalizedInput = Vector3.zero;
    }

    private void OnJump(InputValue input)
    {
        

        if (Physics.Raycast(new Ray(transform.position,new Vector3(0,-1,0)), JumpThreshold) )
        {
            //if(normalizedInput.x > 0)
            //{
            //    rb.AddRelativeForce(new Vector3(JumpForce, JumpForce, 0).normalized * JumpForce * 100, ForceMode.Acceleration);

            //}
            //else
            //{
            rb.AddRelativeForce(new Vector3(0, JumpForce, 0), ForceMode.Acceleration);
            //}

            
            airJumpCounter = AirJumpsMax;
            
            
        }
        else if (airJumpCounter > 0)
        {
            rb.AddRelativeForce(new Vector3(0, JumpForce, 0), ForceMode.Acceleration);

            airJumpCounter--;
        }
        

    }



    protected void UpdateMovement()
    {
        
        movementVector = normalizedInput * PlayerSpeed * Time.fixedDeltaTime;





        Vector2 v = new Vector2(rb.linearVelocity.x, rb.linearVelocity.z);

        float speed = Vector2.Distance(Vector2.zero, v);

        float maxSpeedFraction = speed / PlayerMaxSpeed;
        
        if(maxSpeedFraction < 1)
        {
            ApplyMovement();
        }
    }

    protected void ApplyMovement()
    {
        

        rb.AddRelativeForce(movementVector, ForceMode.Acceleration);
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
        
    }

    
    

}
