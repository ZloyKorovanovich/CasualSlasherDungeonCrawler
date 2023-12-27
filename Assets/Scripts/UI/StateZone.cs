using UnityEngine;
using UnityEngine.EventSystems;

public class StateZone : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private string _state = "IsAttack";

    private PlayerInputManager _manager;

    private void Start()
    {
        _manager = ServiceLocator.GetService<PlayerInputManager>();
        _manager.SetState(_state, false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _manager.SetState(_state, true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _manager.SetState(_state, false);
    }
}
