using System;
using System.Collections;
using System.Collections.Generic;
using AzurProject.Bullet;
using UnityEngine;
using AzurProject.Core;

namespace AzurProject
{
    public class Boss : MonoBehaviour
    {
        public int StageCount { get; set; }
        public float CurrentMaxHealth { get; set; }
        public float CurrentHealth { get; set; }
        public float CurrentDeathTimer { get; set; }

        [SerializeField] private SpellAttack[] spellAttacks;

        private int _spellAttackIndex = 0;
        private GameObject _currentSpellAttackGameObject;
        private SpellAttack _currentSpellAttack;
        private bool _hittable = false;
        
        private WaveManager _waveManager;
        private AudioPlayer _audioPlayer;

        private bool _isDead = false;
        
        public void StartBoss()
        {
            Next();
        }

        public void MoveToDefaultPosition()
        {
            StartCoroutine(MoveToPositionCoroutine(GameManager.DEFAULT_BOSS_POSITION, 1));
        }

        private void Awake()
        {
            _waveManager = GameObject.Find("WaveManager").GetComponent<WaveManager>();
            _audioPlayer = AudioPlayer.Instance;
        }

        private void Start()
        {
            StageCount = spellAttacks.Length - 1;
        }

        private bool _beingHit = false;
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if ((collision.CompareTag("PlayerBullet") || collision.CompareTag("PlayerLazer")) && _hittable)
            {
                GameManager.Instance.CurrentPlaySession.Score += 80;
                CurrentHealth -= collision.GetComponent<PlayerBullet>().Damage;
                Destroy(collision.gameObject);

                if (!_beingHit)
                {
                    _audioPlayer.PlaySfx(_audioPlayer.enemyHitSfx);
                    _beingHit = true;
                    StartCoroutine(PlaySoundEffectsResetCoroutine());
                }
                
                if (CurrentHealth <= 0 && !_isDead)
                {
                    _isDead = true;
                    GameManager.Instance.CurrentPlaySession.Score += (ulong)_currentSpellAttack.scoreWorth;
                    KillBoss();
                }
            }
        }

        private IEnumerator PlaySoundEffectsResetCoroutine()
        {
            yield return new WaitForEndOfFrame();
            _beingHit = false;
        }
        
        private void KillBoss()
        {
            _audioPlayer.PlaySfx(_audioPlayer.enemyDeathSfx);
            WaveManager.ClearBullets();
            StopAllCoroutines();
            DropItems(_currentSpellAttack.powerItems, _currentSpellAttack.bigPowerItems, _currentSpellAttack.scoreItems, _currentSpellAttack.lifeItems, _currentSpellAttack.bombItems);
            _hittable = false;
            StageCount--;
            _spellAttackIndex++;
            if (_spellAttackIndex < spellAttacks.Length)
            {
                MoveToDefaultPosition();
                DestroyCurrentStage();
                Next();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void DropItems(int powerItemQuantity, int bigPowerItemQuantity, int scoreItemQuantity, int lifeItemQuantity, int bombItemQuantity)
        {
            _waveManager.SpawnItems(transform.position, powerItemQuantity, bigPowerItemQuantity, scoreItemQuantity, lifeItemQuantity, bombItemQuantity);
        }

        private IEnumerator MoveToPositionCoroutine(Vector3 destination, float timeToMove)
        {
            Vector3 currentPos = transform.position;
            float t = 0f;
            while (t < 1)
            {
                t += Time.deltaTime / timeToMove;
                transform.position = Vector3.Lerp(currentPos, destination, t);
                yield return null;
            }
        }

        private IEnumerator CountDownDeathTimer()
        {
            while (CurrentDeathTimer > 0)
            {
                CurrentDeathTimer--;
                yield return new WaitForSeconds(1f);
            }
            KillBoss();
        }

        private IEnumerator FillHealthBar(float timeToFill)
        {
            CurrentMaxHealth = 100f;
            float t = 0f;
            while (t < 1)
            {
                t += Time.deltaTime / timeToFill;
                CurrentHealth = t * CurrentMaxHealth;
                yield return null;
            }
        }

        private void Next()
        {
            _isDead = false;
            MoveToDefaultPosition();
            StartCoroutine(NextCoroutine());
        }

        private IEnumerator NextCoroutine()
        {
            float timeToWait = spellAttacks[_spellAttackIndex].chargeTime;
            StartCoroutine(FillHealthBar(timeToWait));
            yield return new WaitForSeconds(timeToWait);
            SetStage(_spellAttackIndex);
        }

        private void SetStage(int stageIndex)
        {
            _currentSpellAttack = spellAttacks[stageIndex];
            CurrentMaxHealth = _currentSpellAttack.health;
            CurrentHealth = _currentSpellAttack.health;
            CurrentDeathTimer = _currentSpellAttack.deathTimer;
            _currentSpellAttackGameObject = Instantiate(_currentSpellAttack.spellAttack, transform);

            _hittable = true;

            StartCoroutine(CountDownDeathTimer());
        }

        private void DestroyCurrentStage()
        {
            Destroy(_currentSpellAttackGameObject);
        }
    }
}
