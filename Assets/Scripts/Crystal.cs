using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 10.0f;
    [SerializeField]
    private float _movingSpeed = 5.0f;
    [SerializeField]
    private float _movingAmplitude = 0.3f;

    private Vector3 _pos;

    private void OnEnable()
    {
        _pos = transform.position;
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * _rotateSpeed * Time.deltaTime);
        transform.position = _pos + Vector3.up * Mathf.Sin(Time.time * _movingSpeed) * _movingAmplitude;
    }
}
