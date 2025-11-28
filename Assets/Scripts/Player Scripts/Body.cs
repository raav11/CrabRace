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

        if (transform.rotation.eulerAngles.z >= 60 && transform.rotation.eulerAngles.z <= 200 || transform.rotation.eulerAngles.z <= 300 && transform.rotation.eulerAngles.z >= 200)
        {

            if (coroutine == null)
            {
                coroutine = (Resposition());

                StartCoroutine(coroutine);

                Debug.Log("Start");
            }

        }

        else
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);

                Debug.Log("Stop");

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

        Debug.Log("Repositioned");

    }

    private void Punch()
    {

    }
}
