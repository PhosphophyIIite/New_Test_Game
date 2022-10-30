using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public const float runSpeed = 1.5f;
    public const float walkingSpeed = 10f;

    public GameObject cube;

    public float moveSpeed = walkingSpeed;
    public float jumpForce = 1500f;
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

    //runs when game starts
    public void Start()
    {
        //get player's rigidbody and collider
        rb = GetComponent<Rigidbody>();
        cd = GetComponent<CapsuleCollider>();

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
        //  Debug.Log("1:   " + transform.GetLowestPoint<CapsuleCollider>());
        // Debug.Log("2:   " + cd.bounds.min.y);
        float halfHeight = cd.height / 2;
        float lowestPoint = transform.position.y - halfHeight;
        Debug.Log("3:   " + lowestPoint);
        Instantiate(cube, new Vector3(transform.position.x, lowestPoint, transform.position.z), Quaternion.identity);
    }

    //when move input get input and set movement on z axis from (w, s, forward, backward) (-1 / 0 / 1)
    public void OnMove(InputValue movementValue)
    {
        movementVector = movementValue.Get<Vector2>();
    }

    public void OnRun(InputValue movementValue)
    {
        //check if run butten is held (held = true, release = false)
        if (movementValue.isPressed)
        {
            moveSpeed = walkingSpeed * runSpeed;
        } else
        {
            moveSpeed = walkingSpeed;
        }
    }

    //when player presses space add force upwards
    public void OnJump()
    {
        //when grounded jump
        if (CheckGround())
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
        //send raycast below player with variables: start, direction, distance, hit return, layermask
        Ray groundRay = new Ray(rb.position, Vector3.down);
        bool grounded = Physics.Raycast(groundRay, cd.bounds.extents.y + 0.1f, ground);
        Debug.DrawRay(rb.position, Vector3.down * (cd.bounds.extents.y + 0.1f), Color.white, 1f);


        // Vector3 predictedLocationIn1Sec = calculateDownwardsMomentum();
        // Debug.Log(predictedLocationIn1Sec);

        //Debug.Log(rb.velocity.y);
        //Debug.Log(rb.velocity.y * 0.1f);
        //Debug.Log(cd.bounds.extents.y + 0.1f + (rb.velocity.y * 0.1f));

        bool closeToGround = Physics.Raycast(groundRay, (cd.bounds.extents.y + 0.1f), ground);

        return grounded;
    }

    private Vector3 calculateDownwardsMomentum()
    {


        return new Vector3(1f, 1f, 1f);
    }
}
