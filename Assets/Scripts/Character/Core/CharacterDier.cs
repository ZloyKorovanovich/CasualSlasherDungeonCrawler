using UnityEngine;

[RequireComponent(typeof(CharacterMain))]
public class CharacterDier : MonoBehaviour
{
    private void Awake()
    {
        ActivateRagdoll(false);
        GetComponent<CharacterMain>().OnDeath += Die;
    }

    private void Die()
    {
        ActivateRagdoll(true);
        Destroy(this);
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
