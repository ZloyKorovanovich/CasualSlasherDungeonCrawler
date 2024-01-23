using System;
using UnityEngine;

[DisallowMultipleComponent]
public class CharacterMain : MonoBehaviour
{
    public Action onSetInputs;
    public Action onDeath;

    private bool _isAttack;
    private bool _isPickUp;
    private Vector3 _inputAxis;
    private Vector3 _target;

    public bool IsAttack => _isAttack;
    public Vector3 InputAxis => _inputAxis;
    public Vector3 Target => _target;

    public void SetInputs(Vector3 inputAxis, Vector3 target, bool isAttack)
    {
        _inputAxis = inputAxis;
        _target = target;
        _isAttack = isAttack;

        onSetInputs?.Invoke();
    }

    public void Die()
    {
        onDeath?.Invoke();
        Destroy(this);
    }
}
