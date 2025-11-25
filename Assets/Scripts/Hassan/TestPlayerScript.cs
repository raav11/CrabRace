using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestPlayerController : Entity
{
    [SerializeField]
    private Vector2 moveInput;

    public float distanceToFinish;

    [SerializeField]
    private List<BasePickUp> collectedPickUps = new List<BasePickUp>();
    private const int maxPickUps = 3;

    public override void Start()
    {
        base.Start();
        GameManager.Instance.RegisterPlayer(this);
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
    
    private void OnPowerUPSlot1()
    {
        collectedPickUps[0]?.PickUpEffect(this.gameObject);
    }

    private void OnPowerUPSlot2()
    {
        collectedPickUps[1]?.PickUpEffect(this.gameObject);
    }

    private void OnPowerUPSlot3()
    {
        collectedPickUps[2]?.PickUpEffect(this.gameObject);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("PickUp"))
        {
            if (collectedPickUps.Count >= maxPickUps)
            {
                Debug.Log("Cannot collect more pick-ups. Inventory full.");
                return;
            }
            BasePickUp pickUp = hit.gameObject.GetComponent<BasePickUp>();
            collectedPickUps.Add(pickUp);
            pickUp.PickUpEffect(this.gameObject);
        }
    }
}
