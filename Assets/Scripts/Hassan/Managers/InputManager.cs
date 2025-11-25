using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{

    public EventHandler OnPlayerRegistered;

    private void Start()
    {
       OnPlayerRegistered += HandlePlayerRegistered;
    }

    private void HandlePlayerRegistered(object sender, EventArgs e)
    {
        // Assign input controls to the newly registered player
    }
}
