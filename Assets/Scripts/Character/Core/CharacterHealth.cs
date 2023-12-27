using UnityEngine;

[RequireComponent(typeof(CharacterMain))]
public class CharacterHealth : CharacterComponent, IDamagable, IHealable
{
    [SerializeField]
    private float _maxHealth = 100.0f;
    [SerializeField]
    private float _health = 100.0f;
    [SerializeField]
    private HealthBar _healthBar;
    [SerializeField]
    private ParticleAnimation _bloodAnimation;

    private Animator _animator;

    private void OnValidate()
    {
        if(_health > _maxHealth)
            _maxHealth = _health;
    }

    private void Awake()
    {
        _characterMain = GetComponent<CharacterMain>();
        _animator = GetComponent<Animator>();
        _characterMain.OnDeath += Dispose;
    }

    private void Start()
    {
        _healthBar?.Fill(_health / _maxHealth);
    }

    private void Dispose()
    {
        if(_healthBar)
            Destroy(_healthBar.gameObject);

        Destroy(this);
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        _animator.SetTrigger("Hit");
        _healthBar?.Fill(_health / _maxHealth);
        _bloodAnimation?.Spawn();
        if (_health <= 0.0f)
        {
            _health = 0.0f;
            _characterMain.Die();
        }
    }

    public void Heal(float amount)
    {
        _health += amount;
        if(_health > _maxHealth)
            _health = _maxHealth;

        _animator.SetTrigger("Heal");
        _healthBar?.Fill(_health / _maxHealth);
    }
}
