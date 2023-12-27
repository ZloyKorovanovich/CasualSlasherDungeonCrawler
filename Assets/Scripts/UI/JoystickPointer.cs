using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickPointer : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private JoystickZone _zone;
    private bool _isDragging;

    public Vector2 UnscaledDelta => transform.position - _zone.DefaultPos;
    public Vector2 Delta => Vector2.Distance(transform.position, _zone.DefaultPos)
        / _zone.Radius * Vector3.Normalize(UnscaledDelta);
    public bool IsDragging => _isDragging;

    public void Init(JoystickZone zone)
    {
        _zone = zone;
    }

    public void OnDrag(PointerEventData eventData)
    {
        StopAllCoroutines();
        transform.position = _zone.ClampPositionInZone(eventData.position);
        _isDragging = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = _zone.DefaultPos;
        _isDragging = false;
    }
}