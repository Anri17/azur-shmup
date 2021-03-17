using System.Collections;
using AzurProject.Bullet;
using AzurProject.Core;
using UnityEngine;

namespace AzurProject.Enemy
{
    public class NormalEnemy : Enemy
    {
        [Header("Base")] [SerializeField] public float health = 80;
        [SerializeField] public int scoreWorth = 100;

        [Header("Items to Drop")] [SerializeField]
        public int powerItems = 0;

        [SerializeField] public int bigPowerItems = 0;
        [SerializeField] public int scoreItems = 5;
        [SerializeField] public int lifeItems = 0;
        [SerializeField] public int bombItems = 0;

        public bool IsInPlayArea { get; set; }

        private GameManager gameManager;
        private WaveManager waveManager;
        private AudioPlayer audioPlayer;

        private bool _isDead = false;

        private void Awake()
        {
            gameManager = GameManager.Instance;
            waveManager = FindObjectOfType<WaveManager>();
            audioPlayer = AudioPlayer.Instance;
        }

        private void CalculateHealth()
        {
            if (health <= 0 && !_isDead)
            {
                waveManager.SpawnItems(
                    transform.position,
                    powerItems,
                    bigPowerItems,
                    scoreItems,
                    lifeItems,
                    bombItems);

                Death();
                _isDead = true;
            }
        }

        private bool _beingHit = false;

        private void TakeDamage(Collider2D playerBullet)
        {
            float damageValue = playerBullet.GetComponent<PlayerBullet>().Damage;
            health -= damageValue;
            gameManager.CurrentPlaySession.Score += 40;

            if (!_beingHit)
            {
                audioPlayer.PlaySfx(audioPlayer.enemyHitSfx);
                _beingHit = true;
                StartCoroutine(PlaySoundEffectsResetCoroutine());
            }

            Destroy(playerBullet.gameObject);
        }

        private void Death()
        {
            Instantiate(gameManager.deathAnimation, transform.position, Quaternion.identity);
            audioPlayer.PlaySfx(audioPlayer.enemyDeathSfx);
            gameManager.CurrentPlaySession.Score += (ulong) scoreWorth;
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("PlayerBullet"))
            {
                TakeDamage(collision);
                CalculateHealth();
            }

            if (collision.CompareTag("PlayArea"))
            {
                IsInPlayArea = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("PlayArea"))
            {
                IsInPlayArea = false;
            }
        }
        

        private IEnumerator PlaySoundEffectsResetCoroutine()
        {
            yield return new WaitForEndOfFrame();
            _beingHit = false;
        }
    }
}