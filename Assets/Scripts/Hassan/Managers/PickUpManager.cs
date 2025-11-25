using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PickUpManager : MonoBehaviour
{
    public static PickUpManager Instance { get; private set; }

    [SerializeField]
    private List<BasePickUp> pickUps = new List<BasePickUp>();

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple PickUpManager instances found!");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void RegisterPickUp(BasePickUp pickUp)
    {
        if (!pickUps.Contains(pickUp))
        {
            pickUps.Add(pickUp);
        }
    }

    public void DeregisterPickUp(BasePickUp pickUp)
    {
        if (pickUps.Contains(pickUp))
        {
            pickUps.Remove(pickUp);
        }
    }

    public List<BasePickUp> GetAllPickUps()
    {
        return new List<BasePickUp>(pickUps);
    }
}
