using UnityEngine;

[RequireComponent(typeof(CharacterMain))]
public class CharacterHealth : CharacterComponent, IDamagable
{
    [SerializeField]
    private float _health = 100.0f;

    private CharacterAnimation _animation;

    private void Awake()
    {
        _characterMain = GetComponent<CharacterMain>();
        _animation = GetComponent<CharacterAnimation>();

        _characterMain.OnDeath += Dispose;
    }

    private void Dispose()
    {
        Destroy(this);
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        _characterMain.Hit();
        if(_health <= 0.0f)
        {
            _health = 0.0f;
            _characterMain.Die();
        }

        _animation.SetHit();
    }
}
