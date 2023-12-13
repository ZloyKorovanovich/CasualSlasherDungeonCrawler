using UnityEngine;

[DisallowMultipleComponent]
public class CharacterMain : MonoBehaviour
{
    private CharacterMover _mover;

    private void Awake()
    {
        _mover = GetComponent<CharacterMover>();
    }
}
