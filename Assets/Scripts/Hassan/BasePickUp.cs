using UnityEngine;

public abstract class BasePickUp : MonoBehaviour
{
    [SerializeField]
    public PickupSO pickupSO;

    protected GameObject player;

    protected virtual void Awake()
    {
        // Pickup registers itself with the PickUpManager
        PickUpManager.Instance.RegisterPickUp(this);
    }

    protected abstract void PickUpEffect(GameObject player);

    protected void DestroyPickup()
    {
        // Pickup deregisters itself from the PickUpManager
        PickUpManager.Instance.DeregisterPickUp(this);
        Destroy(gameObject);
    }

    protected void OnDestroy()
    {
        // Ensure deregistration in case DestroyPickup wasn't called
        PickUpManager.Instance.DeregisterPickUp(this);
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            PickUpEffect(player);
            DestroyPickup();
        }
    }
}
