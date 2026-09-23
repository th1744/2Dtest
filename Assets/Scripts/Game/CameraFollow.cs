using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField, Min(0.01f)] private float smoothTime = 0.15f;
    [SerializeField, Min(0.1f)] private float maxFollowSpeed = 20f;
    [SerializeField, Min(0f)] private float lookAheadDistance = 1.5f;
    [SerializeField, Range(0f, 0.5f)] private float lookAheadStrength = 0.25f;
    private Camera followCamera;

    private Vector2 currentVelocity;
    private float fixedZ;

    private void Awake()
    {
        fixedZ = transform.position.z;
        followCamera = GetComponent<Camera>();
    }

    private void OnEnable()
    {
        currentVelocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            currentVelocity = Vector2.zero;
            return;
        }

        if (Time.deltaTime <= 0f)
            return;

        Vector2 currentPosition = transform.position;
        Vector2 lookAhead = Vector2.zero;
        if (Mouse.current != null)
        {
            Vector2 mouse = Mouse.current.position.ReadValue();
            float depth = target.position.z - fixedZ;
            Vector2 mouseWorld = followCamera.ScreenToWorldPoint(
                new Vector3(mouse.x, mouse.y, depth));
            float limit = lookAheadDistance;
            if (followCamera.orthographic)
            {
                float halfHeight = followCamera.orthographicSize;
                limit = Mathf.Min(limit,
                    Mathf.Min(halfHeight, halfHeight * followCamera.aspect) * 0.3f);
            }
            lookAhead = Vector2.ClampMagnitude(
                (mouseWorld - (Vector2)target.position) * lookAheadStrength, limit);
        }
        Vector2 targetPosition = (Vector2)target.position + lookAhead;
        Vector2 nextPosition = Vector2.SmoothDamp(
            currentPosition, targetPosition, ref currentVelocity,
            smoothTime, maxFollowSpeed, Time.deltaTime);

        // Settle exactly once both distance and speed are very small.
        if ((targetPosition - nextPosition).sqrMagnitude < 0.000001f
            && currentVelocity.sqrMagnitude < 0.0001f)
        {
            nextPosition = targetPosition;
            currentVelocity = Vector2.zero;
        }

        transform.position = new Vector3(nextPosition.x, nextPosition.y, fixedZ);
    }
}
