using Unity.VisualScripting;
using UnityEngine;

public class SeagullPickUp : BasePickUp
{
    protected override void PickUpEffect(GameObject player)
    {
        GameObject playerInTheLead = GameManager.Instance.GetPlayerInTheLead();

        MovePlayer(player, playerInTheLead.transform);
    }

    private void MovePlayer(GameObject player, Transform endPoint)
    {
        // Implement moving the player with the seagull

    }
}
