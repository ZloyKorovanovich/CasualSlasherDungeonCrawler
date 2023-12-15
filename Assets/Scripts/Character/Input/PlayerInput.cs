using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(CharacterMain))]
public class PlayerInput : MonoBehaviour
{
    private const string _VERTICAL = "Vertical";
    private const string _HORIZONTAL = "Horizontal";

    [SerializeField]
    private LayerMask _interactable;
    [SerializeField]
    private LayerMask _enemy;
    [SerializeField]
    private float _visionRadius = 5.0f;

    private CharacterMain _character;
    private Vector3 _oldTarget;

    private void Awake()
    {
        _character = GetComponent<CharacterMain>();
        _character.OnDeath += CharacterDeath;
    }

    private void CharacterDeath()
    {
        Destroy(this);
    }

    private void Update()
    {
        var inputAxis = new Vector3(Input.GetAxis(_HORIZONTAL), 0, Input.GetAxis(_VERTICAL));
        var currentTarget = Vector3.Lerp(_oldTarget, TargetPoint(inputAxis), Time.deltaTime * 2.0f);
        _character.SetInputs(inputAxis, currentTarget, Input.GetMouseButtonUp(0));
        _oldTarget = currentTarget;
    }

    private Vector3 TargetPoint(Vector3 inputAxis)
    {
        // Need to sort priority, primary targets => enemies, secondaey => interactable

        var closestTarget = transform.position + inputAxis + Vector3.up * 1.5f;
        var casted = Physics.OverlapSphere(transform.position, _visionRadius, _enemy + _interactable);

        if (casted.Length > 0)
        {
            var closestMagnitude = _visionRadius + 1.0f;
            foreach (var target in casted)
            {
                if (target.transform == transform)
                    continue;

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