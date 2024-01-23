using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour, IService
{
    private readonly Dictionary<string, float> _axes = new Dictionary<string, float>();
    private readonly Dictionary<string, bool> _states = new Dictionary<string, bool>();

    #region IService
    private void OnEnable()
    {
        ServiceLocator.RegisterService(this);
    }

    private void OnDisable()
    {
        ServiceLocator.UnregisterService<ControlManager>();
    }
    #endregion

    public void SetAxis(string name, float value)
    {
        if(_axes.ContainsKey(name))
        {
            _axes[name] = value;
            return;
        }

        _axes.Add(name, value);
    }

    public float GetAxis(string name)
    {
        if(!_axes.ContainsKey(name))
            return 0f;

        return _axes[name];
    }

    public void SetState(string name, bool value)
    {
        if (_states.ContainsKey(name))
        {
            _states[name] = value;
            return;
        }

        _states.Add(name, value);
    }

    public void InverseState(string name)
    {
        if (_states.ContainsKey(name))
        {
            _states[name] = !_states[name];
            return;
        }

        _states.Add(name, true);
    }

    public bool GetState(string name)
    {
        if (!_states.ContainsKey(name))
            return false;

        return _states[name];
    }
}
