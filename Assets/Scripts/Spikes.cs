using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Spikes : MonoBehaviour
{
    [SerializeField]
    private float _damage = 20.0f;
    [SerializeField]
    private float _damageCallDown = 1.0f;

    private bool _isCourutine;

    void OnTriggerStay(Collider collider)
    {
        IDamagable damagable = collider.GetComponent<IDamagable>();

        if (damagable != null)
        {
            if (_isCourutine)
                return;

            damagable.TakeDamage(_damage);
            _isCourutine = true;
            StartCoroutine(DamageCallDown());
        }
    }

    private IEnumerator DamageCallDown()
    {
        yield return new WaitForSeconds(_damageCallDown);
        _isCourutine = false;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        _isCourutine = false;
    }
}
