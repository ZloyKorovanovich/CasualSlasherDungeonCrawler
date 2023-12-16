using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[DisallowMultipleComponent]
[RequireComponent(typeof(CharacterMain))]
public class PlayerInput : CharacterComponent
{
    private const string _VERTICAL = "Vertical";
    private const string _HORIZONTAL = "Horizontal";

    [SerializeField]
    private LayerMask _lookable;
    [SerializeField]
    private float _visionRadius = 5.0f;

    private void Awake()
    {
        _characterMain = GetComponent<CharacterMain>();
        _characterMain.OnDeath += CharacterDeath;
    }

    private void CharacterDeath()
    {
        Destroy(this);
    }

    private void Update()
    {
        var inputAxis = new Vector3(Input.GetAxis(_HORIZONTAL), 0, Input.GetAxis(_VERTICAL));
        _characterMain.SetInputs(inputAxis, TargetPoint(inputAxis), Input.GetMouseButtonUp(0));
    }

    private Vector3 TargetPoint(Vector3 inputAxis)
    {
        var closestTarget = transform.position + inputAxis + Vector3.up * 1.5f;
        var casted = Physics.OverlapSphere(transform.position, _visionRadius, _lookable, QueryTriggerInteraction.Collide);

        if (casted.Length > 0)
        {
            var closestMagnitude = Mathf.Infinity;

            bool isSecondIteration = true;
            var secondaryList = new List<Transform>();

            for(int i = 0; i < casted.Length; i++)
            {
                if (casted[i].transform == transform)
                    continue;

                if (!casted[i].GetComponent<CharacterMain>())
                {
                    secondaryList.Add(casted[i].transform);
                    continue;
                }

                var magnitude = Vector3.SqrMagnitude(casted[i].transform.position - transform.position);
                if (magnitude < closestMagnitude)
                {
                    isSecondIteration = false;
                    closestMagnitude = magnitude;
                    closestTarget = casted[i].transform.position + Vector3.up * 1.6f;
                }
            }

            if (!isSecondIteration)
                return closestTarget;

            for (int i = 1; i < secondaryList.Count; i++)
            {
                var magnitude = Vector3.SqrMagnitude(secondaryList[i].position - transform.position);
                if (magnitude < closestMagnitude)
                {
                    closestMagnitude = magnitude;
                    closestTarget = secondaryList[i].transform.position + Vector3.up * 1.6f;
                }
            }

            return closestTarget;
        }

        return closestTarget;
    }
}