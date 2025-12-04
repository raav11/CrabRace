using System.Collections;
using UnityEngine;

public class Body : MonoBehaviour
{

    [SerializeField] private PlayerMovement left;
    [SerializeField] private PlayerMovement right;

    public WaitForSeconds wait = new WaitForSeconds(3.5f);

    private WaitForSeconds punch = new WaitForSeconds(0.5f);

    private IEnumerator coroutineReposition;

    [SerializeField] private BoxCollider punchHitBox;


    void Update()
    {

        CheckIfFallen();

        if (left.punched && right.punched)
        {
            StartCoroutine(Punch());
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
