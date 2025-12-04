using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishCheck : MonoBehaviour
{
    public EventHandler<Body> OnPlayerFinished;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Body player = other.GetComponent<Body>();
            if (player != null)
            {
                OnPlayerFinished?.Invoke(this, player);
            }

            SceneManager.LoadScene("End");
        }
    }
}
