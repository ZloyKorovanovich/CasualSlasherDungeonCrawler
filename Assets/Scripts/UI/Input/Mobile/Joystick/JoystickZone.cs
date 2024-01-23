using UnityEngine;

public class JoystickZone : MonoBehaviour
{
    public float radius;

    public Vector3 DefaultPos => transform.position;

    public Vector2 ClampPositionInZone(Vector2 inputPos)
    {
        var pos = transform.InverseTransformPoint(inputPos);
        return transform.TransformPoint(Vector2.ClampMagnitude(pos, radius));
    }
}
