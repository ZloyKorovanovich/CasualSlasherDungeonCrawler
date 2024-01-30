using UnityEngine;

public class CharacterMover : CharacterComponent
{
    private const float _GRAVITY_SPEED = 9f;

    public float sensetivity = 7.0f;
    public float luft = 60.0f;

    private CharacterController _controller;
    private Animator _animator;
    private bool _isRotating;

    private void Awake()
    {
        Init();
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Displace(_characterMain.InputAxis);
        Rotate(_characterMain.Target, _characterMain.InputAxis, Time.deltaTime);
    }

    private void OnAnimatorIK(int layerIndex)
    {
        SetLook(_characterMain.Target);
    }

    protected override void OnDeath()
    {
        base.OnDeath();
        Destroy(_controller);
    }

    private void Displace(Vector3 inputAxis)
    {
        _controller.Move(Vector3.down * _GRAVITY_SPEED * Time.deltaTime);
        var direction = transform.InverseTransformDirection(inputAxis);
        _animator.SetFloat("vert", direction.z);
        _animator.SetFloat("hor", direction.x);
    }

    private void Rotate(Vector3 target, Vector3 inputAxis, float deltaTime)
    {
        bool rotating = false;
        if (Mathf.Abs(inputAxis.x) < Mathf.Epsilon && Mathf.Abs(inputAxis.z) < Mathf.Epsilon)
            rotating = true;

        Vector3 oldRotation = transform.eulerAngles;
        transform.LookAt(target);

        float angle = Mathf.DeltaAngle(transform.eulerAngles.y, oldRotation.y);
        _animator.SetFloat("rot", -Mathf.Sign(angle), 1.0f / sensetivity, Time.deltaTime);
        angle = Mathf.Abs(angle);
        deltaTime *= sensetivity;

        if (!rotating)
            oldRotation.y = Mathf.LerpAngle(oldRotation.y, transform.eulerAngles.y, deltaTime);
        else if (angle > luft)
            _isRotating = true;

        transform.eulerAngles = oldRotation;
        if (!_isRotating)
            return;

        if(angle * Mathf.Deg2Rad <= deltaTime)
        {
            _isRotating = false;
            rotating = false;
        }
        _animator.SetBool("isRot", rotating);
    }

    private void SetLook(Vector3 target)
    {
        _animator.SetLookAtWeight(1f, 0.7f, 0.9f, 1f, 1f);
        _animator.SetLookAtPosition(target);
    }
}
