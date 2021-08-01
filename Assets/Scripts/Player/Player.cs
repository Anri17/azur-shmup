using System;
using System.Collections;
using UnityEngine;
using AzurProject.Core;
using UnityEngine.Serialization;

namespace AzurProject
{
    public class Player : MonoBehaviour
    {
        public ParticleSystem deathParticles;
        public GameObject[] barrages;
        public GameObject bombBarrage;
        public GameObject sprites;

        [SerializeField] private GameObject[] powerOrbsLevels;

        [FormerlySerializedAs("_powerLevel")] [SerializeField] private float powerLevel = 0f;
        [FormerlySerializedAs("_lives")] [SerializeField] private int lives = 2;
        [FormerlySerializedAs("_bombs")] [SerializeField] private int bombs = 2;

        Coroutine fireSoundCoroutine;

        public int Lives { get => lives; set => lives = value; }
        public float PowerLevel { get => powerLevel; set => powerLevel = value > 4 ? 4f : value < 0 ? 0f : value; }
        public int Bombs { get => bombs; set => bombs = value; }

        [HideInInspector] public bool canCollectItems = true;
        bool canFire = true;
        GameObject currentBarrage;
        GameObject mainBarrage;
        GameObject currentPowerLevelOrbs;
        int currentPowerLevel;

        PlayerController playerController;

        private AudioPlayer audioPlayer;

        public bool hittable = false;

        private PlayerInput _playerInput;
        
        private void Awake()
        {
            hittable = true;
            canCollectItems = true;
            playerController = GetComponent<PlayerController>();
            _playerInput = GetComponent<PlayerInput>();
            playerController.canMove = true;
            PlayerCanMove();
            audioPlayer = AudioPlayer.Instance;
        }

        private void Start()
        {
            currentPowerLevel = -1;
        }

        private void Update()
        {
            if ((PowerLevel >= 0.0f && PowerLevel < 1.0f) && currentPowerLevel != 0)      // Level 0 Barrage
            {
                Destroy(currentPowerLevelOrbs);
                currentPowerLevelOrbs = Instantiate(powerOrbsLevels[0], sprites.transform);
                SetBarrage(barrages[0]);
                currentPowerLevel = 0;
            }
            else if ((PowerLevel >= 1.0f && PowerLevel < 2.0f) && currentPowerLevel != 1) // Level 1 Barrage
            {
                Destroy(currentPowerLevelOrbs);
                currentPowerLevelOrbs = Instantiate(powerOrbsLevels[1], sprites.transform);
                SetBarrage(barrages[1]);
                currentPowerLevel = 1;
            }
            else if ((PowerLevel >= 2.0f && PowerLevel < 3.0f) && currentPowerLevel != 2) // Level 2 Barrage
            {
                Destroy(currentPowerLevelOrbs);
                currentPowerLevelOrbs = Instantiate(powerOrbsLevels[2], sprites.transform);
                SetBarrage(barrages[2]);
                currentPowerLevel = 2;
            }
            else if ((PowerLevel >= 3.0f && PowerLevel < 4.0f) && currentPowerLevel != 3) // Level 3 Barrage
            {
                Destroy(currentPowerLevelOrbs);
                currentPowerLevelOrbs = Instantiate(powerOrbsLevels[3], sprites.transform);
                SetBarrage(barrages[3]);
                currentPowerLevel = 3;
            }
            else if (PowerLevel == 4 && currentPowerLevel != 4)
            {
                Destroy(currentPowerLevelOrbs);
                currentPowerLevelOrbs = Instantiate(powerOrbsLevels[4], sprites.transform);
                SetBarrage(barrages[4]);
                currentPowerLevel = 4;
            }

            FireBarrage();
        }

        private void SetBarrage(GameObject barrage)
        {
            if (mainBarrage != barrage)
            {
                AssignBarrage(barrage);
                // update barrage if firing
                if (Input.GetButton("Fire1"))
                {
                    Destroy(currentBarrage);
                    SpawnBarrage(mainBarrage);
                }
            }
        }

