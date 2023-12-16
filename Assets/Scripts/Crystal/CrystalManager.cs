using UnityEngine;

public class CrystalManager : MonoBehaviour, IService
{
    private int _crystalCount;

    private void Awake()
    {
        ServiceLocator.RegisterService(this);
    }

    private void OnDestroy()
    {
        ServiceLocator.UnregisterService<CrystalManager>();
    }

    public void AddCrystals(int crystals)
    {
        _crystalCount += crystals;
    }
}
