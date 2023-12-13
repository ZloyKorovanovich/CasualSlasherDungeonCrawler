using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
public class CharacterMover : MonoBehaviour
{
    private const float _GRAVITY_SPEED = 0.5f;

    [SerializeField]
    private float _sensetivity;
    [SerializeField]
    private float _luft;

    private CharacterController _controller;
    private AnimationHandler _animationHandler;

    private Vector3 _inputAxis;
    private Vector3 _target;

    private bool _isRotating;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();

        var animator = GetComponent<Animator>();
        _animationHandler = new AnimationHandler(animator);
    }

    private void OnAnimatorIK(int layerInde)
    {
    }

    public void Displace(InputAction.CallbackContext context)
    {
        var x = context.ReadValue<Vector2>().x;
        var y = context.ReadValue<Vector2>().y;
        var moveDirection = new Vector3(x, 0, y).normalized;
        _animationHandler.Move(transform.InverseTransformDirection(moveDirection));
        //_controller.Move(Vector3.down * _GRAVITY_SPEED);
    }

    private void SetLook(Vector3 target)
    {
        _animationHandler.SetLook(target);
    }

    // This one rotates player, don't touch this pice of shit, that actually works.
    private void Rotate(Vector3 target, float deltaTime)
    {
        bool rotating = false;
        if (Mathf.Abs(_inputAxis.x) < Mathf.Epsilon && Mathf.Abs(_inputAxis.z) < Mathf.Epsilon)
            rotating = true;

        Vector3 oldRotation = transform.eulerAngles;
        transform.LookAt(target);

        float angle = Mathf.DeltaAngle(transform.eulerAngles.y, oldRotation.y);
        _animationHandler.Rotate(angle, Time.deltaTime, 1.0f / _sensetivity);
        angle = Mathf.Abs(angle);
        deltaTime *= _sensetivity;

        if (!rotating)
            oldRotation.y = Mathf.LerpAngle(oldRotation.y, transform.eulerAngles.y, deltaTime);
        else if (angle > _luft)
            _isRotating = true;

        transform.eulerAngles = oldRotation;

        if (!_isRotating)
            return;

        if(angle * Mathf.Deg2Rad <= deltaTime)
        {
            _isRotating = false;
            rotating = false;
        }

        _animationHandler.SetRotation(rotating);
    }
}
