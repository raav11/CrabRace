using System.Collections;
using UnityEngine;

public class SeagullPickUp : BasePickUp
{
    [SerializeField] 
    private float moveDuration = 1.2f;
    [SerializeField] 
    private float arcHeight = 2f;
    [SerializeField] 
    private float forwardOffset = 1.5f;
    [SerializeField] 
    private float targetYOffset = 0.5f;

    public override void PickUpEffect(GameObject player)
    {
        TestPlayerController playerInTheLead = GameManager.Instance.GetPlayerInTheLead();
        if (playerInTheLead == null)
        {
            // Fallback: destroy pickup immediately if no leader
            DestroyPickup();
            return;
        }

        StartCoroutine(MovePlayerCoroutine(player, playerInTheLead.transform));
    }

    private IEnumerator MovePlayerCoroutine(GameObject player, Transform leaderTransform)
    {
        if (player == null || leaderTransform == null)
        {
            DestroyPickup();
            yield break;
        }

        Vector3 startPos = player.transform.position;
        Vector3 targetPos = leaderTransform.position + leaderTransform.forward * forwardOffset + Vector3.up * targetYOffset;

        // Try to disable player control and physics while being carried
        TestPlayerController controller = player.GetComponent<TestPlayerController>();
        if (controller != null)
        {
            controller.enabled = false;
        }

        Rigidbody rb = player.GetComponent<Rigidbody>();
        bool rbWasKinematic = false;
        if (rb != null)
        {
            rbWasKinematic = rb.isKinematic;
            rb.isKinematic = true;
        }

        float elapsed = 0f;
        Quaternion startRot = player.transform.rotation;

        while (elapsed < moveDuration)
        {
            float t = Mathf.Clamp01(elapsed / moveDuration);

            // Interpolate horizontally/linearly between start and target
            Vector3 basePos = Vector3.Lerp(startPos, targetPos, t);

            // Add vertical arc using a sine curve (smooth up and down)
            float arc = Mathf.Sin(t * Mathf.PI) * arcHeight;
            player.transform.position = basePos + Vector3.up * arc;

            // Smoothly rotate to face the leader (only on Y axis)
            Vector3 lookDir = leaderTransform.position - player.transform.position;
            lookDir.y = 0f;
            if (lookDir.sqrMagnitude > 0.001f)
            {
                Quaternion targetRot = Quaternion.LookRotation(lookDir.normalized, Vector3.up);
                player.transform.rotation = Quaternion.Slerp(startRot, targetRot, t);
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure final snap
        player.transform.position = targetPos;

        // Match y-rotation to leader (so the player faces the same direction as leader)
        Vector3 finalEuler = player.transform.rotation.eulerAngles;
        finalEuler.y = leaderTransform.rotation.eulerAngles.y;
        player.transform.rotation = Quaternion.Euler(finalEuler);

        // Re-enable controller and physics
        if (controller != null)
        {
            controller.enabled = true;
        }

        if (rb != null)
        {
            rb.isKinematic = rbWasKinematic;
        }

        // Finally destroy the pickup object
        DestroyPickup();
    }
}
