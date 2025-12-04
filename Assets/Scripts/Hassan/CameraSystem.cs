using Unity.Cinemachine;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    [SerializeField]
    private CinemachineCamera cinemachineCamera;
    [SerializeField]
    private CinemachineFollow cinemachineFollow;
    [SerializeField]
    private Body targetCrab;

    private void Start()
    {
        GameManager.Instance.OnLeaderChanged += SwitchTarget;
    }

    private void SwitchTarget(object sender, Body e)
    {
        if (e != null && e != targetCrab)
        {
            targetCrab = e;
            cinemachineCamera.Follow = targetCrab.transform;
        }
    }
}
