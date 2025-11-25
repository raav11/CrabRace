using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "PickupSO", menuName = "Scriptable Objects/ new PickupSO")]
public class PickupSO : ScriptableObject
{
    public Transform pickupPrefab;
    public Image pickupIcon;
    public string pickupName;
    public int inventoryID;
}