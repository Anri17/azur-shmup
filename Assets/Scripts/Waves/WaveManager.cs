using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using AzurProject.Core;
using SceneManager = AzurProject.Core.SceneManager;

namespace AzurProject
{
    public class WaveManager : MonoBehaviour
    {
        [Header("Managers")] public StageManager stageManager;
        public BossManager bossManager;
        public DialogueManager dialogueManager;
        public UIManager uiManager;

        [Header("Level Settings")] public StageBanner banner;
        public Vector3 playerSpawnPoint = new Vector3(9, 4, 0);
        public MusicClip stageMusicClip;
        public MusicClip bossMusicClip;

        private GameManager gameManager;
        private SceneManager _sceneManager;
        private AudioPlayer musicPlayer;
        private UIUpdate uiUpdate;

        Player player;
        GameObject spawnedBanner;

        [Header("Waves")] public Wave[] waves;
        [HideInInspector] public GameObject[] spawnedWaves;

        void Awake()
        {
            gameManager = GameManager.Instance;
            _sceneManager = SceneManager.Instance;
            musicPlayer = AudioPlayer.Instance;
            uiUpdate = FindObjectOfType<UIUpdate>();
        }

        public void LoadWaves(Wave[] waves)
        {
            this.waves = waves;
            spawnedWaves = new GameObject[waves.Length];
        }

        public void LoadBanner(StageBanner banner)
        {
            this.banner = banner;
        }

        public void LoadMusic(MusicClip stageMusic, MusicClip bossMusic)
        {
            stageMusicClip = stageMusic;
            bossMusicClip = bossMusic;
        }

        public void UnloadWaves()
        {
            foreach (var wave in spawnedWaves)
            {
                Destroy(wave);
            }

            spawnedWaves = null;
            waves = null;
        }

        public void UnloadStartFlag()
        {
            Destroy(spawnedBanner);
            spawnedBanner = null;
            banner = null;
        }

        public void UnloadMusic()
        {
            musicPlayer.StopMusic();
            stageMusicClip = null;
            bossMusicClip = null;
        }

        // used to be start, now it only starts after the stage manager
        public void StartWaveManager()
        {
            SpawnPlayer(playerSpawnPoint); // Spawn the Player

            if (gameManager.CurrentDifficultyPack != null)
            {
                gameManager.CreateNewReplay();
            }
            else
            {
                gameManager.CreateNewReplay(DifficultyTypes.UNDEFINED);
            }

            uiUpdate.StartUIUpdate();

            Time.timeScale = 1; // Allows the time and objects to move
            GameManager.LockCursor(); // Lock the cursor during play time
            player = gameManager.spawnedPlayer.GetComponent<Player>();

            // Load Stage 1
            if (stageManager.stages.Length > 0)
            {
                stageManager.LoadStage(stageManager.stages[0]);
                PlayStage(); // Start Level
            }
        }

        public void SpawnPlayer(Vector3 position)
        {
            if (gameManager.spawnedPlayer == null)
            {
                gameManager.spawnedPlayer = Instantiate(gameManager.player, position, Quaternion.identity);
            }
            else
            {
                player.SpawnPlayer(playerSpawnPoint);
            }
        }

        public static void ClearBullets()
        {
            foreach (GameObject item in GameObject.FindGameObjectsWithTag("EnemyBullet"))
            {
                Destroy(item);
            }
        }

        public static void ClearEnemies()
        {
            foreach (GameObject item in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                Destroy(item);
            }
        }

        public void PlayBossMusic(float waitTime)
        {
            if (bossMusicClip != null)
            {
                musicPlayer.StopMusic();
                StartCoroutine(WaitSeconds(() =>
                    {
                        musicPlayer.PlayMusic(bossMusicClip.musicClip, bossMusicClip.loopStart);
                        uiManager.DisplayCurrentBMGText(bossMusicClip.name);
                    },
                    waitTime));
            }
        }

