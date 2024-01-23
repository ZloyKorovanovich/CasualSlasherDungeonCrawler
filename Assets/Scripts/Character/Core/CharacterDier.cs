using System.Collections.Generic;
using UnityEngine;

public class CharacterDier : CharacterComponent
{
    public List<GameObject> drop = new List<GameObject>();

    private void Awake()
    {
        Init();
        ActivateRagdoll(false);
    }

    protected override void OnDeath()
    {
        DropItems();
        var animator = GetComponent<Animator>();
        if(animator)
            Destroy(animator);

        ActivateRagdoll(true);
        base.OnDeath();
    }

    private void DropItems()
    {
        var service = ServiceLocator.GetService<PropManager>();
        foreach (var item in drop)
            service.SpawnLostProp(item, transform.position + Vector3.up * 1.6f, transform.rotation);
    }

    private void ActivateRagdoll(bool activate)
    {
        var Ragdoll = GetComponentsInChildren<Rigidbody>();
        foreach (var rb in Ragdoll)
        {
            rb.isKinematic = !activate;
            var collider = rb.transform.GetComponent<Collider>();
            collider.enabled = activate;
            if (activate)
            {
                var joint = rb.transform.GetComponent<CharacterJoint>();
                if (joint)
                    Destroy(joint, 5.0f);

                Destroy(rb, 5.0f);
                Destroy(collider, 5.0f);
            }
        }
    }
}
