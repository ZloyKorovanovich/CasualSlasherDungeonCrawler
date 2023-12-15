using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(CharacterMain))]
[RequireComponent(typeof(CharacterController))]
public class CharacterMover : MonoBehaviour
{
    private const float _GRAVITY_SPEED = 0.5f;

    [SerializeField]
    private float _sensetivity = 7.0f;
    [SerializeField]
    private float _luft = 60.0f;

    private CharacterController _controller;
    private CharacterAnimation _animation;
    private CharacterMain _main;

    private bool _isRotating;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _animation = GetComponent<CharacterAnimation>();
        _main = GetComponent<CharacterMain>();
    }

    private void OnAnimatorIK(int layerInde)
    {
        Displace(_main.InputAxis);
        Rotate(_main.Target, _main.InputAxis, Time.deltaTime);
        SetLook(_main.Target);
    }

    private void Displace(Vector3 inputAxis)
    {
        _controller.Move(Vector3.down * _GRAVITY_SPEED);
        _animation.Move(transform.InverseTransformDirection(inputAxis));
    }

    private void Rotate(Vector3 target, Vector3 inputAxis, float deltaTime)
    {
        bool rotating = false;
        if (Mathf.Abs(inputAxis.x) < Mathf.Epsilon && Mathf.Abs(inputAxis.z) < Mathf.Epsilon)
            rotating = true;

        Vector3 oldRotation = transform.eulerAngles;
        transform.LookAt(target);

        float angle = Mathf.DeltaAngle(transform.eulerAngles.y, oldRotation.y);
        _animation.Rotate(angle, Time.deltaTime, 1.0f / _sensetivity);
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

        _animation.SetRotation(rotating);
    }

    private void SetLook(Vector3 target)
    {
        _animation.SetLook(target);
    }
}
