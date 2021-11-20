using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AzurProject
{
    public class PlayerHitbox : MonoBehaviour
    {
        [SerializeField] float playerRespawnDelay = 1f;

        WaveManager waveManager;
        Player player;

        private void Awake()
        {
            waveManager = GameObject.Find("WaveManager").GetComponent<WaveManager>();
            player = GetComponentInParent<Player>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (player.hittable)
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
            waveManager.SpawnItems(transform.position, 5, 1, 0, 0, 0);
            player.RespawnPlayer(waveManager.playerSpawnPoint, playerRespawnDelay);
        }
    }
}
