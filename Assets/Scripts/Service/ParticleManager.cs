using System;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour, IService
{
    [SerializeField]
    private int _maxParticles;
    [SerializeField]
    private Transform _particleRoot;

    private List<GameObject> _particles = new List<GameObject>();

    private void Awake()
    {
        ServiceLocator.RegisterService(this);
    }

    private void OnDestroy()
    {
        ServiceLocator.UnregisterService<ParticleManager>();
    }

    public bool SpawnParticle(GameObject particle, Vector3 position, Vector3 eulerAngles)
    {
        if(_particles.Count >= _maxParticles)
        {
            if (!GetFreeSpace(out var index))
                return false;

            _particles[index] = Spawn();

            return true;
        }


        _particles.Add(Spawn());

        GameObject Spawn()
        {
            var instance = Instantiate(particle, _particleRoot);
            instance.transform.position = position;
            instance.transform.eulerAngles = eulerAngles;
            return instance;
        }

        return true;
    }

    public bool GetFreeSpace(out int index)
    {
        for(int i = 0; i < _particles.Count; i++)
        {
            if (!_particles[i])
            {
                index = i;
                return true;
            }
        }

        index = -1;
        return false;
    }
}
