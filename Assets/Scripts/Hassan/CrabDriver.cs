using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class CrabDriver : MonoBehaviour
{
    private CrabLeg _leftLeg;
    private CrabLeg _rightLeg;

    // The Manager calls this immediately after spawning this object
    public void AssignLegs(CrabLeg left, CrabLeg right)
    {
        _leftLeg = left;
        _rightLeg = right;
    }

    // Input Action Callback (hook this up in PlayerInput events)
    public void OnMoveLeftLeg(InputValue value)
    {
        if (_leftLeg != null) _leftLeg.Move(value.Get<Vector2>());
    }

    public void OnMoveRightLeg(InputValue value)
    {
        if (_rightLeg != null) _rightLeg.Move(value.Get<Vector2>());
    }
}