using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] float timeBetweenWaves = 1f;
    [SerializeField] int numEnemiesPerWave = 8;
    [SerializeField] float decreaseTimeBetweenShotsRatio = 0.96f;

    WaveConfig currentWave;
    float currentDecreaseRatio = 1f;

    void Start()
    {
        StartCoroutine(SpawnEnemyWaves());
    }

    IEnumerator SpawnEnemyWaves()
    {
        while (true)
        {
            foreach (WaveConfig wave in waveConfigs)
            {
                currentWave = wave;
                for (int i = 0; i < numEnemiesPerWave; i++)
                {
                    GameObject enemy = Instantiate(currentWave.GetRandomEnemyPrefab(),
                                                        currentWave.GetStartingWayPoint().position,
                                                        Quaternion.Euler(new Vector3(0, 0, 180)),
                                                        transform);

                    IncreaseEnemyFiringRate(enemy);

                    yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
                }
                yield return new WaitForSeconds(timeBetweenWaves);
            }

            // After we looped through all waves, we decrease the time between 2 enemy shots
            currentDecreaseRatio *= decreaseTimeBetweenShotsRatio;
        }
    }

    void IncreaseEnemyFiringRate(GameObject enemy)
    {
        Shooter enemyShooter = enemy.GetComponent<Shooter>();
        if (enemyShooter != null)
        {
            enemyShooter.DecreaseTimeBetweenShots(currentDecreaseRatio);
        }
    }

    public WaveConfig GetCurrentWave()
    {
        return currentWave;
    }
}
