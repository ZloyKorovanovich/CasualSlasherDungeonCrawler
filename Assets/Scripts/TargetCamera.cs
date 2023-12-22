using UnityEngine;

public class TargetCamera : MonoBehaviour
{
    [SerializeField]
    private float _initialDistance = 5.0f;
    [SerializeField]
    private float _height = 5.0f;
    [SerializeField]
    private float _angle = 180.0f;
    [SerializeField]
    private LayerMask _obstacleLayer;
    [SerializeField]
    private Transform _target;

    private float _rayPosition = 0.3f;


    private float _currentDistance;

    private void Awake()
    {
        if (!_target)
            _target = GameObject.FindGameObjectWithTag("Player").transform;

        InitializeCameraPosition();
    }

    void LateUpdate()
    {
        UpdateDistance();
        UpdateCameraPosition();
    }

    private void InitializeCameraPosition()
    {
        float angleRad = Mathf.Deg2Rad * _angle;

        Vector3 offset = new Vector3(Mathf.Sin(angleRad) * _initialDistance, _height, Mathf.Cos(angleRad) * _initialDistance);
        Vector3 targetPosition = _target.position + Vector3.up * _rayPosition;
        Vector3 cameraPosition = targetPosition + offset;

        transform.position = cameraPosition;
        transform.LookAt(targetPosition);

        _currentDistance = _initialDistance;
    }

    private void UpdateDistance()
    {

        float angleRad = Mathf.Deg2Rad * _angle;

        Vector3 offset = new Vector3(Mathf.Sin(angleRad) * _currentDistance, _height, Mathf.Cos(angleRad) * _currentDistance);
        Vector3 targetPosition = _target.transform.position + Vector3.up * _rayPosition;
        Vector3 cameraPosition = targetPosition + offset;

        Debug.DrawRay(targetPosition, (cameraPosition - targetPosition).normalized * 100f, Color.red, 0.1f);
        if (Physics.Raycast(targetPosition, (cameraPosition - targetPosition).normalized, _obstacleLayer))
            _currentDistance = Mathf.Lerp(_currentDistance, 1, Time.deltaTime * 4);
        else
            _currentDistance = Mathf.Lerp(_currentDistance, _initialDistance, Time.deltaTime * 4);
    }

    private void UpdateCameraPosition()
    {
        float angleRad = Mathf.Deg2Rad * _angle;

        Vector3 offset = new Vector3(Mathf.Sin(angleRad) * _currentDistance, _height, Mathf.Cos(angleRad) * _currentDistance);
        Vector3 targetPosition = _target.transform.position + Vector3.up * _rayPosition;
        Vector3 cameraPosition = targetPosition + offset;

        transform.position = Vector3.Lerp(transform.position, cameraPosition, Time.deltaTime * 4);
        transform.LookAt(targetPosition);
    }
}