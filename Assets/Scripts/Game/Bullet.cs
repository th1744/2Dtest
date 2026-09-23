using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField, Min(0f)] private float speed = 15f;
    [SerializeField, Min(0.01f)] private float lifetime = 3f;
    [SerializeField, Min(1)] private int damage = 1;
    private bool consumed;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (consumed) return;
        EnemyHealth enemy = other.GetComponentInParent<EnemyHealth>();
        if (enemy == null) return;
        consumed = true;
        enemy.TakeDamage(damage);
        Destroy(gameObject);
    }

    private void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = (Vector2)transform.up * speed;
        Destroy(gameObject, lifetime);
    }
}
