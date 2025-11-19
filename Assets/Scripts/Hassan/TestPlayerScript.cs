using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Entity
{
    [SerializeField]
    private Vector2 moveInput;

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();

        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        Move(move);

        // Handle jump input
        if (jump)
        {
            Jump();
            jump = false;
        }
    }

    public float GetHorizontalInput()
    {
        return moveInput.x;
    }

    public override void Move(Vector3 direction)
    {
        characterController.Move(direction * moveSpeed * Time.deltaTime);
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public override void Jump()
    {
        if (isGrounded)
        {
            velocity.y = jumpForce;
        }
    }

    private void OnJump()
    {
        jump = true;
    }
}
