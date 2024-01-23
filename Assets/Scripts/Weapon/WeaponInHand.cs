using UnityEngine;

public class WeaponInHand : MonoBehaviour
{
    public float speed = 1.0f;
    public float damage = 20.0f;
    public float attackLength = 2.0f;
    public float attackRadius = 1.5f;
    public LayerMask attackable;
    public RuntimeAnimatorController animatorController;
    public GameObject weaponOnGround;

    public void Attack(Vector3 position, Vector3 direction, IDamagable attacker)
    {
        var casted = Physics.SphereCastAll(position, attackRadius, direction, attackLength, attackable);
        foreach (var enemy in casted)
        {
            var damagable = enemy.transform.gameObject.GetComponent<IDamagable>();
            if (damagable != null && damagable != attacker)
                damagable.TakeDamage(damage);
        }
    }

    public void Drop()
    {
        if(weaponOnGround)
        {
            var drop = ServiceLocator.GetService<PropManager>().SpawnProp(weaponOnGround, transform.position, transform.rotation);
            drop.GetComponent<Rigidbody>().AddForce(Vector3.up * 12.0f);
        }

        Destroy(gameObject);
    }
}
