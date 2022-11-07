using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private readonly float walkingSpeed = 10f;
    public float WalkingSpeed{
        get
        {
            return walkingSpeed;
        }
    }

    private readonly float runSpeed = 1.5f;
    public float RunSpeed{
        get
        {
            return runSpeed;
        }
    }

    public float moveSpeed = 10f;
    public float jumpForce = 1500f;
    public float groundHitPredictTime = 0.1f;
    public LayerMask groundLayer;

    // Camera
    public float mouseSensitivity = 100f;
    public new Transform camera;

    // Camera
    private float cameraPitch = 0f;
    private float mouseX;
    private float mouseY;

    private Vector2 movementVector;
    public Rigidbody rb;
    private CapsuleCollider cd;

    // FSM
    public bool runningIsPressed;
    public bool jumpIsPressed;
    public MovementState initialMovementState;
    public MovementState currentMovementState;
    public ItemState initialItemState;
    public ItemState currentItemState;
    public bool grounded = false;
    public bool groundedPredict = false;

    // Movement states
    public MovementState m_InBetweenState;
    public MovementState m_DefaultMovementState;
    public MovementState m_JumpState;
    public MovementState m_RunState;
    public MovementState m_FallState;

    // Item States
    public ItemState m_DefaultItemState;
    public ItemState m_HoldState;

    // Extra condition for testing
    public bool testKey = false;
    public bool testKey2 = false;

    // Called when game starts
    public void Awake()
    {
        if(!rb){
            rb = GetComponent<Rigidbody>();
        }

        if(!cd){
            cd = GetComponent<CapsuleCollider>();
        }

        if(!m_InBetweenState){
            m_InBetweenState = gameObject.GetComponent<InBetweenState>();
        }
        
        if(!m_DefaultMovementState){
            m_DefaultMovementState = gameObject.GetComponent<DefaultMovementState>();
        }

        if(!m_JumpState){
            m_JumpState = gameObject.GetComponent<JumpState>();
        }

        if(!m_FallState){
            m_FallState = gameObject.GetComponent<FallState>();
        }

        if(!m_RunState){
            m_RunState = gameObject.GetComponent<RunState>();
        }

        if(!m_DefaultItemState){
            m_DefaultItemState = gameObject.GetComponent<DefaultItemState>();
        }

        if(!m_HoldState){
            m_HoldState = gameObject.GetComponent<HoldState>();
        }

        moveSpeed = WalkingSpeed;

        //lock mouse in the middle of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Called every physics update
    public void FixedUpdate()
    {
        //move player forwards
        Vector3 movement = new Vector3(movementVector.x, 0.0f, movementVector.y);
        movement = transform.TransformDirection(-movement * runSpeed);
        Vector3 direction = (transform.position - (transform.position + movement)).normalized;

        //move player forwards or backwards when a/s are pressed
        rb.MovePosition(transform.position + (direction * Time.deltaTime * moveSpeed));
    }

    // Called every frame
    public void Update()
    {
        CheckGround();
        Look();
    }

    //when move input get input and set movement on z axis from (w, s, forward, backward) (-1 / 0 / 1)
    public void OnMove(InputValue movementValue)
    {
        movementVector = movementValue.Get<Vector2>();
    }

    public void OnRun(InputValue movementValue)
    {
        runningIsPressed = movementValue.isPressed;
    }

    //when player presses space add force upwards
    public void OnJump(InputValue jumpValue)
    {
        if (jumpValue.isPressed){
            PredictGroundHitInTime();

            CheckGround();
        }

        jumpIsPressed = jumpValue.isPressed;
    }

    // Extra condition for testing FSM
    public void OnTestMinKey(InputValue testMinKey)
    {
        // Debug.Log("Test key is: " + testMinKey.isPressed);
        CheckGround();

        testKey = testMinKey.isPressed;
    }

    // Extra condition for testing FSM
    public void OnTestNumPlusKey(InputValue testPlusKey)
    {
        // Debug.Log("Test key is: " + testPlusKey.isPressed);
        CheckGround();

        testKey2 = testPlusKey.isPressed;
    }

    // Get look vector2 from mouse delta
    public void OnLook(InputValue value)
    {
        mouseX = value.Get<Vector2>().x * mouseSensitivity * Time.deltaTime;
        mouseY = value.Get<Vector2>().y * mouseSensitivity * Time.deltaTime;
    }

    // Sets camera to mouse movement
    private void Look()
    {
        //rotate camera on x-axis when lookin up and down (inverted because camera angles are inverterd compared mouse delta up and down
        cameraPitch -= mouseY;
        //set borders for looking up and down
        cameraPitch = Mathf.Clamp(cameraPitch, -90f, 90f);

        camera.localEulerAngles = Vector3.right * cameraPitch;

        //Rotate player on y axis when looking left / righ
        Vector2 mouseDelta = new Vector2(mouseX, mouseY);

        transform.Rotate(Vector2.up * mouseDelta.x);
    }

    // Send raycast towards the ground to check if grounded
    public void CheckGround()
    {
        // if it predicts you are grounded then no need for second raycast
        if(Physics.Raycast(rb.position, Vector3.down, cd.bounds.extents.y + 0.1f, groundLayer))
        {
            grounded = true;
        } else {
            grounded = false;
        }
    }

    // Send raycast towards the ground multiplied by velocity to make players be able to buffer a jump
    private void PredictGroundHitInTime()
    {
        Ray ray = new Ray(new Vector3(transform.position.x, cd.bounds.min.y, transform.position.z), Vector3.down);

        //check if player is actually going down (if velocity goes up: return)
        if(rb.velocity.y > 0){
            groundedPredict = false;
            return;
        }

        float distance = rb.velocity.y * groundHitPredictTime;

        groundedPredict = Physics.Raycast(ray, -distance, groundLayer);
        Debug.DrawRay(new Vector3(transform.position.x, cd.bounds.min.y, transform.position.z), Vector3.down * -distance, Color.red, 60f);
    }
}