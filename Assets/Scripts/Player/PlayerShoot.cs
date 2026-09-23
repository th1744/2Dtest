using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField, Min(0.1f)] private float fireRate = 5f;
    private double nextFireTime;

    private void OnEnable()
    {
        nextFireTime = Time.timeAsDouble;
    }

    private void Update()
    {
        if (Time.timeScale <= 0f || Mouse.current == null
            || bulletPrefab == null || firePoint == null)
            return;

        double now = Time.timeAsDouble;
        double interval = 1.0 / Mathf.Max(fireRate, 0.1f);
        if (!Mouse.current.leftButton.isPressed)
        {
            nextFireTime = System.Math.Max(nextFireTime, now);
            return;
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
            nextFireTime = System.Math.Max(nextFireTime, now);

        // Preserve the firing schedule across different frame rates.
        while (now >= nextFireTime)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            nextFireTime += interval;
        }
    }
}
