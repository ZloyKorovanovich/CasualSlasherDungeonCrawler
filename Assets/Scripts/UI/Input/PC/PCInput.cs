using UnityEngine;

public class PCInput : MonoBehaviour
{
    private ControlManager _manager;

    private void Start()
    {
        _manager = ServiceLocator.GetService<ControlManager>();
    }

    private void Update()
    {
        _manager.SetAxis("Vertical", Input.GetAxis("Vertical"));
        _manager.SetAxis("Horizontal", Input.GetAxis("Horizontal"));
        _manager.SetState("IsAttack", Input.GetMouseButton(0));

        if (Input.GetKeyDown(KeyCode.Escape))
            _manager.InverseState("IsPaused");
    }
}
