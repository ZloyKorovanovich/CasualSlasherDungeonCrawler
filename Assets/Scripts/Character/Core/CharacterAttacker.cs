using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterMain))]
public class CharacterAttacker : MonoBehaviour
{
    [SerializeField]
    private Transform _rightHand;
    [SerializeField]
    private float _pickUpRadius = 3.0f;
    [SerializeField]
    private LayerMask _weaponLayer;

    private WeaponInHand _currentWeapon;
    private CharacterMain _main;
    private CharacterAnimation _animation;
    private CharacterHealth _health;

    private bool _isAttacking;

    public WeaponInHand CurrentWeapon => _currentWeapon;

    private void Awake()
    {
        _main = GetComponent<CharacterMain>();
        _animation = GetComponent<CharacterAnimation>();
        _health = GetComponent<CharacterHealth>();

        _main.OnDeath += Dispose;
    }

    private void Start()
    {
        _main.OnSetInputs += UpdateInputs;
    }

    private void Dispose()
    {
        _currentWeapon?.Drop();
        _animation?.Dispose();
        Destroy(this);
    }

    private void UpdateInputs()
    {
        if (_isAttacking)
            return;

        if (_main.IsAttack && _currentWeapon)
        {
            _isAttacking = true;
            _animation.SetAttack();

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
        _currentWeapon.Attack(transform.position + Vector3.up * 1.5f, transform.forward, _health);
    }

    public void SetWeapon(GameObject weapon)
    {
        _currentWeapon?.Drop();
        _currentWeapon = Instantiate(weapon, _rightHand).GetComponent<WeaponInHand>();
    }
}
