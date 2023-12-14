using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(CharacterMain))]
public class PlayerInput : MonoBehaviour
{
    private const string _VERTICAL = "Vertical";
    private const string _HORIZONTAL = "Horizontal";

    [SerializeField]
    private LayerMask _lookable;
    [SerializeField]
    private float _visionRadius = 5.0f;

    private CharacterMain _character;

    private void Awake()
    {
        _character = GetComponent<CharacterMain>();
    }

    private void Update()
    {
        var inputAxis = new Vector3(Input.GetAxis(_HORIZONTAL), 0, Input.GetAxis(_VERTICAL));
        _character.SetInputs(inputAxis, Input.GetMouseButton(0), TargetPoint(inputAxis));
    }

    private Vector3 TargetPoint(Vector3 inputAxis)
    {
        var casted = Physics.OverlapSphere(transform.position, _visionRadius, _lookable);
        var closestTarget = transform.position + inputAxis + Vector3.up * 1.5f;

        if (casted.Length > 0)
        {
            var closestMagnitude = _visionRadius;
            foreach (var target in casted)
            {
                var currentMagnitude = Vector3.SqrMagnitude(transform.position - target.transform.position);
                if (currentMagnitude < closestMagnitude)
                {
                    closestTarget = target.transform.position;
                    closestMagnitude = currentMagnitude;
                }
            }
        }

        return closestTarget;
    }
}