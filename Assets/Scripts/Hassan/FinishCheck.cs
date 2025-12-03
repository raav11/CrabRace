using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishCheck : MonoBehaviour
{
    public EventHandler<TestPlayerController> OnPlayerFinished;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            SceneManager.LoadScene("End");

            TestPlayerController player = other.GetComponent<TestPlayerController>();
            if (player != null)
            {
                OnPlayerFinished?.Invoke(this, player);
            }
        }
    }
}
