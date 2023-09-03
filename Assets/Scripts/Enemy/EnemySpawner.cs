using System.Collections;
using UnityEngine;

namespace AzurShmup.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [Header("Spawn Settings")]
        [Tooltip("The game object prefab which will be instantiated by this spawner")]
        [SerializeField] private GameObject prefab;
        
        [Tooltip("How many enemies this spawn will instantiate")]
        [SerializeField] private int spawnCount = 1;
        
        [Tooltip("How long to wait before this spawner starts creating the first enemy game object instance")]
        [SerializeField] private float startDelay = 0f;
        
        [Tooltip("How many enemies, per second, the spawner will instantiate a new enemy game object")]
        [SerializeField] private float spawnRate = 1f;
        
        private void Start()
        {
            StartCoroutine(SpawnEnemyCoroutine());
        }

        private IEnumerator SpawnEnemyCoroutine()
        {
            yield return new WaitForSeconds(startDelay);
            
            // Instantiate the enemy a number of times defined by spawnCount
            for (int spawnIndex = 0; spawnIndex < spawnCount; spawnIndex++)
            {
                Vector3 spawnPoint = gameObject.transform.position;     // The poisition of this object
                Instantiate(prefab, spawnPoint, Quaternion.identity);   // Spawn the enemy
                yield return new WaitForSeconds(1 / spawnRate);
            }
            
            yield return null;
        }
    }
}
