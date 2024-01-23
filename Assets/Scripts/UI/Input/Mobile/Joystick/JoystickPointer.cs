using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickPointer : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public bool isDragging;

    private JoystickZone _zone;

    public Vector2 UnscaledDelta => transform.position - _zone.DefaultPos;
    public Vector2 Delta => Vector2.Distance(transform.position, _zone.DefaultPos)
        / _zone.radius * Vector3.Normalize(UnscaledDelta);

    public void Init(JoystickZone zone)
    {
        _zone = zone;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = _zone.ClampPositionInZone(eventData.position);
        isDragging = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = _zone.DefaultPos;
        isDragging = false;
    }
}