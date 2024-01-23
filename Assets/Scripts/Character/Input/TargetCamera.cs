using UnityEngine;

public class TargetCamera : MonoBehaviour
{
    public float initialDistance = 5.0f;
    public float height = 5.0f;
    public float angle = 180.0f;
    public LayerMask obstacleLayer;
    public Transform target;

    private float _rayPosition = 0.3f;
    private float _currentDistance;

    private void Awake()
    {
        if (!target)
            target = GameObject.FindGameObjectWithTag("Player").transform;

        InitializeCameraPosition();
    }

    void LateUpdate()
    {
        UpdateDistance();
        UpdateCameraPosition();
    }

    private void InitializeCameraPosition()
    {
        float angleRad = Mathf.Deg2Rad * angle;

        Vector3 offset = new Vector3(Mathf.Sin(angleRad) * initialDistance, height, Mathf.Cos(angleRad) * initialDistance);
        Vector3 targetPosition = target.position + Vector3.up * _rayPosition;
        Vector3 cameraPosition = targetPosition + offset;

        transform.position = cameraPosition;
        transform.LookAt(targetPosition);

        _currentDistance = initialDistance;
    }

    private void UpdateDistance()
    {

        float angleRad = Mathf.Deg2Rad * angle;

        Vector3 offset = new Vector3(Mathf.Sin(angleRad) * _currentDistance, height, Mathf.Cos(angleRad) * _currentDistance);
        Vector3 targetPosition = target.transform.position + Vector3.up * _rayPosition;
        Vector3 cameraPosition = targetPosition + offset;

        Debug.DrawRay(targetPosition, (cameraPosition - targetPosition).normalized * 100f, Color.red, 0.1f);
        if (Physics.Raycast(targetPosition, (cameraPosition - targetPosition).normalized, obstacleLayer))
            _currentDistance = Mathf.Lerp(_currentDistance, 1, Time.deltaTime * 4);
        else
            _currentDistance = Mathf.Lerp(_currentDistance, initialDistance, Time.deltaTime * 4);
    }

    private void UpdateCameraPosition()
    {
        float angleRad = Mathf.Deg2Rad * angle;

        Vector3 offset = new Vector3(Mathf.Sin(angleRad) * _currentDistance, height, Mathf.Cos(angleRad) * _currentDistance);
        Vector3 targetPosition = target.transform.position + Vector3.up * _rayPosition;
        Vector3 cameraPosition = targetPosition + offset;

        transform.position = Vector3.Lerp(transform.position, cameraPosition, Time.deltaTime * 4);
        transform.LookAt(targetPosition);
    }
}