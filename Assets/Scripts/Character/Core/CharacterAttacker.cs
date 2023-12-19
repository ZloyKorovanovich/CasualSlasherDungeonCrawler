using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterMain))]
public class CharacterAttacker : CharacterComponent
{
    [SerializeField]
    private Transform _rightHand;

    private WeaponInHand _currentWeapon;
    private CharacterAnimation _animation;
    private CharacterHealth _health;

    private bool _isAttacking;

    public WeaponInHand CurrentWeapon => _currentWeapon;

    private void Awake()
    {
        _characterMain = GetComponent<CharacterMain>();
        _animation = GetComponent<CharacterAnimation>();
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
        if (_isAttacking)
            return;

        if (_characterMain.IsAttack && _currentWeapon)
        {
            _isAttacking = true;
            _animation.SetAttack(_currentWeapon.AttackSpeed);

            StartCoroutine(AttackCallDown());
        }
    }

    private IEnumerator AttackCallDown()
    {
        yield return new WaitForSeconds(_currentWeapon.AttackCallDown);
        _isAttacking = false;
    }

    public void MakeAttack()
    {
        if(_isAttacking)
            _currentWeapon.Attack(transform.position + Vector3.up * 1.5f, transform.forward, _health);
    }

    public void SetWeapon(GameObject weapon)
    {
        _currentWeapon?.Drop();
        _currentWeapon = Instantiate(weapon, _rightHand).GetComponent<WeaponInHand>();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
