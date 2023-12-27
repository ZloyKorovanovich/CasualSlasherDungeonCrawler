using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour, IService
{
    private readonly Dictionary<string, float> _axes = new Dictionary<string, float>();
    private readonly Dictionary<string, bool> _states = new Dictionary<string, bool>();

    private void Awake()
    {
        ServiceLocator.RegisterService(this);
    }

    private void OnDestroy()
    {
        ServiceLocator.UnregisterService<PlayerInputManager>();
    }

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
        {
            Debug.LogError("Attempting to get axis that does not exist");
            throw new InvalidOperationException();
        }

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

    public bool GetState(string name)
    {
        if (!_states.ContainsKey(name))
        {
            Debug.LogError("Attempting to get state that does not exist");
            throw new InvalidOperationException();
        }

        return _states[name];
    }
}
