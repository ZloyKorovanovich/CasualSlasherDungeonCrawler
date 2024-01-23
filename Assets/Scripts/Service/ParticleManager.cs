using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour, IService
{
    public int maxParticles;
    public Transform particleRoot;

    private List<GameObject> _particles = new List<GameObject>();

    #region IService
    private void OnEnable()
    {
        ServiceLocator.RegisterService(this);
    }

    private void OnDisable()
    {
        ServiceLocator.UnregisterService<ParticleManager>();
    }
    #endregion

    public bool SpawnParticle(GameObject particle, Vector3 position, Vector3 eulerAngles)
    {
        if(_particles.Count >= maxParticles)
        {
            if (!GetFreeSpace(out var index))
                return false;

            _particles[index] = Spawn();

            return true;
        }


        _particles.Add(Spawn());

        GameObject Spawn()
        {
            var instance = Instantiate(particle, particleRoot);
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
