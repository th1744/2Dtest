using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerAim : MonoBehaviour
{
    private Rigidbody2D rb;
    private Camera mainCamera;
    private float targetAngle;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        targetAngle = rb.rotation;

        if (mainCamera == null)
        {
            Debug.LogError("PlayerAim requires a camera tagged MainCamera.", this);
            enabled = false;
        }
    }

    private void Update()
    {
        if (mainCamera == null || Mouse.current == null)
            return;

        Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();
        float cameraDistance = transform.position.z - mainCamera.transform.position.z;
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(
            new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, cameraDistance));

        Vector2 direction = (Vector2)mouseWorldPosition - rb.position;

        // Keep the previous direction when the cursor is at the player center.
        if (direction.sqrMagnitude < 0.0001f)
            return;

        // Atan2 measures from the right; this sprite faces up.
        targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
    }

    private void FixedUpdate()
    {
        rb.MoveRotation(targetAngle);
    }
}
