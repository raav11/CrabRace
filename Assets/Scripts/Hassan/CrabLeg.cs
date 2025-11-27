using UnityEngine;

public class CrabLeg : MonoBehaviour
{
    public void Move(Vector2 input)
    {
        // Implement movement logic here
        transform.Translate(new Vector3(input.x, 0, input.y) * Time.deltaTime);
    }
}

[System.Serializable]
public struct LegPair
{
    public string pairName; // e.g. "Front Legs"
    public CrabLeg leftLeg;
    public CrabLeg rightLeg;
}