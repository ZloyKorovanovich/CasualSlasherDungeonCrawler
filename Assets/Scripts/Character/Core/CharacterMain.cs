using UnityEngine;

[DisallowMultipleComponent]
public class CharacterMain : MonoBehaviour
{
    private CharacterMover _mover;

    private void Awake()
    {
        _mover = GetComponent<CharacterMover>();
    }

    public void SetInputs(float vertical, float horizontal)
    {
        var inputAxis = new Vector3(horizontal, 0, vertical);
        _mover?.SetInputs(inputAxis, CastPoint());

        Vector3 CastPoint()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, 100.0f))
                return hit.point;

            return Vector3.zero;
        }
    }
}
