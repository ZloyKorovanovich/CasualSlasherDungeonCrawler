using UnityEngine;

public class PropManager : MonoBehaviour, IService
{
    public Transform propRoot;

    #region IService
    private void OnEnable()
    {
        ServiceLocator.RegisterService(this);
    }

    private void OnDisable()
    {
        ServiceLocator.UnregisterService<PropManager>();
    }
    #endregion

    public GameObject SpawnProp(GameObject prop, Vector3 position, Quaternion rotation)
    {
        var instance = Instantiate(prop, propRoot);
        instance.transform.position = position;
        instance.transform.rotation = rotation;

        return instance;
    }

    public void SpawnLostProp(GameObject prop, Vector3 position, Quaternion rotation)
    {
        var instance = Instantiate(prop, propRoot);
        instance.transform.position = position;
        instance.transform.rotation = rotation;
    }
}
