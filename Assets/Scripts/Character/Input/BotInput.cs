using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(CharacterMain))]
public class BotInput : CharacterInput
{
    [SerializeField]
    private float _agressiveDistance = 5.0f;
    [SerializeField]
    private float _attackDistance = 1.3f;

    private Transform _target;

    private void Awake()
    {
        _characterMain = GetComponent<CharacterMain>();
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        _characterMain.OnDeath += CharacterDeath;
    }

    private void CharacterDeath()
    {
        Destroy(this);
    }

    private void Update()
    {
        if(!_target)
        {
            _characterMain.SetInputs(Vector3.zero, _target.position + Vector3.up * 1.5f, false);
            Destroy(this);
            return;
        }

        var dist = Vector3.Distance(transform.position, _target.position);
        if (dist < _agressiveDistance)
        {
            if (dist < _attackDistance)
                _characterMain.SetInputs(Vector3.zero, _target.position + Vector3.up * 1.5f, true);
            else
                _characterMain.SetInputs(Vector3.Normalize(_target.position - transform.position), _target.position + Vector3.up * 1.5f, false);
        }
        else
            _characterMain.SetInputs(Vector3.zero, _target.position + Vector3.up * 1.5f, false);
    }
}
