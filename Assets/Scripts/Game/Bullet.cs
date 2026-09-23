using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField, Min(0f)] private float speed = 15f;
    [SerializeField, Min(0.01f)] private float lifetime = 3f;

    private void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = (Vector2)transform.up * speed;
        Destroy(gameObject, lifetime);
    }
}
