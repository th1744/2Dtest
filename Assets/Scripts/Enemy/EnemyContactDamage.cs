using UnityEngine;

public class EnemyContactDamage : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    private void OnTriggerEnter2D(Collider2D other) { TryDamage(other); }
    private void OnTriggerStay2D(Collider2D other) { TryDamage(other); }
    private void TryDamage(Collider2D other)
    {
        PlayerHealth player = other.GetComponentInParent<PlayerHealth>();
        if (player != null) player.TakeDamage(damage);
    }
}
