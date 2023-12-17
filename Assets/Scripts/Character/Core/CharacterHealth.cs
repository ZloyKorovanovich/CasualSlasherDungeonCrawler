using UnityEngine;

[RequireComponent(typeof(CharacterMain))]
public class CharacterHealth : CharacterComponent, IDamagable
{
    [SerializeField]
    private float _maxHealth = 100.0f;
    [SerializeField]
    private float _health = 100.0f;
    [SerializeField]
    private HealthBar _healthBar;

    private CharacterAnimation _animation;

    private void OnValidate()
    {
        if(_health > _maxHealth)
            _maxHealth = _health;
    }

    private void Awake()
    {
        _characterMain = GetComponent<CharacterMain>();
        _animation = GetComponent<CharacterAnimation>();
        _characterMain.OnDeath += Dispose;
    }

    private void Start()
    {
        _healthBar?.FillBar(_health / _maxHealth);
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
        _characterMain.Hit();
        _healthBar?.FillBar(_health / _maxHealth);
        if(_health <= 0.0f)
        {
            _health = 0.0f;
            _characterMain.Die();
        }

        _animation.SetHit();
    }
}
