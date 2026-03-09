using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveDefinition
{
    public EnemyData enemyData;
    public int enemyCount = 10;
    public float spawnInterval = 0.8f;
}

public class WaveManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private List<WaveDefinition> waves = new();

    private int currentWaveIndex = -1;
    private int aliveEnemyCount;

    public bool IsWaveRunning { get; private set; }
    public bool HasRemainingWaves => currentWaveIndex + 1 < waves.Count;

    private void OnEnable()
    {
        Enemy.Spawned += HandleEnemySpawned;
        Enemy.Died += HandleEnemyRemoved;
    }

    private void OnDisable()
    {
        Enemy.Spawned -= HandleEnemySpawned;
        Enemy.Died -= HandleEnemyRemoved;
    }

    public void StartNextWave()
    {
        if (IsWaveRunning || !HasRemainingWaves)
        {
            return;
        }

        currentWaveIndex++;
        StartCoroutine(RunWave(waves[currentWaveIndex]));
    }

    private IEnumerator RunWave(WaveDefinition wave)
    {
        IsWaveRunning = true;
        yield return StartCoroutine(enemySpawner.SpawnEnemies(wave.enemyCount, wave.spawnInterval, wave.enemyData));

        while (aliveEnemyCount > 0)
        {
            yield return null;
        }

        IsWaveRunning = false;
    }

    private void HandleEnemySpawned(Enemy enemy)
    {
        aliveEnemyCount++;
    }

    private void HandleEnemyRemoved(Enemy enemy)
    {
        aliveEnemyCount = Mathf.Max(0, aliveEnemyCount - 1);
    }
}
