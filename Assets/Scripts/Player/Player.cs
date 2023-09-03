using System.Collections;
using UnityEngine;
using AzurShmup.Core;
using AzurShmup.Stage;

namespace AzurShmup
{
    public class Player : MonoBehaviour
    {
        public ParticleSystem deathParticles;
        public GameObject[] barrages;
        public GameObject bombBarrage;
        public GameObject sprites;
        public GameObject deathAnimation;

        [SerializeField] private GameObject[] powerOrbsLevels;

        [SerializeField] private float powerLevel = 0f;
        [SerializeField] private int lives = 2;
        [SerializeField] private int bombs = 2;

        Coroutine fireSoundCoroutine;

        public int Lives { get => lives; set => lives = value; }
        public float PowerLevel { get => powerLevel; set => powerLevel = value > 4 ? 4f : value < 0 ? 0f : value; }
        public int Bombs { get => bombs; set => bombs = value; }

        [HideInInspector] public bool canCollectItems = true;
        bool canFire = true;
        GameObject current_shot;
        GameObject main_shot;
        GameObject currentPowerLevelOrbs;
        int currentPowerLevel;

        PlayerController playerController;

        private AudioPlayer audioPlayer;

        public bool hittable = false;

        private PlayerInput _playerInput;
        
        private void Awake()
        {
            playerController = GetComponent<PlayerController>();
            _playerInput = GetComponent<PlayerInput>();
            
            hittable = true;
            canCollectItems = true;
            playerController.canMove = true;
            
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
                Shot_Set(barrages[0]);
                currentPowerLevel = 0;
            }
            else if ((PowerLevel >= 1.0f && PowerLevel < 2.0f) && currentPowerLevel != 1) // Level 1 Barrage
            {
                Destroy(currentPowerLevelOrbs);
                currentPowerLevelOrbs = Instantiate(powerOrbsLevels[1], sprites.transform);
                Shot_Set(barrages[1]);
                currentPowerLevel = 1;
            }
            else if ((PowerLevel >= 2.0f && PowerLevel < 3.0f) && currentPowerLevel != 2) // Level 2 Barrage
            {
                Destroy(currentPowerLevelOrbs);
                currentPowerLevelOrbs = Instantiate(powerOrbsLevels[2], sprites.transform);
                Shot_Set(barrages[2]);
                currentPowerLevel = 2;
            }
            else if ((PowerLevel >= 3.0f && PowerLevel < 4.0f) && currentPowerLevel != 3) // Level 3 Barrage
            {
                Destroy(currentPowerLevelOrbs);
                currentPowerLevelOrbs = Instantiate(powerOrbsLevels[3], sprites.transform);
                Shot_Set(barrages[3]);
                currentPowerLevel = 3;
            }
            else if (PowerLevel == 4 && currentPowerLevel != 4)
            {
                Destroy(currentPowerLevelOrbs);
                currentPowerLevelOrbs = Instantiate(powerOrbsLevels[4], sprites.transform);
                Shot_Set(barrages[4]);
                currentPowerLevel = 4;
            }

            Shot_Fire();
        }

        private void Shot_Set(GameObject shot)
        {
            if (main_shot != shot)
            {
                main_shot = shot;
                // update barrage if firing
                if (Input.GetButton("Fire1"))
                {
                    Destroy(current_shot);
                    SpawnBarrage(main_shot);
                }
            }
        }

        private void Shot_Fire()
        {
            bool gamePaused = GameManager.Instance.GamePaused;
            bool playingDialogue = DialogueManager.Instance.PlayingDialogue;
            
            if (canFire && !(gamePaused || playingDialogue))
            {
                if (_playerInput.FireInput && current_shot == null)
                {
                    SpawnBarrage(main_shot);
                    fireSoundCoroutine = StartCoroutine(FireSoundCoroutine());
                }
                else if (!_playerInput.FireInput && current_shot != null)
                {
                    StopCoroutine(fireSoundCoroutine);
                    Destroy(current_shot);
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
                Destroy(current_shot);
            }
        }

        public void SpawnPlayer(Vector3 position)
        {
            // BulletManager.Instance.AddAllActiveBulletsToPool();
            // WaveManager.RemoveBulletsFromPlayField();
            transform.position = position;
            sprites.SetActive(true);
            playerController.canMove = true;
            canFire = true;
            canCollectItems = true;
            StartCoroutine(IvulnerabilityCoroutine(5f));
            playerController.canMove = true;
            GameObject.Find("ItemCollectionArea").GetComponent<ItemCollectionArea>().canSucc = true;
        }

        public void RespawnPlayer(Vector3 position, float time)
        {
            KillPlayer();
            if (Lives >= 0)
                StartCoroutine(RespawnCoroutine(position, time));
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Collectable") && canCollectItems)
            {
                float powerToAdd = collision.GetComponent<Collectable>().powerLevelWorth;
                int scoreToAdd = collision.GetComponent<Collectable>().scoreWorth;
                int livesToAdd = collision.GetComponent<Collectable>().livesWorth;
                int bombsToAdd = collision.GetComponent<Collectable>().bombsWorth;
                
                PowerLevel += powerToAdd;
                GameManager.Instance.CurrentPlaySession.Score += (ulong)scoreToAdd;
                Bombs += bombsToAdd;
                Lives += livesToAdd;
                
                Destroy(collision.gameObject);
            }
        }

        private void KillPlayer()
        {
            Instantiate(deathAnimation, transform.position, Quaternion.identity);
            Lives--;
            PowerLevel = 0;
            Destroy(current_shot);
            playerController.canMove = false;
            hittable = false;
            canFire = false;
            canCollectItems = false;
            GameObject.Find("ItemCollectionArea").GetComponent<ItemCollectionArea>().canSucc = false;
            audioPlayer.PlaySfx(audioPlayer.playerDeathSfx);
            sprites.SetActive(false);
        }

        private void SpawnBarrage(GameObject barrage)
        {
            current_shot = Instantiate(barrage, transform.position, main_shot.transform.rotation, transform);
        }

        private IEnumerator RespawnCoroutine(Vector3 position, float time)
        {
            yield return new WaitForSeconds(time);
            SpawnPlayer(position);
        }

        private IEnumerator IvulnerabilityCoroutine(float timeToWait)
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
