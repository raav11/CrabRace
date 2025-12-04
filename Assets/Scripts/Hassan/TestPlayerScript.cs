using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TestPlayerController : Entity
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
        characterController.Move(moveSpeed * Time.deltaTime * direction);
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
