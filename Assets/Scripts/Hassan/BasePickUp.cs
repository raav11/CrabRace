using System;
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

    public abstract void PickUpEffect(GameObject player);

    protected void DestroyPickup()
    {
        // Pickup deregisters itself from the PickUpManager
        PickUpManager.Instance.DeregisterPickUp(this);
        Destroy(gameObject);
    }
}
