using UnityEngine;

public class BodyScript : MonoBehaviour
{

    private float timer;

    private void Update()
    {

    }
    private void Resposition()
    {

        gameObject.transform.localRotation = Quaternion.Euler(0, gameObject.transform.rotation.eulerAngles.y, 0);
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, 9f, gameObject.transform.position.z);

        //if (rightD || leftA)
        //{
        //    gameObject.transform.rotation = Quaternion.Euler(-35.97f, 180, 0);
        //}
        //else if (right || left)
        //{
        //    gameObject.transform.rotation = Quaternion.Euler(-35.97f, 0, 0);
        //}

        timer = 0;

    }
}
