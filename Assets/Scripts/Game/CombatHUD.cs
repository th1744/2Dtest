using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatHUD : MonoBehaviour
{
    [SerializeField] private PlayerHealth player;
    private int score;
    public void AddScore(int amount) { score += amount; }

    // Small prototype HUD without font assets or Canvas setup.
    private void OnGUI()
    {
        if (player == null) return;
        GUI.Box(new Rect(15, 15, 190, 65),
            $"HP: {player.CurrentHP} / {player.MaxHP}\nScore: {score}");
        if (!player.IsDead) return;
        Rect panel = new Rect(Screen.width / 2f - 130, Screen.height / 2f - 60, 260, 120);
        GUI.Box(panel, "GAME OVER");
        if (GUI.Button(new Rect(panel.x + 50, panel.y + 50, 160, 40), "Restart"))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
