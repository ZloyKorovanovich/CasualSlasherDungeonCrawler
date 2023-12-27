using UnityEngine;

public class JoystickZone : MonoBehaviour
{
    [SerializeField]
    private float _radius;
    public Vector3 DefaultPos => transform.position;
    public float Radius => _radius;

    public Vector2 ClampPositionInZone(Vector2 inputPos)
    {
        var pos = transform.InverseTransformPoint(inputPos);
        return transform.TransformPoint(Vector2.ClampMagnitude(pos, _radius));
    }
}
