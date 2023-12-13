using UnityEngine;

[RequireComponent(typeof(CharacterMain))]
public class PlayerInput : MonoBehaviour
{
    private const string _VERTICAL = "Vertical";
    private const string _HORIZONTAL = "Horizontal";

    private CharacterMain _character;

    private void Awake()
    {
        _character = GetComponent<CharacterMain>();
    }

    private void Update()
    {
        _character.SetInputs(Input.GetAxis(_VERTICAL), Input.GetAxis(_HORIZONTAL));
    }
}