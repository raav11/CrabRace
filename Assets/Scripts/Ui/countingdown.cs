using System.Collections;
using UnityEngine;

public class countingdown : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(CountdownCoroutine());
    }

    public IEnumerator CountdownCoroutine()
    {
        for (int i = 3; i > 0; i--)
        {
            Debug.Log(i);
            yield return new WaitForSeconds(1f);
        }
        Debug.Log("Go");
    }
}
