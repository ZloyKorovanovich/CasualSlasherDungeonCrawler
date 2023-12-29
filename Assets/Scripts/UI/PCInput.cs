using UnityEngine;

public class PCInput : MonoBehaviour
{
    private PlayerInputManager _manager;

    private void Start()
    {
        _manager = ServiceLocator.GetService<PlayerInputManager>();
        _manager.SetAxis("Vertical", 0.0f);
        _manager.SetAxis("Horizontal", 0.0f);
        _manager.SetState("IsAttack", false);
    }

    private void Update()
    {
        _manager.SetAxis("Vertical", Input.GetAxis("Vertical"));
        _manager.SetAxis("Horizontal", Input.GetAxis("Horizontal"));

        _manager.SetState("IsAttack", Input.GetMouseButton(0));
    }
}
