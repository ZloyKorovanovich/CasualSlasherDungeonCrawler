using UnityEngine;

public class AnimationHandler
{
    private const string _ANIMATOR_VERTICAL = "Vertical";
    private const string _ANIMATOR_HORIZONTAL = "Horizontal";
    private const string _ANIMATOR_ROTATION = "Rotation";
    private const string _ANIMATOR_IS_ROTATING = "IsRotating";

    private Animator _animator;

    public AnimationHandler(Animator animator)
    {
        _animator = animator;
    }

    public void Move(Vector3 inputAxis)
    {
        _animator.SetFloat(_ANIMATOR_VERTICAL, inputAxis.z);
        _animator.SetFloat(_ANIMATOR_HORIZONTAL, inputAxis.x);
    }

    public void Rotate(float angle, float deltaTime, float sensetivity)
    {
        _animator.SetFloat(_ANIMATOR_ROTATION, -Mathf.Sign(angle), sensetivity, deltaTime);
    }

    public void SetRotation(bool isRotating)
    {
        _animator.SetBool(_ANIMATOR_IS_ROTATING, isRotating);
    }

    public void SetLook(Vector3 target)
    {
        _animator.SetLookAtWeight(1f, 0.7f, 0.9f, 1f, 1f); //some magic numbers
        _animator.SetLookAtPosition(target);
    }
}
