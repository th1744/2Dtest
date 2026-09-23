using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 moveDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        moveDirection = Vector2.zero;
        Keyboard keyboard = Keyboard.current;

        if (keyboard == null)
            return;

        if (keyboard.wKey.isPressed)
            moveDirection.y += 1f;
        if (keyboard.sKey.isPressed)
            moveDirection.y -= 1f;
        if (keyboard.aKey.isPressed)
            moveDirection.x -= 1f;
        if (keyboard.dKey.isPressed)
            moveDirection.x += 1f;

        // Keep diagonal movement at the same speed as straight movement.
        moveDirection = moveDirection.normalized;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * moveSpeed;
    }
}
