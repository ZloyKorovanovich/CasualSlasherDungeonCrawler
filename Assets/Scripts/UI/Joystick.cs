using UnityEngine;

public class Joystick : MonoBehaviour
{
    [SerializeField]
    private string _verticalAxis = "Vertical";
    [SerializeField]
    private string _horizontalAxis = "Horizontal";
    [SerializeField]
    private string _joystickState = "IsJoystick";

    private JoystickPointer _pointer;
    private JoystickZone _zone;

    private PlayerInputManager _manager;

    private void Awake()
    {
        _pointer = GetComponentInChildren<JoystickPointer>();
        _zone = GetComponentInChildren<JoystickZone>();

        _pointer.Init(_zone);
    }

    private void Start()
    {
        _manager = ServiceLocator.GetService<PlayerInputManager>();

        _manager.SetAxis(_verticalAxis, 0.0f);
        _manager.SetAxis(_horizontalAxis, 0.0f);
        _manager.SetState(_joystickState, _pointer.IsDragging);
    }

    private void Update()
    {
        var delta = _pointer.Delta;

        _manager.SetAxis(_verticalAxis, delta.y);
        _manager.SetAxis(_horizontalAxis, delta.x);
        _manager.SetState(_joystickState, _pointer.IsDragging);
    }
}
