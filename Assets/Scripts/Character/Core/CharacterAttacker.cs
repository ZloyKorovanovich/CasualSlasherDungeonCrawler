using UnityEngine;

[RequireComponent(typeof(CharacterMain))]
public class CharacterAttacker : CharacterComponent
{
    [SerializeField]
    private Transform _rightHand;

    private WeaponInHand _currentWeapon;
    private Animator _animator;
    private CharacterHealth _health;

    public WeaponInHand CurrentWeapon => _currentWeapon;

    private void Awake()
    {
        _characterMain = GetComponent<CharacterMain>();
        _animator = GetComponent<Animator>();
        _health = GetComponent<CharacterHealth>();

        _characterMain.OnDeath += Dispose;
    }

    private void Start()
    {
        _characterMain.OnSetInputs += UpdateInputs;
    }

    private void Dispose()
    {
        _currentWeapon?.Drop();
        Destroy(this);
    }

    private void UpdateInputs()
    {
        _animator.SetBool("IsAttack", _characterMain.IsAttack && _currentWeapon);
    }

    public void MakeAttack()
    {
        _currentWeapon.Attack(transform.position + Vector3.up * 1.5f, transform.forward, _health);
    }

    public void SetWeapon(GameObject weapon)
    {
        _currentWeapon?.Drop();
        _currentWeapon = Instantiate(weapon, _rightHand).GetComponent<WeaponInHand>();
        if(_currentWeapon)
            _animator.SetFloat("AttackSpeed", _currentWeapon.AttackSpeed);
    }
}
