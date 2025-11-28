using System.Collections;
using UnityEngine;

public class Body : MonoBehaviour
{

    [SerializeField] private PlayerMovement left;
    [SerializeField] private PlayerMovement right;

    private WaitForSeconds wait = new WaitForSeconds(3.5f);

    private IEnumerator coroutine;



    void Update()
    {

        CheckIfFallen();

        if (left.punched && right.punched)
        {
            Punch();
        }

    }

    private void CheckIfFallen()
    {
        if (transform.rotation.eulerAngles.z >= 60 && transform.rotation.eulerAngles.z <= 200 || transform.rotation.eulerAngles.z <= 300 && transform.rotation.eulerAngles.z >= 200)
        {

            if (coroutine == null)
            {
                coroutine = (Resposition());

                StartCoroutine(coroutine);

            }

        }

        else
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);

                coroutine = null;
            }
        }
    }

    private IEnumerator Resposition()
    {

        yield return wait;

        transform.localRotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        transform.position = new Vector3(transform.position.x, 9f, transform.position.z);

        coroutine = null;

    }

    private void Punch()
    {

        left.punched = false;
        right.punched = false;

        left.punchCooldown = 10f;
        right.punchCooldown = 10f;

        Debug.Log("Punched!");

    }
}
