using UnityEngine;

public class Barrel : MonoBehaviour, IDamagable
{
    public GameObject drop;
    public GameObject particle;

    [Range(0f, 100f)]
    public float probability = 50f;

    public void TakeDamage(float damage)
    {
        var random = Random.Range(0f, 100f);
        if (random < probability)
            ServiceLocator.GetService<PropManager>().SpawnLostProp(drop, transform.position, Quaternion.Euler(Vector3.zero));

        ServiceLocator.GetService<ParticleManager>().SpawnParticle(particle, transform.position, Vector3.zero);
        Destroy(gameObject);
    }
}
