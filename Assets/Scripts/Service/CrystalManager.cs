using System;
using UnityEngine;

public class CrystalManager : MonoBehaviour, IService
{
    public Action onAdd;

    private int _crystalCount;
    public int Crystals => _crystalCount;

    #region IService
    private void OnEnable()
    {
        ServiceLocator.RegisterService(this);
    }

    private void OnDisable()
    {
        ServiceLocator.UnregisterService<CrystalManager>();
    }
    #endregion

    public void AddCrystals(int crystals)
    {
        _crystalCount += crystals;
        onAdd?.Invoke();
    }
}
