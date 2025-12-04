using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerMovement;

public class Body : MonoBehaviour
{
    public enum Team
    {
        Team1,
        Team2
    }


    public PlayerMovement left;
    public PlayerMovement right;

    public WaitForSeconds wait = new WaitForSeconds(3.5f);

    private WaitForSeconds punch = new WaitForSeconds(0.5f);

    private IEnumerator coroutineReposition;

    public float distanceToFinish;

    [SerializeField] private BoxCollider punchHitBox;

    public Team team;

    public List<PlayerMovement> players = new List<PlayerMovement>(maxPlayers);
    private const int maxPlayers = 2;


    void Update()
    {

        CheckIfFallen();

        if (left != null && right != null)
        {
            if (left.punched && right.punched)
            {
                StartCoroutine(Punch());
            }
        }
    }

    private void CheckIfFallen()
    {
        if (transform.rotation.eulerAngles.z >= 60 && transform.rotation.eulerAngles.z <= 200 || transform.rotation.eulerAngles.z <= 300 && transform.rotation.eulerAngles.z >= 200)
        {

            if (coroutineReposition == null)
            {
                coroutineReposition = (Resposition());

                StartCoroutine(coroutineReposition);

            }

        }

        else
        {
            if (coroutineReposition != null)
            {
                StopCoroutine(coroutineReposition);

                coroutineReposition = null;
            }
        }
    }

    private IEnumerator Resposition()
    {

        yield return wait;

        transform.localRotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        transform.position = new Vector3(transform.position.x, 9f, transform.position.z);

        coroutineReposition = null;

        wait = new WaitForSeconds(3.5f);

    }

    private IEnumerator Punch()
    {
        left.punched = false;
        right.punched = false;

        left.punchCooldown = 10f;
        right.punchCooldown = 10f;

        //enable a hitbox
        punchHitBox.enabled = true;
        yield return punch;


        //disable it
        punchHitBox.enabled = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();

            rb.AddForce(transform.right * -35000);

            Body script = other.GetComponent<Body>();
            script.wait = new WaitForSeconds(15f);
        }
    }
}
