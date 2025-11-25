using Unity.VisualScripting;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    // This is a placeholder class for the testPlayer will not be in the final version

    public enum EntityType
    {
        Player,
        Enemy,
        NPC,
        Item
    }

    public EntityType entityType;

    public float moveSpeed;
    public float jumpForce;
    public Vector3 velocity;
    public const float gravity = -9.81f;
    public bool isGrounded = true;
    public bool jump = false;

    public StateMachine stateMachine;
    public CharacterController characterController;

    public virtual void Start()
    {
        stateMachine = new StateMachine();
        characterController = GetComponent<CharacterController>();
    }


    public virtual void Update()
    {
        // Apply gravity
        velocity.y += gravity * Time.deltaTime;

        // Apply motion
        characterController.Move(velocity * Time.deltaTime);

        // Now update grounded status
        isGrounded = characterController.isGrounded;

        // If grounded, reset downward velocity
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }


    public abstract void Move(Vector3 direction);

    public virtual void Jump() { }
}
