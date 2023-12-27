using UnityEngine;

public class ParticleAnimation : MonoBehaviour
{
    [SerializeField]
    private GameObject _particle;

    private ParticleManager _manager;

    private void Start()
    {
        _manager = ServiceLocator.GetService<ParticleManager>();
    }

    public void Spawn()
    {
        _manager.SpawnParticle(_particle, transform.position, transform.eulerAngles);
    }
}