        public void SpawnItems(Vector3 position, int powerItemCount, int bigPowerItemCount, int scoreItemCount,
            int lifeItemCount, int bombItemCount)
        {
            for (int i = 0; i < powerItemCount; i++)
            {
                Vector3 pos = position +
                              new Vector3(UnityEngine.Random.Range(-2f, 2), UnityEngine.Random.Range(-0.6f, 2), 0);
                GameObject collectable = Instantiate(gameManager.powerItem, position, Quaternion.identity);
                Collectable collectableScript = collectable.GetComponent<Collectable>();
                collectableScript.Move(pos, 8, 0.6f);
            }

            for (int i = 0; i < bigPowerItemCount; i++)
            {
                Vector3 pos = position +
                              new Vector3(UnityEngine.Random.Range(-2f, 2), UnityEngine.Random.Range(-0.6f, 2), 0);
                GameObject collectable = Instantiate(gameManager.bigPowerItem, position, Quaternion.identity);
                Collectable collectableScript = collectable.GetComponent<Collectable>();
                collectableScript.Move(pos, 8, 0.6f);
            }

            for (int i = 0; i < scoreItemCount; i++)
            {
                Vector3 pos = position +
                              new Vector3(UnityEngine.Random.Range(-2f, 2), UnityEngine.Random.Range(-0.6f, 2), 0);
                GameObject collectable = Instantiate(gameManager.scoreItem, position, Quaternion.identity);
                Collectable collectableScript = collectable.GetComponent<Collectable>();
                collectableScript.Move(pos, 8, 0.6f);
            }

            for (int i = 0; i < lifeItemCount; i++)
            {
                Vector3 pos = position +
                              new Vector3(UnityEngine.Random.Range(-2f, 2), UnityEngine.Random.Range(-0.6f, 2), 0);
                GameObject collectable = Instantiate(gameManager.lifeItem, position, Quaternion.identity);
                Collectable collectableScript = collectable.GetComponent<Collectable>();
                collectableScript.Move(pos, 8, 0.6f);
            }

            for (int i = 0; i < bombItemCount; i++)
            {
                Vector3 pos = position +
                              new Vector3(UnityEngine.Random.Range(-2f, 2), UnityEngine.Random.Range(-0.6f, 2), 0);
                GameObject collectable = Instantiate(gameManager.bombItem, position, Quaternion.identity);
                Collectable collectableScript = collectable.GetComponent<Collectable>();
                collectableScript.Move(pos, 8, 0.6f);
            }
        }

        private IEnumerator WaitSeconds(Action methodToRun, float secondsToWait)
        {
            yield return new WaitForSeconds(secondsToWait);
            methodToRun();
        }

        [Header("Wave Manager")] Coroutine level;

        private void PlayStage()
        {
            level = StartCoroutine(PlayStageCoroutine());
        }

        private void Update()
        {
            if (uiManager.reachedEnd)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    stageManager.stageIndex++;
                    if (stageManager.stages.Length > stageManager.stageIndex)
                    {
                        stageManager.UnloadStage();
                        stageManager.LoadStage(stageManager.stages[stageManager.stageIndex]);
                        uiManager.ResetStage();
                        PlayStage();
                    }
                    else
                    {
                        _sceneManager.LoadScene((int) SceneIndex.MENU_SCENE);
                    }
                }
            }

            if (gameManager.spawnedPlayer.GetComponent<Player>().Lives < 0)
            {
                gameManager.spawnedPlayer.GetComponent<Player>().Lives = 0;
                uiManager.GameOver();
                StopCoroutine(level);
            }
        }

        private IEnumerator PlayStageCoroutine()
        {
            musicPlayer.PlayMusic(stageMusicClip.musicClip, stageMusicClip.loopStart); // Plays the Music

            uiManager.DisplayCurrentBMGText(stageMusicClip.name);

            if (banner != null)
            {
                if (spawnedBanner != null)
                {
                    Destroy(spawnedBanner);
                }

                spawnedBanner = Instantiate(banner.bannerObject, GameManager.GAME_FIELD_CENTER, Quaternion.identity);
            }
            
            yield return new WaitForSeconds(banner.duration);

            for (int waveIndex = 0; waveIndex < waves.Length; waveIndex++)
            {
                if (waves[waveIndex] is EnemyWave)
                {
                    EnemyWave enemyWave = (EnemyWave) waves[waveIndex];

                    spawnedWaves[waveIndex] = Instantiate(enemyWave.WaveGameObject, transform);
                    yield return new WaitForSeconds(enemyWave.LifeCycle);
                    continue;
                }

                if (waves[waveIndex] is BossWave)
                {
                    ClearBullets();
                    ClearEnemies();

                    BossWave bossWave = (BossWave) waves[waveIndex];

                    Vector2 bossSpawnPosition = new Vector2(18, 30);

                    bossManager.spawnedBoss = Instantiate(bossWave.Boss, bossSpawnPosition,
                        Quaternion.identity, transform);

                    if (bossWave.Dialogue1 != null)
                    {
                        dialogueManager.StartDialogue(bossWave.Dialogue1);
                        yield return new WaitUntil(() => dialogueManager.dialogueEnded);
                    }

                    bossManager.DisplayBossInterface();
                    bossManager.spawnedBoss.GetComponent<Boss>().StartBoss();

                    yield return new WaitUntil(() => bossManager.spawnedBoss == null);

                    bossManager.DeactivateBossInterface();
                    ClearBullets();
                    ClearEnemies();

                    if (bossWave.Dialogue2 != null)
                    {
                        dialogueManager.StartDialogue(bossWave.Dialogue2);
                        yield return new WaitUntil(() => dialogueManager.dialogueEnded);
                    }

                    yield return new WaitForSeconds(bossWave.EndDelay);
                }
            }

            musicPlayer.StopMusic();
            ClearBullets();
            ClearEnemies();
            uiManager.EndStage();
        }
    }
}