        private void FireBarrage()
        {
            bool gamePaused = GameManager.Instance.GamePaused;
            bool playingDialogue = DialogueManager.Instance.PlayingDialogue;
            
            if (canFire && !(gamePaused || playingDialogue))
            {
                if (_playerInput.FireInput && currentBarrage == null)
                {
                    SpawnBarrage(mainBarrage);
                    fireSoundCoroutine = StartCoroutine(FireSoundCoroutine());
                }
                else if (!_playerInput.FireInput && currentBarrage != null)
                {
                    StopCoroutine(fireSoundCoroutine);
                    Destroy(currentBarrage);
                    for (int i = 0; i < transform.childCount; i++)
                    {
                        if (transform.GetChild(i).tag.Equals("PlayerBullet"))
                        {
                            // Debug.Log("Found a left over firing thing");
                            Destroy(transform.GetChild(i).gameObject);
                            break;
                        }
                    }
                }
            }
            else if (fireSoundCoroutine != null)
            {
                StopCoroutine(fireSoundCoroutine);
                Destroy(currentBarrage);
            }
        }

        public void AssignBarrage(GameObject barrage)
        {
            mainBarrage = barrage;
        }

        public void AddValues(float powerLevel, int score, int lives, int bombs)
        {
            PowerLevel += powerLevel;
            GameManager.Instance.CurrentPlaySession.Score += (ulong)score;
            Bombs += bombs;
            Lives += lives;
        }

        public void SpawnPlayer(Vector3 position)
        {
            WaveManager.ClearBullets();
            transform.position = position;
            sprites.SetActive(true);
            GetComponent<PlayerController>().canMove = true;
            canFire = true;
            canCollectItems = true;
            PlayerCanMove();
            GameObject.Find("ItemCollectionArea").GetComponent<ItemCollectionArea>().canSucc = true;
        }

        public void PlayerCanMove()
        {
            StartCoroutine(BecomeVulnerable(5f));
            playerController.canMove = true;
        }

        public void RespawnPlayer(Vector3 position, float time)
        {
            Die();
            if (Lives >= 0)
                StartCoroutine(RespawnCoroutine(position, time));
        }

        public void PlayerCantMove()
        {
            playerController.canMove = false;
            hittable = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Collectable") && canCollectItems)
            {
                float powerToAdd = collision.GetComponent<Collectable>().powerLevelWorth;
                int scoreToAdd = collision.GetComponent<Collectable>().scoreWorth;
                int livesToAdd = collision.GetComponent<Collectable>().livesWorth;
                int bombsToAdd = collision.GetComponent<Collectable>().bombsWorth;
                AddValues(powerToAdd, scoreToAdd, livesToAdd, bombsToAdd);
                Destroy(collision.gameObject);
            }
        }

        private void Die()
        {
            Instantiate(GameManager.Instance.deathAnimation, transform.position, Quaternion.identity);
            Lives--;
            PowerLevel = 0;
            Destroy(currentBarrage);
            PlayerCantMove();
            canFire = false;
            canCollectItems = false;
            GameObject.Find("ItemCollectionArea").GetComponent<ItemCollectionArea>().canSucc = false;
            audioPlayer.PlaySfx(audioPlayer.enemyDeathSfx);
            sprites.SetActive(false);
        }

        private void SpawnBarrage(GameObject barrage)
        {
            currentBarrage = Instantiate(barrage, transform.position, mainBarrage.transform.rotation, transform);
        }

        private IEnumerator RespawnCoroutine(Vector3 position, float time)
        {
            yield return new WaitForSeconds(time);
            SpawnPlayer(position);
        }

        private IEnumerator BecomeVulnerable(float timeToWait)
        {
            yield return new WaitForSeconds(timeToWait);
            hittable = true;
        }

        private IEnumerator FireSoundCoroutine()
        {
            while (true)
            {
                audioPlayer.PlaySfx(audioPlayer.playerShootSfx);
                yield return new WaitForSeconds(.05f);
            }
        }
    }
}
