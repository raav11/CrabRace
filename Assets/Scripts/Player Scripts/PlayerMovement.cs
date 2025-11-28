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

    private Material matFront;
    private Material matBack;

    [SerializeField] private Transform body;

    private bool punched;

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

        matFront = front.GetComponentInChildren<MeshRenderer>().material;
        matBack = back.GetComponentInChildren<MeshRenderer>().material;

        Debug.Log(matFront);
        Debug.Log(matBack);
    }
    private void FixedUpdate()
    {

        if (moving)
        {
            Movement();
        }

        if (movementDirectionFront.y <= 0 && movementDirectionFront.x <= 0)
        {
            rbFront.mass = 10f;
            matFront.DisableKeyword("_EMISSION");
        }

        if (movementDirectionBack.y <= 0 && movementDirectionBack.x <= 0)
        {
            rbBack.mass = 10f;
            matBack.DisableKeyword("_EMISSION");
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

        matFront.EnableKeyword("_EMISSION");

        rbFront.mass = 4f;
    }

    public void MoveDown(InputAction.CallbackContext context)
    {
        movementDirectionBack = context.ReadValue<Vector2>();

        moving = context.performed;

        matBack.EnableKeyword("_EMISSION");

        rbBack.mass = 4f;
    }



}


