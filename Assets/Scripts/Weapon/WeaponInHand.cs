using UnityEngine;

public class WeaponInHand : MonoBehaviour
{
    [SerializeField]
    private int _level;
    [SerializeField]
    private float _damage = 20.0f;
    [SerializeField]
    private float _attackLength = 2.0f;
    [SerializeField]
    private float _attackRadius = 1.5f;
    [SerializeField]
    private LayerMask _attackable;
    [SerializeField]
    private float _attackCallDown = 1.0f;
    [SerializeField]
    private float _attackSpeed = 1.0f;
    [SerializeField]
    private GameObject _weaponOnGround;

    public int Level => _level;
    public float AttackSpeed => _attackSpeed;
    public float AttackCallDown => _attackCallDown;

    public void Attack(Vector3 position, Vector3 direction, IDamagable attacker)
    {
        var casted = Physics.SphereCastAll(position, _attackRadius, direction, _attackLength, _attackable);
        if(casted.Length > 0)
        {
            foreach(var enemy in casted)
            {
                var damagable = enemy.transform.gameObject.GetComponent<IDamagable>();
                if (damagable != null && damagable != attacker)
                    damagable.TakeDamage(_damage);
            }
        }
    }

    public void Drop()
    {
        if(_weaponOnGround)
        {
            var drop = ServiceLocator.GetService<PropManager>().SpawnProp(_weaponOnGround, transform.position, transform.rotation);
            drop.GetComponent<Rigidbody>().AddForce(Vector3.up * 12.0f);
        }

        Destroy(gameObject);
    }
}
