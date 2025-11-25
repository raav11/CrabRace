using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public EventHandler<TestPlayerController> OnLeaderChanged;

    [SerializeField]
    private List<TestPlayerController> players = new List<TestPlayerController>();

    private TestPlayerController playerInTheLead;
    [SerializeField]
    private Transform finishLine;

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

    private void Update()
    {
        foreach (var player in players)
        {
            player.distanceToFinish = Vector3.Distance(player.transform.position, finishLine.position);
        }

        playerInTheLead = players.OrderBy(p => p.distanceToFinish).FirstOrDefault();
        if (playerInTheLead != null)
        {
            OnLeaderChanged?.Invoke(this, playerInTheLead);
        }
    }

    public void RegisterPlayer(TestPlayerController player)
    {
        // Implementation for registering the player
        players.Add(player);
        Debug.Log("Player registered: " + player.name);
        //InputManager.Instance.OnPlayerRegistered?.Invoke(this, EventArgs.Empty);
    }
    
    public void DeRegisterPlayer(GameObject player)
    {
        // Implementation for deregistering the player
        Debug.Log("Player deregistered: " + player.name);
    }

    public List<TestPlayerController> GetPlayers()
    {
        return players;
    }

    public TestPlayerController GetPlayerInTheLead()
    {
        return playerInTheLead;
    }

    // Example method to demonstrate interaction with AudioManager
    private void PlayExampleSound()
    {
        AudioManager.Instance.Play("ExampleSound");
    }
}
