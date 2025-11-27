using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class CrabInputManager : MonoBehaviour
{
    public static CrabInputManager Instance { get; private set; }

    [Header("Settings")]
    [SerializeField] private GameObject driverPrefab;

    [Header("The Crab Configuration")]
    [SerializeField] 
    private LegPair[] legPairs;

    private HashSet<InputDevice> pairedDevices = new HashSet<InputDevice>();
    private int nextPairIndex = 0;

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    private void Update()
    {
        if (nextPairIndex >= legPairs.Length) return;

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
        var playerInput = PlayerInput.Instantiate(
            driverPrefab,
            controlScheme: (device is Keyboard) ? "Keyboard" : "Gamepad",
            pairWithDevice: device
        );

        pairedDevices.Add(device);

        LegPair targetLegs = legPairs[nextPairIndex];
        nextPairIndex++;

        var driver = playerInput.GetComponent<CrabDriver>();
        if (driver != null)
        {
            driver.AssignLegs(targetLegs.leftLeg, targetLegs.rightLeg);
            driver.name = $"Player_{nextPairIndex}_({targetLegs.pairName})";
        }

        Debug.Log($"Player joined! Controlling: {targetLegs.pairName} using {device.displayName}");
    }
}
