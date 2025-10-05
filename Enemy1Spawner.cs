using System.Collections;
using UnityEngine;

namespace Shmup
{
    public class Enemy1Spawner : MonoBehaviour
    {

        [SerializeField] GameObject enemyPrefab;
        [SerializeField] int minEnemiesPerBatch = 1;
        [SerializeField] int maxEnemiesPerBatch = 3;
        [SerializeField] float minSpawnInterval = 2f;
        [SerializeField] float maxSpawnInterval = 3f;

        [SerializeField] float initialSpawn = 0f;
        [SerializeField] float spawnRadius = 0f;



        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            StartCoroutine(SpawnEnemies());
        }

        private IEnumerator SpawnEnemies()
        {
            while (true)
            {
                float interval = Random.Range(minSpawnInterval, maxSpawnInterval);
                yield return new WaitForSeconds(interval);

                int enemiesBatchSize = Random.Range(minEnemiesPerBatch, maxEnemiesPerBatch);
                for (int i = 0; i < enemiesBatchSize; i++)
                {
                    float spawnOffSet = Random.Range(initialSpawn, spawnRadius);
                    Vector2 spawnPostion = (Vector2)transform.position;
                    spawnPostion.x += spawnOffSet;

                    Instantiate(enemyPrefab, spawnPostion, Quaternion.identity);

                }
            }
        }
    }
}
