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
    public LayerMask ground;

    //camera
    public float mouseSensitivity = 100f;
    public new Transform camera;

    //camera
    private float cameraPitch = 0f;
    private float mouseX;
    private float mouseY;

    private Vector2 movementVector;
    private Rigidbody rb;
    private CapsuleCollider cd;


    //Testing with FSM
    public bool runningIsPressed;
    public State initialState;

    //runs when game starts
    public void Start()
    {
        //get player's rigidbody and collider
        rb = GetComponent<Rigidbody>();
        cd = GetComponent<CapsuleCollider>();

        moveSpeed = WalkingSpeed;

        //lock mouse in the middle of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    //fixedupdate is called once per frame
    public void FixedUpdate()
    {
        //move player forwards
        Vector3 movement = new Vector3(movementVector.x, 0.0f, movementVector.y);
        movement = transform.TransformDirection(-movement * runSpeed);
        Vector3 direction = (transform.position - (transform.position + movement)).normalized;

        //move player forwards or backwards when a/s are pressed
        rb.MovePosition(transform.position + (direction * Time.deltaTime * moveSpeed));
    }

    public void Update()
    {
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
    public void OnJump()
    {
        bool grounded = CheckGround();

        //when grounded jump
        if (grounded)
        {
            rb.AddForce(0.0f, jumpForce, 0.0f);
        }
    }

    //get look vector2 from mouse delta
    public void OnLook(InputValue value)
    {
        mouseX = value.Get<Vector2>().x * mouseSensitivity * Time.deltaTime;
        mouseY = value.Get<Vector2>().y * mouseSensitivity * Time.deltaTime;
    }

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

    //send raycast towards the ground to check if grounded
    public bool CheckGround()
    {
        bool grounded = false;

        // if it predicts you are grounded then no need for second raycast
        if(predictGroundHitInTime() || Physics.Raycast(rb.position, Vector3.down, cd.bounds.extents.y + 0.1f, ground))
        {
            grounded = true;
        }
       
        //Debug.DrawRay(rb.position, Vector3.down * (cd.bounds.extents.y + 0.1f), Color.white, 1f);
        return grounded;
    }

    private bool predictGroundHitInTime()
    {
        Ray ray = new Ray(cd.bounds.min, Vector3.down);
        float distance = rb.velocity.y * groundHitPredictTime;

        // Debug.DrawRay(cd.bounds.min, Vector3.down * -distance, Color.red, 1f);
        return Physics.Raycast(ray, -distance, ground);
    }
}
