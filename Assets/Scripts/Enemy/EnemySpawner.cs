using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyChase enemyPrefab;
    [SerializeField] private PlayerHealth player;
    [SerializeField] private Camera spawnCamera;
    [SerializeField] private CombatHUD hud;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private int maxEnemies = 10;
    [SerializeField] private float minPlayerDistance = 6f;
    private readonly List<EnemyChase> enemies = new List<EnemyChase>();
    private float timer;

    private void Update()
    {
        if (player == null || player.IsDead || enemyPrefab == null || spawnCamera == null) return;
        timer += Time.deltaTime;
        if (timer < spawnInterval) return;
        timer = 0f;
        enemies.RemoveAll(enemy => enemy == null);
        if (enemies.Count >= maxEnemies) return;

        float height = spawnCamera.orthographicSize;
        float width = height * spawnCamera.aspect;
        for (int attempt = 0; attempt < 20; attempt++)
        {
            float margin = Random.Range(1f, 2f);
            Vector2 offset;
            switch (Random.Range(0, 4))
            {
                case 0: offset = new Vector2(-width - margin, Random.Range(-height, height)); break;
                case 1: offset = new Vector2(width + margin, Random.Range(-height, height)); break;
                case 2: offset = new Vector2(Random.Range(-width, width), height + margin); break;
                default: offset = new Vector2(Random.Range(-width, width), -height - margin); break;
            }
            Vector2 position = (Vector2)spawnCamera.transform.position + offset;
            if (Vector2.Distance(position, player.transform.position) < minPlayerDistance) continue;
            EnemyChase enemy = Instantiate(enemyPrefab,
                new Vector3(position.x, position.y, player.transform.position.z), Quaternion.identity);
            enemy.SetTarget(player);
            enemy.GetComponent<EnemyHealth>().SetScoreTarget(hud);
            enemies.Add(enemy);
            break;
        }
    }
}
