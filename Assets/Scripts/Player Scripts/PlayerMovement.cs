using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    private Player playerInput;

    private Vector2 movementDirectionFront;
    private Vector2 movementDirectionBack;

    [SerializeField] private float speed = 250f;
    private bool moving;
    [SerializeField] Transform front;
    [SerializeField] Transform back;

    private Rigidbody rbFront;
    private Rigidbody rbBack;

    [SerializeField] private Transform body;

    private float timer;

    private void OnEnable()
    {
        if (playerInput == null)
        {
            playerInput = new Player();
        }

        playerInput.Movement.Enable();

        playerInput.Movement.Left.performed += MoveUp;
        playerInput.Movement.Left.canceled += MoveUp;

        playerInput.Movement.Right.performed += MoveDown;
        playerInput.Movement.Right.canceled += MoveDown;

    }

    private void OnDisable()
    {
        if (playerInput != null)
        {
            playerInput.Movement.Left.performed -= MoveUp;
            playerInput.Movement.Left.canceled -= MoveUp;

            playerInput.Movement.Right.performed -= MoveDown;
            playerInput.Movement.Right.canceled -= MoveDown;

            playerInput.Movement.Disable();
        }
    }


    private void Start()
    {

        if (transform.rotation.eulerAngles.y == 270)
        {
            speed = -speed;
        }

        rbFront = front.GetComponent<Rigidbody>();
        rbBack = back.GetComponent<Rigidbody>();

    }
    private void FixedUpdate()
    {

        if (moving)
        {
            Movement();
        }

        if (movementDirectionFront.y <= 0 && movementDirectionFront.x <= 0)
        {
            Debug.Log("No Input Front");
            rbFront.mass = 10f;
        }

        if (movementDirectionBack.y <= 0 && movementDirectionBack.x <= 0)
        {
            Debug.Log("No Input Back");
            rbBack.mass = 10f;
        }

    }

    private void Update()
    {
        if (body.rotation.eulerAngles.z >= 60 && body.rotation.eulerAngles.z <= 200 || body.rotation.eulerAngles.z <= 300 && body.rotation.eulerAngles.z >= 200)
        {
            timer += Time.deltaTime;

            if (timer >= 3.5f)
            {
                Resposition();
            }
        }

        else
        {
            timer = 0;
        }

    }

    private void Movement()
    {
        Vector3 targetRotationFront = new Vector3();
        Vector3 targetRotationBack = new Vector3();

        targetRotationFront.y = movementDirectionFront.y * speed;
        targetRotationBack.y = movementDirectionBack.y * speed;

        front.Rotate(targetRotationFront * Time.fixedDeltaTime);
        back.Rotate(targetRotationBack * Time.fixedDeltaTime);

    }

    public void MoveUp(InputAction.CallbackContext context)
    {
        movementDirectionFront = context.ReadValue<Vector2>();

        moving = context.performed;

        rbFront.mass = 4f;
    }

    public void MoveDown(InputAction.CallbackContext context)
    {
        movementDirectionBack = context.ReadValue<Vector2>();

        moving = context.performed;

        rbBack.mass = 4f;
    }

    private void Resposition()
    {

        body.localRotation = Quaternion.Euler(0, body.rotation.eulerAngles.y, 0);
        body.position = new Vector3(body.position.x, 9f, body.position.z);

        timer = 0;

    }

}


