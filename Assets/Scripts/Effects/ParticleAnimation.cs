using UnityEngine;

public class ParticleAnimation : MonoBehaviour
{
    public GameObject particle;

    private ParticleManager _manager;

    private void Start()
    {
        _manager = ServiceLocator.GetService<ParticleManager>();
    }

    public void Spawn()
    {
        _manager.SpawnParticle(particle, transform.position, transform.eulerAngles);
    }
}
