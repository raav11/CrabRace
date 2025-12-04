using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using System.Collections.Generic;

public class CrabInputManager : MonoBehaviour
{
    public static CrabInputManager Instance { get; private set; }

    [Header("Settings")]
    [SerializeField]
    private GameObject driverPrefab;

    [Header("Crab Object")]
    [SerializeField]
    private List<GameObject> crabs = new List<GameObject>();
    private Body crabScript;

    private HashSet<InputDevice> pairedDevices = new HashSet<InputDevice>();
    private int nextPairIndex = 0;

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

    private void Update()
    {
        // Keyboard Join
        if (Keyboard.current != null && Keyboard.current.fKey.wasPressedThisFrame)
        {
            AttemptJoin(Keyboard.current);
        }

        // Gamepad Join
        foreach (var gp in Gamepad.all)
        {
            if (gp.startButton.wasPressedThisFrame)
            {
                AttemptJoin(gp);
            }
        }
    }

    private void AttemptJoin(InputDevice device)
    {
        // prevent the same controller from joining twice
        if (pairedDevices.Contains(device)) return;

        SpawnDriver(device);
    }

    private void SpawnDriver(InputDevice device)
    {
        // Global maximum players check
        if (GameManager.Instance.GetPlayers().Count >= GameManager.maxPlayers)
        {
            Debug.Log("Maximum players reached!");
            return;
        }

        // Find the first crab that has an available slot (less than 2 players)
        int chosenCrabIndex = -1;
        for (int i = 0; i < crabs.Count; i++)
        {
            if (crabs[i] == null) continue;
            var body = crabs[i].GetComponent<Body>();
            if (body == null) continue;
            if (body.players.Count < 2)
            {
                chosenCrabIndex = i;
                break;
            }
        }

        if (chosenCrabIndex == -1)
        {
            Debug.Log("All crabs are full! Cannot join more players.");
            return;
        }

        var chosenCrab = crabs[chosenCrabIndex];
        crabScript = chosenCrab.GetComponent<Body>();
        if (crabScript == null)
        {
            Debug.LogError("Chosen crab is missing Body component!");
            return;
        }

        // Instantiate PlayerInput paired with the device
        var playerInput = PlayerInput.Instantiate(
            driverPrefab,
            controlScheme: (device is Keyboard) ? "Keyboard" : "Gamepad",
            pairWithDevice: device
        );

        // Mark device as paired
        pairedDevices.Add(device);

        var driver = playerInput.GetComponent<PlayerMovement>();
        if (driver == null)
        {
            Debug.LogError("Driver prefab is missing PlayerMovement component!");
            Destroy(playerInput.gameObject);
            pairedDevices.Remove(device);
            return;
        }

        // Decide side based on how many players already on this crab
        bool assignLeft = crabScript.players.Count == 0;

        // Parent under the chosen crab so transforms track with it
        playerInput.transform.SetParent(chosenCrab.transform, false);

        if (assignLeft)
        {
            // Left Side
            driver.name = $"Driver Left (Crab {chosenCrabIndex})";

            var front = chosenCrab.transform.Find("Foot Front Left");
            var back = chosenCrab.transform.Find("Foot Back Left");

            if (front != null) driver.front = front;
            else Debug.LogWarning("Foot Front Left not found on chosen crab.");

            if (back != null) driver.back = back;
            else Debug.LogWarning("Foot Back Left not found on chosen crab.");

            crabScript.left = driver;
            crabScript.players.Add(driver);
        }
        else
        {
            // Right Side
            driver.name = $"Driver Right (Crab {chosenCrabIndex})";

            var front = chosenCrab.transform.Find("Foot Front Right");
            var back = chosenCrab.transform.Find("Foot Back Right");

            if (front != null) driver.front = front;
            else Debug.LogWarning("Foot Front Right not found on chosen crab.");

            if (back != null) driver.back = back;
            else Debug.LogWarning("Foot Back Right not found on chosen crab.");

            crabScript.right = driver;
            crabScript.players.Add(driver);
        }

        // Let the player movement know which body/crab they're attached to
        driver.body = chosenCrab;

        nextPairIndex++;

        Debug.Log($"Player joined! Controlling: {driver.name} on Crab {chosenCrabIndex} ({(assignLeft ? "Left" : "Right")}) with {device.displayName}");
    }
}
