using Unity.VisualScripting;
using UnityEngine;

public class SeagullPickUp : BasePickUp
{
    public override void PickUpEffect(GameObject player)
    {
        TestPlayerController playerInTheLead = GameManager.Instance.GetPlayerInTheLead();

        MovePlayer(player, playerInTheLead.transform);

        DestroyPickup();
    }

    private void MovePlayer(GameObject player, Transform endPoint)
    {
        // Implement moving the player with the seagull
        player.transform.position = Vector3.MoveTowards(player.transform.position, endPoint.position, 10f * Time.deltaTime);
    }
}
