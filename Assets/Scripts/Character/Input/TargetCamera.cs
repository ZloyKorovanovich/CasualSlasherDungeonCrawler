using UnityEngine;

public class TargetCamera : MonoBehaviour
{
    [SerializeField]
    private float initialDistance = 10.0f;
    [SerializeField]
    private float height = 10.0f;
    [SerializeField]
    private float angle = 180.0f;
    [SerializeField]
    private LayerMask obstacleLayer;
    [SerializeField]
    private Transform target;

    private float currentDistance;

    private void Awake()
    {
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
        Vector3 targetPosition = target.position + Vector3.up * 1.3f;
        Vector3 cameraPosition = targetPosition + offset;

        transform.position = cameraPosition;
        transform.LookAt(targetPosition);

        currentDistance = initialDistance;
    }

    private void UpdateDistance()
    {

        float angleRad = Mathf.Deg2Rad * angle;

        Vector3 offset = new Vector3(Mathf.Sin(angleRad) * currentDistance, height, Mathf.Cos(angleRad) * currentDistance);
        Vector3 targetPosition = target.transform.position + Vector3.up * 1.3f;
        Vector3 cameraPosition = targetPosition + offset;

        if (Physics.Raycast(targetPosition, (cameraPosition - targetPosition).normalized, obstacleLayer))
            currentDistance = Mathf.Lerp(currentDistance, 0, Time.deltaTime * 4);
        else
            currentDistance = Mathf.Lerp(currentDistance, initialDistance, Time.deltaTime * 4);
    }

    private void UpdateCameraPosition()
    {
        float angleRad = Mathf.Deg2Rad * angle;

        Vector3 offset = new Vector3(Mathf.Sin(angleRad) * currentDistance, height, Mathf.Cos(angleRad) * currentDistance);
        Vector3 targetPosition = target.transform.position + Vector3.up * 1.3f;
        Vector3 cameraPosition = targetPosition + offset;

        transform.position = Vector3.Lerp(transform.position, cameraPosition, Time.deltaTime * 4);
        transform.LookAt(targetPosition);
    }
}