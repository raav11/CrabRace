using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public EventHandler<Body> OnLeaderChanged;

    [SerializeField]
    private List<PlayerMovement> players = new List<PlayerMovement>();
    public const int maxPlayers = 4;
    [SerializeField]
    private List<Body> crabs = new List<Body>();

    [SerializeField]
    private Body crabInTheLead;
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
        finishCheck.OnPlayerFinished += CrabFinished;
    }

    private void Update()
    {
        foreach (var crab in crabs)
        {
            crab.distanceToFinish = Vector3.Distance(crab.transform.position, finishLine.transform.position);
        }

        crabInTheLead = crabs.OrderBy(p => p.distanceToFinish).FirstOrDefault();
        if (crabInTheLead != null)
        {
            OnLeaderChanged?.Invoke(this, crabInTheLead);
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

    private void CrabFinished(object sender, Body e)
    {
        if (e.team == Body.Team.Team1 && !raceFinishedTeam1)
        {
            raceFinishedTeam1 = true;
        }
        else if (e.team == Body.Team.Team2 && !raceFinishedTeam2)
        {
            raceFinishedTeam2 = true;
        }
    }

    public void RegisterPlayer(PlayerMovement player)
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
            player.team = PlayerMovement.Team.Team1;
        }
        else
        {
            player.team = PlayerMovement.Team.Team2;
        }
    }

    public void DeRegisterPlayer(PlayerMovement player)
    {
        // Implementation for deregistering the player
        if (players.Contains(player))
        {
            players.Remove(player);
        }
    }

    public List<PlayerMovement> GetPlayers()
    {
        return players;
    }

    public Body GetPlayerInTheLead()
    {
        return crabInTheLead;
    }

    public Body GetPlayerInTheBack()
    {
        return crabs.OrderByDescending(p => p.distanceToFinish).FirstOrDefault();
    }

    // Example method to demonstrate interaction with AudioManager
    private void PlayExampleSound()
    {
        AudioManager.Instance.Play("ExampleSound");
    }
}
