using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private float waveDelay = 2f;
    [SerializeField] private int defeatEnemyCount = 20;

    private bool isGameOver;

    private void Start()
    {
        StartCoroutine(GameLoop());
    }

    private IEnumerator GameLoop()
    {
        while (!isGameOver && waveManager != null && waveManager.HasRemainingWaves)
        {
            waveManager.StartNextWave();

            while (!isGameOver && waveManager.IsWaveRunning)
            {
                CheckDefeatCondition();
                yield return null;
            }

            if (!isGameOver)
            {
                yield return new WaitForSeconds(waveDelay);
            }
        }

        if (!isGameOver)
        {
            Debug.Log("게임 종료: 모든 웨이브 완료");
        }
    }

    private void CheckDefeatCondition()
    {
        int aliveEnemies = FindObjectsOfType<Enemy>().Length;
        if (aliveEnemies >= defeatEnemyCount)
        {
            isGameOver = true;
            StopAllCoroutines();
            Debug.Log($"패배: 적이 {defeatEnemyCount}기 이상 누적되었습니다.");
        }
    }
}
