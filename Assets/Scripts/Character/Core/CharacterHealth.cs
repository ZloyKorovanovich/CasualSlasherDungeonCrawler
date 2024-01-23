using UnityEngine;

public class CharacterHealth : CharacterComponent, IDamagable, IHealable
{
    public float maxHealth = 100.0f;
    public FillBar healthBar;
    public ParticleAnimation bloodAnimation;

    private float _health;
    private Animator _animator;

    private void Awake()
    {
        Init();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _health = maxHealth;
        healthBar?.Fill(_health / maxHealth);
    }

    protected override void OnDeath()
    {
        if(healthBar)
            Destroy(healthBar.gameObject);

        base.OnDeath();
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        _animator.SetTrigger("hit");

        healthBar?.Fill(_health / maxHealth);
        bloodAnimation?.Spawn();
        if (_health <= 0.0f)
        {
            _health = 0.0f;
            _characterMain.Die();
        }
    }

    public void Heal(float amount)
    {
        _health += amount;
        if(_health > maxHealth)
            _health = maxHealth;

        _animator.SetTrigger("heal");
        healthBar?.Fill(_health / maxHealth);
    }
}
