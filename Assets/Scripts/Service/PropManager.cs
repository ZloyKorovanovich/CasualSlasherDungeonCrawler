using UnityEngine;

public class PropManager : MonoBehaviour, IService
{
    [SerializeField]
    private Transform _propRoot;

    private void Awake()
    {
        ServiceLocator.RegisterService(this);
    }

    private void OnDestroy()
    {
        ServiceLocator.UnregisterService<PropManager>();
    }

    public GameObject SpawnProp(GameObject prop, Vector3 position, Quaternion rotation)
    {
        var instance = Instantiate(prop, _propRoot);
        instance.transform.position = position;
        instance.transform.rotation = rotation;

        return instance;
    }

    public void SpawnLostProp(GameObject prop, Vector3 position, Quaternion rotation)
    {
        var instance = Instantiate(prop, _propRoot);
        instance.transform.position = position;
        instance.transform.rotation = rotation;
    }
}
