using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    private Player playerInput;

    private Vector2 movementDirectionFront;
    private Vector2 movementDirectionBack;

    [SerializeField] private float speed = 250f;
    private bool moving;
    public Transform front;
    public Transform back;

    private Rigidbody rbFront;
    private Rigidbody rbBack;

    private Material matFront;
    private Material matBack;

    public GameObject body;

    public bool punched = false;
    public float punchCooldown;

    private WaitForSeconds punchtime = new WaitForSeconds(0.5f);

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

        playerInput.Movement.Punch.performed += Punch;

    }

    private void OnDisable()
    {
        if (playerInput != null)
        {
            playerInput.Movement.Left.performed -= MoveUp;
            playerInput.Movement.Left.canceled -= MoveUp;

            playerInput.Movement.Right.performed -= MoveDown;
            playerInput.Movement.Right.canceled -= MoveDown;

            playerInput.Movement.Punch.performed -= Punch;

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

    private void Update()
    {
        if (punchCooldown > 0)
        {
            punchCooldown -= Time.deltaTime;
        }
    }
    private void FixedUpdate()
    {

        if (moving)
        {
            Movement();
        }


        //Is the player not moving dont make the legs glow.
        if (movementDirectionFront.y <= 0 && movementDirectionFront.x <= 0)
        {
            rbFront.mass = 10f;
            matFront.DisableKeyword("_Emission");
        }

        if (movementDirectionBack.y <= 0 && movementDirectionBack.x <= 0)
        {
            rbBack.mass = 10f;
            matBack.DisableKeyword("_Emission");
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

        matFront.EnableKeyword("_Emission");

        rbFront.mass = 4f;
    }

    public void MoveDown(InputAction.CallbackContext context)
    {
        movementDirectionBack = context.ReadValue<Vector2>();

        moving = context.performed;

        matBack.EnableKeyword("_Emission");

        rbBack.mass = 4f;
    }

    public void Punch(InputAction.CallbackContext context)
    {
        if (punchCooldown <= 0)
        {
            StartCoroutine(PunchCoroutine());

            punchCooldown = 1.5f;
        }
    }

    private IEnumerator PunchCoroutine()
    {
        punched = true;

        yield return punchtime;

        punched = false;
    }

}


