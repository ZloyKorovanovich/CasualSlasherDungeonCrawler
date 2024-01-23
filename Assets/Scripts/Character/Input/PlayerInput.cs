using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : CharacterInput
{
    public LayerMask lookable;
    public float visionRadius = 5.0f;

    private ControlManager _manager;

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        _manager = ServiceLocator.GetService<ControlManager>();
    }

    private void Update()
    {
        if(_manager.GetState("Stuck"))
        {
            _characterMain.SetInputs(Vector3.zero, TargetPoint(transform.position + transform.forward + Vector3.up * 1.5f), false);
            return;
        }

        var inputAxis = new Vector3(_manager.GetAxis("Horizontal"), 0, _manager.GetAxis("Vertical"));
        _characterMain.SetInputs(inputAxis, TargetPoint(inputAxis), _manager.GetState("IsAttack"));
    }

    private Vector3 TargetPoint(Vector3 inputAxis)
    {
        var closestTarget = transform.position + inputAxis + Vector3.up * 1.5f;
        var casted = Physics.OverlapSphere(transform.position, visionRadius, lookable, QueryTriggerInteraction.Collide);

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

            for (int i = 0; i < secondaryList.Count; i++)
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