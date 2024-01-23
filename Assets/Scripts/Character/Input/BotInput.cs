using UnityEngine;

public class BotInput : CharacterInput
{
    public float agressiveDistance = 5f;
    public float attackDistance = 1.3f;
    public float RetreatBound = 3f;
    public float retreatDistance = 1f;
    public float delta = 3f;

    private Transform _target;
    private Vector3 _inputAxis;
    private float _targetDirection;
    private bool _isAttack;

    private void OnValidate()
    {
        if(RetreatBound < retreatDistance)
            retreatDistance = RetreatBound;
    }

    private void Awake()
    {
        Init();
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        _target.GetComponent<CharacterMain>().onDeath += OnPlayerDead;
    }

    private void OnPlayerDead()
    {
        _characterMain.SetInputs(Vector3.zero, transform.forward + Vector3.up * 1.5f, false);
        _target.GetComponent<CharacterMain>().onDeath -= OnPlayerDead;
        Destroy(this);
        return;
    }

    private void OnDisable()
    {
        if(_target)
            _target.GetComponent<CharacterMain>().onDeath -= OnPlayerDead;
    }

    private void Update()
    {
        if(!_target)
            OnPlayerDead();

        //Rewrite
        var dir = _target.transform.position - transform.position;
        var sqrDist = Vector3.SqrMagnitude(dir);

        _isAttack = false;
        if (sqrDist <= agressiveDistance * agressiveDistance)
        {
            if(sqrDist < RetreatBound * RetreatBound)
            {
                if (sqrDist < retreatDistance * retreatDistance)
                    _targetDirection = -1f;

                if(sqrDist <= attackDistance * attackDistance)
                    _isAttack = true;
            }
            else
                _targetDirection = 1f;
        }
        else
            _targetDirection = 0f;

        _inputAxis = Vector3.Lerp(_inputAxis, dir.normalized * _targetDirection, Time.deltaTime * delta);
        _characterMain.SetInputs(_inputAxis, _target.transform.position + Vector3.up * 1.5f, _isAttack);
    }
}
