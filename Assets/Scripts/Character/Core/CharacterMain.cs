using System;
using UnityEngine;

[DisallowMultipleComponent]
public class CharacterMain : MonoBehaviour
{
    public Action OnSetInputs;

    private bool _isAttack;
    private Vector3 _inputAxis;
    private Vector3 _target;

    public bool IsAttack => _isAttack;
    public Vector3 InputAxis => _inputAxis;
    public Vector3 Target => _target;

    public void SetInputs(Vector3 inputAxis, bool isAttack, Vector3 target)
    {
        _inputAxis = inputAxis;
        _isAttack = isAttack;
        _target = target;

        OnSetInputs?.Invoke();
    }
}
