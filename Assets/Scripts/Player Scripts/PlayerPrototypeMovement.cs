using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPrototypeMovement : MonoBehaviour
{

    private Player playerInput;
    [SerializeField] Vector2 movementDirection;
    private Vector3 targetRotation;
    [SerializeField] private float speed = 50f;
    private bool moving;
    [SerializeField] private bool left;
    [SerializeField] private bool right;

    [SerializeField] private bool leftA;
    [SerializeField] private bool rightD;

    private void OnEnable()
    {
        if (playerInput == null)
        {
            playerInput = new Player();
        }

        playerInput.Movement.Enable();

        if (left)
        {
            playerInput.Movement.Left.performed += Move;
            playerInput.Movement.Left.canceled += Move;

        }

        else if (right)
        {
            playerInput.Movement.Right.performed += Move;
            playerInput.Movement.Right.canceled += Move;
        }

        else if (leftA)
        {
            playerInput.Movement.LeftA.performed += Move;
            playerInput.Movement.LeftA.canceled += Move;

        }

        else if (rightD)
        {
            playerInput.Movement.RightD.performed += Move;
            playerInput.Movement.RightD.canceled += Move;
        }
    }

    private void OnDisable()
    {
        if (playerInput != null)
        {
            playerInput.Movement.Left.performed -= Move;
            playerInput.Movement.Left.canceled -= Move;

            playerInput.Movement.Right.performed -= Move;
            playerInput.Movement.Right.canceled -= Move;

            playerInput.Movement.LeftA.performed -= Move;
            playerInput.Movement.LeftA.canceled -= Move;

            playerInput.Movement.RightD.performed -= Move;
            playerInput.Movement.RightD.canceled -= Move;

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
        if (left)
        {
            targetRotation.y = movementDirection.y * speed;
        }

        else if (right)
        {
            targetRotation.y = movementDirection.y * -speed;
        }

        else if (leftA)
        {
            targetRotation.y = movementDirection.y * speed;
        }

        else if (rightD)
        {
            targetRotation.y = movementDirection.y * -speed;
        }

        transform.Rotate(targetRotation * Time.fixedDeltaTime);

    }

    public void Move(InputAction.CallbackContext context)
    {
        movementDirection = context.ReadValue<Vector2>();

        moving = context.performed;
    }

}


