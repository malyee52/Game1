using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private Transform[] waypoints;

    public IEnumerator SpawnEnemies(int enemyCount, float spawnInterval, EnemyData enemyData)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            SpawnEnemy(enemyData);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy(EnemyData enemyData)
    {
        if (enemyPrefab == null || waypoints == null || waypoints.Length == 0 || enemyData == null)
        {
            Debug.LogWarning("EnemySpawner 설정이 부족합니다.");
            return;
        }

        Enemy enemy = Instantiate(enemyPrefab, waypoints[0].position, Quaternion.identity);
        enemy.Initialize(enemyData, waypoints);
    }
}
