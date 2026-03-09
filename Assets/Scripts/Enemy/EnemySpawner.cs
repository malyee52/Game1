using System.Collections;
using System.Collections.Generic;
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
        Transform[] validWaypoints = GetValidWaypoints();
        if (enemyPrefab == null || validWaypoints.Length == 0 || enemyData == null)
        {
            Debug.LogWarning("EnemySpawner 설정이 부족합니다.");
            return;
        }

        Enemy enemy = Instantiate(enemyPrefab, validWaypoints[0].position, Quaternion.identity);
        enemy.Initialize(enemyData, validWaypoints);
    }

    private Transform[] GetValidWaypoints()
    {
        if (waypoints == null || waypoints.Length == 0)
        {
            return new Transform[0];
        }

        List<Transform> valid = new List<Transform>(waypoints.Length);
        for (int i = 0; i < waypoints.Length; i++)
        {
            if (waypoints[i] != null)
            {
                valid.Add(waypoints[i]);
            }
        }

        return valid.ToArray();
    }
}
