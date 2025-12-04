using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class CrabInputManager : MonoBehaviour
{
    public static CrabInputManager Instance { get; private set; }

    [Header("Settings")]
    [SerializeField] 
    private GameObject driverPrefab;

    [Header("Crab Object")]
    [SerializeField]
    private GameObject crab;

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
        var playerInput = PlayerInput.Instantiate(
            driverPrefab,
            controlScheme: (device is Keyboard) ? "Keyboard" : "Gamepad",
            pairWithDevice: device
        );
        playerInput.transform.SetParent(crab.transform, false);

        pairedDevices.Add(device);
        var driver = playerInput.GetComponent<PlayerMovement>();

        driver.body = crab;
        driver.front = crab.transform.Find("Foot Front Right").transform;
        driver.back = crab.transform.Find("Foot Back Left").transform;

        Debug.Log($"Player joined! Controlling: {driver.name} with {device.displayName}");
    }
}
