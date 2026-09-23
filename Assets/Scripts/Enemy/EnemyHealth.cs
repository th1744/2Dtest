using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHP = 3;
    [SerializeField] private int scoreValue = 10;
    private int hp;
    private bool dead;
    private CombatHUD hud;

    private void Awake() { hp = maxHP; }
    public void SetScoreTarget(CombatHUD scoreDisplay) { hud = scoreDisplay; }
    public void TakeDamage(int damage)
    {
        if (dead || damage <= 0) return;
        hp -= damage;
        if (hp > 0) return;
        dead = true;
        if (hud != null) hud.AddScore(scoreValue);
        Destroy(gameObject);
    }
}
