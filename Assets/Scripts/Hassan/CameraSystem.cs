using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    [SerializeField]
    private CinemachineCamera cinemachineCamera;
    [SerializeField]
    private TestPlayerController targetPlayer;

    private void Start()
    {
        GameManager.Instance.OnLeaderChanged += SwitchTarget;
    }

    private void SwitchTarget(object sender, TestPlayerController e)
    {
        if (e != null && e != targetPlayer)
        {
            targetPlayer = e;
            cinemachineCamera.Follow = targetPlayer.transform;
            cinemachineCamera.LookAt = targetPlayer.transform;
        }
    }
}
