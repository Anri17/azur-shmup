using System.Collections;
using AzurShmup.Core;
using UnityEngine;
using AzurShmup.Stage.Events;
using AzurShmup.Stage;

namespace AzurShmup.Enemy
{
    public class EnemyBasic : EnemyBase
    {
        [Header("Base")]
        [SerializeField] public float health = 80;
        [SerializeField] public int scoreWorth = 100;
        public GameObject deathAnimation;

        [Header("Items to Drop")] [SerializeField]
        public int powerItems = 0;

        [SerializeField] public int bigPowerItems = 0;
        [SerializeField] public int scoreItems = 5;
        [SerializeField] public int lifeItems = 0;

        public bool IsInPlayArea { get; set; }

        private GameManager _gameManager;
        private StageManager _stageManager;
        private AudioPlayer _audioPlayer;

        private bool _isDead = false;

        private void Awake()
        {
            _gameManager = GameManager.Instance;
            _stageManager = StageManager.Instance;
            _audioPlayer = AudioPlayer.Instance;
        }

        private void CalculateHealth()
        {
            if (health <= 0 && !_isDead)
            {
                _stageManager.SpawnItems(
                    transform.position,
                    powerItems,
                    bigPowerItems,
                    scoreItems,
                    lifeItems);

                Death();
                _isDead = true;
            }
        }

        private bool _beingHit = false;

        private void TakeDamage(Collider2D playerBullet)
        {
            // float damageValue = playerBullet.GetComponent<PlayerBullet>().Damage;
            // health -= damageValue;
            _gameManager.CurrentPlaySession.Score += 40;

            if (!_beingHit)
            {
                _audioPlayer.PlaySfx(_audioPlayer.enemyHitSfx);
                _beingHit = true;
                StartCoroutine(PlaySoundEffectsResetCoroutine());
            }

            Destroy(playerBullet.gameObject);
        }

        private void Death()
        {
            Instantiate(deathAnimation, transform.position, Quaternion.identity);
            _audioPlayer.PlaySfx(_audioPlayer.enemyDeathSfx);
            _gameManager.CurrentPlaySession.Score += (ulong) scoreWorth;
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("PlayerBullet"))
            {
                if (IsInPlayArea)
                {
                    TakeDamage(collision);
                }
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