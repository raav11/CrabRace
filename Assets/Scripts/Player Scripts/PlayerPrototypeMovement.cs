using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPrototypeMovement : MonoBehaviour
{

    private Player playerInput;
    [SerializeField] Vector2 movementDirection;
    private Vector3 targetRotation;
    [SerializeField] private float speed = 50f;
    private bool moving;
    public bool A;
    public bool D;

    private void OnEnable()
    {
        if (playerInput == null)
        {
            playerInput = new Player();
        }

        playerInput.Movement.Enable();

        if (A)
        {
            playerInput.Movement.MoveLeft.performed += Move;
            playerInput.Movement.MoveLeft.canceled += Move;

        }

        else if (D)
        {
            playerInput.Movement.MoveRight.performed += Move;
            playerInput.Movement.MoveRight.canceled += Move;
        }
    }

    private void OnDisable()
    {
        if (playerInput != null)
        {
            playerInput.Movement.MoveLeft.performed -= Move;
            playerInput.Movement.MoveLeft.canceled -= Move;

            playerInput.Movement.MoveRight.performed -= Move;
            playerInput.Movement.MoveRight.canceled -= Move;

            playerInput.Movement.Disable();
        }
    }

    private void FixedUpdate()
    {

        if (moving)
        {
            Movement();
        }

    }

    private void Movement()
    {
        targetRotation.y = movementDirection.x * speed;

        transform.Rotate(targetRotation * Time.fixedDeltaTime);

    }

    public void Move(InputAction.CallbackContext context)
    {
        movementDirection = context.ReadValue<Vector2>();

        moving = context.performed;
    }

}


