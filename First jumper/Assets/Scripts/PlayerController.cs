using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public LayerMask ground;
    
    //camera
    public float mouseSensitivity = 100f;
    public float mouseX;
    public float mouseY;
    public Transform camera;

    //camera
    private float cameraPitch = 0f;

    private float movementX;
    private float movementY;
    private bool grounded;
    private RaycastHit groundHit;
    private Rigidbody rb;

    //runs when game starts
    public void Start()
    {
        //get player's rigidbody and collider
        rb = GetComponent<Rigidbody>();

        //lock mouse in the middle of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    //fixedupdate is called once per frame
    public void FixedUpdate()
    {
        //move player forwards
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        movement = transform.TransformDirection(-movement);
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
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    //when player presses space add force upwards
    public void OnJump()
    {
        //check if grounded
        CheckGround();

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
    public void CheckGround()
    {
        //send raycast below player with variables: start, direction, distance, hit return, layermask
        Ray groundRay = new Ray(rb.position, Vector3.down);
        grounded = Physics.Raycast(groundRay, out groundHit, 2f, ground);

        Debug.DrawRay(rb.position, Vector3.down, Color.white, 2f);
    }
}
