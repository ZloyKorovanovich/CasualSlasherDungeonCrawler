using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMain))]
public class CharacterDier : CharacterComponent
{
    [SerializeField]
    private List<GameObject> _drop = new List<GameObject>();

    private void Awake()
    {
        ActivateRagdoll(false);
        _characterMain = GetComponent<CharacterMain>();
        _characterMain.OnDeath += Die;
    }

    private void Die()
    {
        DropItems();
        ActivateRagdoll(true);
        Destroy(this);
    }

    private void DropItems()
    {
        var service = ServiceLocator.GetService<PropManager>();
        foreach (var item in _drop)
            service.SpawnLostProp(item, transform.position + Vector3.up * 1.6f, transform.rotation);
    }

    private void ActivateRagdoll(bool activate)
    {
        var Ragdoll = GetComponentsInChildren<Rigidbody>();
        foreach (var rb in Ragdoll)
        {
            rb.isKinematic = !activate;
            rb.transform.GetComponent<Collider>().enabled = activate;
        }
    }
}
