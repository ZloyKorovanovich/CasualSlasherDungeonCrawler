using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(CharacterMain))]
[RequireComponent(typeof(CharacterAnimation))]
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

    private Vector3 _inputAxis;
    private Vector3 _target;

    private bool _isRotating;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _animation = GetComponent<CharacterAnimation>();
        _main = GetComponent<CharacterMain>();
    }

    private void Start()
    {
        _main.OnSetInputs += UpdateInputs;
    }

    private void OnAnimatorIK(int layerInde)
    {
        Displace(_inputAxis);
        Rotate(_target, Time.deltaTime);
        SetLook(_target);
    }

    public void UpdateInputs()
    {
        _inputAxis = _main.InputAxis;
        _target = _main.Target;
    }

    private void Displace(Vector3 inputAxis)
    {
        _controller.Move(Vector3.down * _GRAVITY_SPEED);
        _animation.Move(transform.InverseTransformDirection(_inputAxis));
    }

    private void Rotate(Vector3 target, float deltaTime)
    {
        bool rotating = false;
        if (Mathf.Abs(_inputAxis.x) < Mathf.Epsilon && Mathf.Abs(_inputAxis.z) < Mathf.Epsilon)
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
