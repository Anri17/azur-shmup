using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AzurShmup.Stage.Events;
using AzurShmup.Stage;

namespace AzurShmup
{
    public class PlayerHitbox : MonoBehaviour
    {
        [SerializeField] private float playerRespawnDelay = 1f;
        // Managers
        private StageManager _stageManager;
        
        private Player spawnedPlayerReference;


        private void Awake()
        {
            _stageManager = StageManager.Instance;
            spawnedPlayerReference = GetComponentInParent<Player>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (spawnedPlayerReference.hittable)
            {
                if (collision.gameObject.tag.Equals("EnemyBullet"))
                {
                    collision.gameObject.GetComponent<Bullet.Bullet>().RemoveFromPlayField();
                    Respawn();
                }
                if (collision.gameObject.tag.Equals("Enemy"))
                {
                    Respawn();
                }
                if (collision.gameObject.tag.Equals("Boss"))
                {
                    Respawn();
                }
            }
        }

        private void Respawn()
        {
            _stageManager.SpawnItems(transform.position, 5, 1, 0, 0);
            spawnedPlayerReference.RespawnPlayer(_stageManager.playerSpawnPoint, playerRespawnDelay);
        }
    }
}
