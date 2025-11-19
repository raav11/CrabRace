using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    private List<GameObject> players = new List<GameObject>();

    public GameObject playerInTheLead;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple GameManager instances found!");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void RegisterPlayer(GameObject player)
    {
        // Implementation for registering the player
        players.Add(player);
        Debug.Log("Player registered: " + player.name);
        InputManager.Instance.OnPlayerRegistered?.Invoke(this, EventArgs.Empty);
    }
    
    private void DeRegisterPlayer(GameObject player)
    {
        // Implementation for deregistering the player
        Debug.Log("Player deregistered: " + player.name);
    }

    public List<GameObject> GetPlayers()
    {
        return players;
    }

    public GameObject GetPlayerInTheLead()
    {
        return playerInTheLead;
    }

    // Example method to demonstrate interaction with AudioManager
    private void PlayExampleSound()
    {
        AudioManager.Instance.Play("ExampleSound");
    }
}
