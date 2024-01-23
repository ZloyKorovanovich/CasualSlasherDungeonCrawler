using UnityEngine;

[RequireComponent(typeof(CharacterMain))]
public abstract class CharacterComponent : MonoBehaviour
{
    protected CharacterMain _characterMain;

    protected void Init()
    {
        _characterMain = GetComponent<CharacterMain>();
        _characterMain.onDeath += OnDeath;
    }

    protected virtual void OnDeath()
    {
        Destroy(this);
    }
}
