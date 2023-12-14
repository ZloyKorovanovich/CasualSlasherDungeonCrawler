using System;
using UnityEngine;

public class CharacterHealth : IDamagable
{
    private Action _onDeath;

    [SerializeField]
    private float _health = 100.0f;

    public void SubscibeDeath(Action onDeath)
    {
        _onDeath += onDeath;
    }

    public void UnsubscribeDeath(Action onDeath)
    {
        _onDeath -= onDeath;
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        if(_health < 0.0f)
        {
            _health = 0.0f;
            _onDeath?.Invoke();
        }
    }
}
