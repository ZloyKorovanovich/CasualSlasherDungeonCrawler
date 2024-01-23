using UnityEngine;

public class CharacterAttacker : CharacterComponent
{
    public RuntimeAnimatorController defaultAnimator;

    private WeaponInHand _currentWeapon;
    private Animator _animator;
    private CharacterHealth _health;
    private Transform _rightHand;

    private void Awake()
    {
        Init();
        _animator = GetComponent<Animator>();
        _health = GetComponent<CharacterHealth>();

        _rightHand = _animator.GetBoneTransform(HumanBodyBones.RightHand);
        _animator.runtimeAnimatorController = defaultAnimator;
    }

    private void Start()
    {
        _characterMain.onSetInputs += UpdateInputs;
    }

    protected override void OnDeath()
    {
        _currentWeapon?.Drop();
        base.OnDeath();
    }

    private void UpdateInputs()
    {
        _animator.SetBool("isAttack", _characterMain.IsAttack && _currentWeapon);
    }

    public void MakeAttack()
    {
        _currentWeapon.Attack(transform.position + Vector3.up, transform.forward, _health);
    }

    public void SetWeapon(GameObject weapon)
    {
        _currentWeapon?.Drop();
        _currentWeapon = Instantiate(weapon, _rightHand).GetComponent<WeaponInHand>();

        if (_currentWeapon)
        {
            if(_currentWeapon.animatorController != _animator.runtimeAnimatorController)
                _animator.runtimeAnimatorController = _currentWeapon.animatorController;

            _animator.SetFloat("speed", _currentWeapon.speed);
        }
        else
        {
            _animator.runtimeAnimatorController = defaultAnimator;
            _animator.SetFloat("speed", 1.0f);
        }
    }
}
