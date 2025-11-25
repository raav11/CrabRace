using System;
using UnityEngine;

public class FinishCheck : MonoBehaviour
{
    public EventHandler<TestPlayerController> OnPlayerFinished;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TestPlayerController player = other.GetComponent<TestPlayerController>();
            if (player != null)
            {
                OnPlayerFinished?.Invoke(this, player);
            }
        }
    }
}
