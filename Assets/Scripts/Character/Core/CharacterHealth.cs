using UnityEngine;

[RequireComponent(typeof(CharacterMain))]
public class CharacterHealth : MonoBehaviour, IDamagable
{
    [SerializeField]
    private float _health = 100.0f;

    private CharacterMain _main;
    private CharacterAnimation _animation;

    private void Awake()
    {
        _main = GetComponent<CharacterMain>();
        _animation = GetComponent<CharacterAnimation>();

        _main.OnDeath += Dispose;
    }

    private void Dispose()
    {
        Destroy(this);
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        if(_health <= 0.0f)
        {
            _health = 0.0f;
            _main.Die();
        }

        _animation.SetHit();
    }
}
