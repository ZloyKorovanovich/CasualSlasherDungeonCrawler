using UnityEngine;
using UnityEngine.EventSystems;

public class RotationZone : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    public CharacterPreview preview;
    public float sensetivity = 10f;

    private Vector3 _lastPos;
    private float _lastAngle;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _lastPos = eventData.position;
        _lastAngle = preview.targetAngle;
    }

    public void OnDrag(PointerEventData eventData)
    {
        var currentPos = eventData.position;
        preview.targetAngle = _lastAngle + (_lastPos.x - currentPos.x) * sensetivity;
    }
}