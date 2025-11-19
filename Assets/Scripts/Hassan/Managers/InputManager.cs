using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public EventHandler OnPlayerRegistered;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple InputManager instances found!");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
       OnPlayerRegistered += HandlePlayerRegistered;
    }

    private void HandlePlayerRegistered(object sender, EventArgs e)
    {
        // Assign input controls to the newly registered player
    }
}
