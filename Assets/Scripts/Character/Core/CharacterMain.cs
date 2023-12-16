using System;
using UnityEngine;

[DisallowMultipleComponent]
public class CharacterMain : MonoBehaviour
{
    public Action OnSetInputs;
    public Action OnDeath;
    public Action OnHit;

    private bool _isAttack;
    private bool _isPickUp;
    private Vector3 _inputAxis;
    private Vector3 _target;

    public bool IsAttack => _isAttack;
    public bool IsPickUp => _isPickUp;
    public Vector3 InputAxis => _inputAxis;
    public Vector3 Target => _target;

    public void SetInputs(Vector3 inputAxis, Vector3 target, bool interacting)
    {
        _inputAxis = inputAxis;
        _target = target;
        _isAttack = interacting;
        _isPickUp = interacting;

        OnSetInputs?.Invoke();
    }

    public void Die()
    {
        OnDeath?.Invoke();
        Destroy(this);
    }

    public void Hit()
    {
        OnHit?.Invoke();
    }
}
