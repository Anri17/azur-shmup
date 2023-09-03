using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AzurShmup
{
    public class EnemySpawnerRandom : MonoBehaviour
    {
        public GameObject enemy;
        public float startDelay = 0f;
        public float spawnRate = 1f;
        public int spawnCount = 1;

        public float topLimit = 0;
        public float bottomLimit = 0;
        public float leftLimit = 0;
        public float rightLimit = 0;

        int spawnCounter;

        void Start()
        {
            spawnCounter = 0;

            if (enemy != null && spawnCount > 0)
            {
                StartCoroutine(SpawnCoroutine());
            }
        }

        IEnumerator SpawnCoroutine()
        {
            yield return new WaitForSeconds(startDelay);
            while (spawnCounter < spawnCount)
            {
                Vector3 spawnPoint = GetSpawnPositon();                 // The point where the enemy will spawn
                Instantiate(enemy, spawnPoint, Quaternion.identity);    // Spawn the Enemy
                spawnCounter++;
                yield return new WaitForSeconds(spawnRate);
            }
            yield return null;
        }

        Vector3 GetSpawnPositon()
        {
            // Get random coordinates from the limits set in the inspector
            float x = gameObject.transform.position.x + Random.Range(leftLimit, rightLimit);
            float y = gameObject.transform.position.y + Random.Range(bottomLimit, topLimit);
            return new Vector3(x, y, 0);    // return the new position to spawn the enemy
        }
    }
}
