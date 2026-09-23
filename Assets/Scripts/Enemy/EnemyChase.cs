using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyChase : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    private PlayerHealth target;
    private Rigidbody2D rb;

    private void Awake() { rb = GetComponent<Rigidbody2D>(); }
    public void SetTarget(PlayerHealth player) { target = player; }

    private void FixedUpdate()
    {
        if (target == null || target.IsDead)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }
        Vector2 direction = ((Vector2)target.transform.position - rb.position).normalized;
        rb.linearVelocity = direction * moveSpeed;
        if (direction.sqrMagnitude > 0f)
            rb.MoveRotation(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f);
    }
}
