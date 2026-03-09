using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private float waveDelay = 2f;

    private void Start()
    {
        StartCoroutine(GameLoop());
    }

    private IEnumerator GameLoop()
    {
        while (waveManager != null && waveManager.HasRemainingWaves)
        {
            waveManager.StartNextWave();

            while (waveManager.IsWaveRunning)
            {
                yield return null;
            }

            yield return new WaitForSeconds(waveDelay);
        }

        Debug.Log("게임 종료: 모든 웨이브 완료");
    }
}
