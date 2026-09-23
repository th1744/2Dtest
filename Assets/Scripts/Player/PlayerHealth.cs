using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHP = 5;
    [SerializeField] private float invulnerabilityTime = 0.8f;
    public int CurrentHP { get; private set; }
    public int MaxHP => maxHP;
    public bool IsDead => CurrentHP <= 0;
    private float nextDamageTime;

    private void Awake() { CurrentHP = maxHP; }
    public void TakeDamage(int damage)
    {
        if (IsDead || damage <= 0 || Time.time < nextDamageTime) return;
        CurrentHP = Mathf.Max(0, CurrentHP - damage);
        nextDamageTime = Time.time + invulnerabilityTime;
        if (!IsDead) return;
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerAim>().enabled = false;
        GetComponent<PlayerShoot>().enabled = false;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }
}
