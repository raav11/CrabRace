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

    [SerializeField]
    private TestPlayerController playerInTheLead;
    [SerializeField]
    private GameObject finishLine;
    private bool raceFinishedTeam1 = false;
    private bool raceFinishedTeam2 = false;

    [SerializeField]
    private float timerTeam1;
    [SerializeField]
    private float timerTeam2;

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

    private void Start()
    {
        FinishCheck finishCheck = finishLine.GetComponent<FinishCheck>();

        // Fix: Subscribe the method as an event handler, not call it directly
        finishCheck.OnPlayerFinished += PlayerFinished;
    }

    private void Update()
    {
        foreach (var player in players)
        {
            player.distanceToFinish = Vector3.Distance(player.transform.position, finishLine.transform.position);
        }

        playerInTheLead = players.OrderBy(p => p.distanceToFinish).FirstOrDefault();
        if (playerInTheLead != null)
        {
            OnLeaderChanged?.Invoke(this, playerInTheLead);
        }

        if (!raceFinishedTeam1)
        {
            timerTeam1 += Time.deltaTime;
        }
        if (!raceFinishedTeam2)
        {
            timerTeam2 += Time.deltaTime;
        }
    }

    private void PlayerFinished(object sender, TestPlayerController e)
    {
        if (e.team == TestPlayerController.Team.Team1 && !raceFinishedTeam1)
        {
            raceFinishedTeam1 = true;
        }
        else if (e.team == TestPlayerController.Team.Team2 && !raceFinishedTeam2)
        {
            raceFinishedTeam2 = true;
        }
    }

    public void RegisterPlayer(TestPlayerController player)
    {
        // Avoid duplicate registration
        if (players.Contains(player))
        {   
            return;
        }

        // Add player to the list
        players.Add(player);

        // Determine team based on registration order:
        int index = players.Count - 1;
        if (index < 2)
        {
            player.team = TestPlayerController.Team.Team1;
        }
        else
        {
            player.team = TestPlayerController.Team.Team2;
        }
        //InputManager.Instance.OnPlayerRegistered?.Invoke(this, EventArgs.Empty);
    }

    public void DeRegisterPlayer(TestPlayerController player)
    {
        // Implementation for deregistering the player
        if (players.Contains(player))
        {
            players.Remove(player);
        }
    }

    public List<TestPlayerController> GetPlayers()
    {
        return players;
    }

    public TestPlayerController GetPlayerInTheLead()
    {
        return playerInTheLead;
    }

    public TestPlayerController GetPlayerInTheBack()
    {
        return players.OrderByDescending(p => p.distanceToFinish).FirstOrDefault();
    }

    // Example method to demonstrate interaction with AudioManager
    private void PlayExampleSound()
    {
        AudioManager.Instance.Play("ExampleSound");
    }
}
