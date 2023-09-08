using UnityEngine;
using AzurShmup.Core;
using AzurShmup.Stage.Events;
using System;
using System.Collections.Generic;
using System.Collections;
using UnityEditor.SceneManagement;

namespace AzurShmup.Stage
{
    public class StageManager : Singleton<StageManager>
    {
        // Persistent Managers
        private GameManager _gameManager;
        private AudioPlayer _audioPlayer;
        private SceneManager _sceneManager;

        // Stage Managers
        private DialogueManager _dialogueManager;
        private UIManager _uiManager;
        private BossManager _bossManager;
        private BackgroundManager _backgroundManager;

        //[HideInInspector] public GameObject spawnedPlayer;
        //[HideInInspector] public GameObject deathAnimation;

        [Header("Stages")]
        public Stage[] stages;
        public int stageIndex = 0;
        public Vector3 playerSpawnPoint = new Vector3(9, 4, 0);

        public StageEvent[] Events { get; set; }
        public bool HasStageEnded { get; set; }
        public Coroutine StageEventsCoroutine;

        private void Awake()
        {
            MakeSingleton();

            _gameManager = GameManager.Instance;
            _sceneManager = _gameManager.SceneManager;
            _audioPlayer = _gameManager.AudioPlayer;

            _bossManager = BossManager.Instance;
            _dialogueManager = DialogueManager.Instance;
            _uiManager = UIManager.Instance;
            _backgroundManager = BackgroundManager.Instance;

            HasStageEnded = false;
        }

        private void Start()
        {
            if (_gameManager.CurrentStagePack != null)
            {
                // Load the stages of the chosen difficulty for use
                stages = new Stage[_gameManager.CurrentStagePack.stages.Length];
                stages = _gameManager.CurrentStagePack.stages;
            }

            // Spawn the Player
            if (_gameManager.Player == null)
            {
                // TODO: change _gameManager.playerA to spawn player type defined by user on main menu
                _gameManager.Player = Instantiate(_gameManager.playerAPrefab, playerSpawnPoint, Quaternion.identity).GetComponent<Player>();
            }
            else
            {
                _gameManager.Player.SpawnPlayer(playerSpawnPoint);
            }


            // Start Event Manager
            if (_gameManager.CurrentStagePack != null)
            {
                _gameManager.CreatePlaySession();
            }
            else
            {
                _gameManager.CreatePlaySession(DifficultyType.UNDEFINED);
            }


            // Unpause game
            Time.timeScale = 1; // Allows the time and objects to move
            GameManager.LockCursor(); // Lock the cursor during play time


            // Load Stage 1, if there are any stages to play.
            if (stages.Length > 0)
            {
                LoadStageEvents(stages[0]);
                StageEventsCoroutine = StartCoroutine(PlayStageEventsCoroutine()); // Play Stage Events Coroutine
            }
        }

        public void LoadStageEvents(Stage stage)
        {
            // TODO: Start the stage and run it when the scene loads.
            // TODO: Write cheats for debugging, so I can easily Load a new stage if I want to.
            // TODO: When the stage scene is loaded from the main menu, there should be a struct with all the data to start the game.
            //       If that data doesn't exist, leave the control to the developer via the cheat menu.
            // TODO: Implement a system where it is possible to load a stage at the middle of it's runtime.
            //       Doing this should make the music start at the time the stage starts, the background should load at the correct state, the waves should start spawning at the set start time
            // TODO: Each stage will play by a timer, set and updated in StageManager. This time will define the start of each "event" (background movement and state, enemy spawning, dialogue queues, boss spawning, etc).

            // Load Events
            Events = stage.Events;
        }

        

        private void Update()
        {
            if (InputManager.GetKeyDown(InputManager.Pause) && _uiManager.CanOpenMenu)
            {
                _uiManager.TogglePauseMenu();
            }

            if (HasStageEnded)
            {
                if (InputManager.GetKeyDown(InputManager.Return) || InputManager.GetKeyDown(InputManager.Shoot))
                {
                    _audioPlayer.FadeOutMusic();

                    stageIndex++;
                    if (stages.Length > stageIndex)
                    {
                        // Unload Current Stage
                        _backgroundManager.UnloadBackground(); // Unload Background 
                        // Unload Events
                        Events = null;


                        // Load Next Stage
                        _uiManager.HideFinalInfo();
                        LoadStageEvents(stages[stageIndex]);
                        // Unpause game
                        Time.timeScale = 1;
                        GameManager.LockCursor();
                        // Play Stage Events
                        StageEventsCoroutine = StartCoroutine(PlayStageEventsCoroutine());
                    }
                    else
                    {
                        _sceneManager.LoadScene(SceneIndex.MENU_SCENE);
                    }
                }
            }

            if (_gameManager.Player.Lives < 0)
            {
                _gameManager.Player.Lives = 0;
                GameOver();
                if (StageEventsCoroutine != null)
                {
                    StopCoroutine(StageEventsCoroutine);
                }
            }
        }

        private IEnumerator PlayStageEventsCoroutine()
        {
            HasStageEnded = false;

            _audioPlayer.StopMusic();

            for (int eventIndex = 0; eventIndex < Events.Length; eventIndex++)
            {
                if (Events[eventIndex] is BossEvent)
                {
                    ClearEnemies();

                    BossEvent currentEvent = (BossEvent)Events[eventIndex];

                    // Spawn Boss if it wasn't already spawned by other events
                    if (_bossManager.spawnedBoss == null)
                    {
                        _bossManager.spawnedBoss = Instantiate(currentEvent.bossGameObject, currentEvent.spawnPosition,
                            Quaternion.identity, transform);
                    }

                    // Display Interface
                    _bossManager.DisplayBossInterface();
                    _bossManager.spawnedBoss.GetComponent<Boss>().StartBoss();

                    // Wait for the boss phase to end
                    yield return new WaitUntil(() => _bossManager.spawnedBoss == null);

                    _bossManager.DeactivateBossInterface();
                    ClearBullets();
                    ClearEnemies();
                }

                if (Events[eventIndex] is PlayMusicEvent)
                {
                    PlayMusicEvent currentEvent = (PlayMusicEvent)Events[eventIndex];

                    // Play Music
                    _audioPlayer.StopMusic();
                    yield return new WaitForSeconds(currentEvent.Delay);

                    _audioPlayer.PlayMusic(currentEvent.Clip);
                    _uiManager.DisplayCurrentBMGText(currentEvent.Clip.musicTitle);

                    yield return new WaitForSeconds(currentEvent.Duration);
                }

                if (Events[eventIndex] is DialogueEvent)
                {
                    DialogueEvent currentEvent = (DialogueEvent)Events[eventIndex];

                    yield return new WaitForSeconds(currentEvent.Delay);

                    // Play Dialogue
                    _dialogueManager.StartDialogue(currentEvent.Dialogue);
                    yield return new WaitUntil(() => _dialogueManager.dialogueEnded);

                    yield return new WaitForSeconds(currentEvent.Duration);
                }

                if (Events[eventIndex] is WaveEvent)
                {
                    WaveEvent currentEvent = (WaveEvent)Events[eventIndex];

                    yield return new WaitForSeconds(currentEvent.Delay);

                    // Play Wave
                    Instantiate(currentEvent.Wave, Vector3.zero, Quaternion.identity);

                    yield return new WaitForSeconds(currentEvent.Duration);
                }

                if (Events[eventIndex] is LoadBackgroundEvent)
                {
                    LoadBackgroundEvent currentEvent = (LoadBackgroundEvent)Events[eventIndex];

                    yield return new WaitForSeconds(currentEvent.Delay);

                    _backgroundManager.LoadBackground(currentEvent.Background); // Load Background

                    yield return new WaitForSeconds(currentEvent.Duration);
                }
            }

            // End Stage
            ClearBullets();
            ClearEnemies();
            // Calculate Final Score Information
            ulong score = _gameManager.CurrentPlaySession.Score;
            ulong clearBonus = 1000000;
            int lives = _gameManager.Player.Lives;
            ulong lifeBonus = (ulong)lives * 10000;
            ulong finalScore = lifeBonus + score + clearBonus;
            _uiManager.DisplayFinalScoreInformation(score, clearBonus, lives, lifeBonus, finalScore);
            GameManager.Instance.CurrentPlaySession.Score = finalScore; // Save Final Score. Score is comulative across stages.
            // Pause Game
            GameManager.UnlockCursor();
            Time.timeScale = 1;
            HasStageEnded = true;
        }

        public void SpawnItems(Vector3 position, int powerItemCount, int bigPowerItemCount, int scoreItemCount,
            int lifeItemCount)
        {
            // Spawn Power Items
            for (int i = 0; i < powerItemCount; i++)
            {
                Vector3 pos = position +
                              new Vector3(UnityEngine.Random.Range(-2f, 2), UnityEngine.Random.Range(-0.6f, 2), 0);
                GameObject collectable = Instantiate(_gameManager.powerItemPrefab, position, Quaternion.identity);
                Collectable collectableScript = collectable.GetComponent<Collectable>();
                collectableScript.Move(pos, 8, 0.6f);
            }

            // Spawn Big Power Items
            for (int i = 0; i < bigPowerItemCount; i++)
            {
                Vector3 pos = position +
                              new Vector3(UnityEngine.Random.Range(-2f, 2), UnityEngine.Random.Range(-0.6f, 2), 0);
                GameObject collectable = Instantiate(_gameManager.bigPowerItemPrefab, position, Quaternion.identity);
                Collectable collectableScript = collectable.GetComponent<Collectable>();
                collectableScript.Move(pos, 8, 0.6f);
            }

            // Spawn Score Items
            for (int i = 0; i < scoreItemCount; i++)
            {
                Vector3 pos = position +
                              new Vector3(UnityEngine.Random.Range(-2f, 2), UnityEngine.Random.Range(-0.6f, 2), 0);
                GameObject collectable = Instantiate(_gameManager.scoreItemPrefab, position, Quaternion.identity);
                Collectable collectableScript = collectable.GetComponent<Collectable>();
                collectableScript.Move(pos, 8, 0.6f);
            }

            // Spawn Life Items
            for (int i = 0; i < lifeItemCount; i++)
            {
                Vector3 pos = position +
                              new Vector3(UnityEngine.Random.Range(-2f, 2), UnityEngine.Random.Range(-0.6f, 2), 0);
                GameObject collectable = Instantiate(_gameManager.lifeItemPrefab, position, Quaternion.identity);
                Collectable collectableScript = collectable.GetComponent<Collectable>();
                collectableScript.Move(pos, 8, 0.6f);
            }
        }

        public void ClearBullets()
        {
            foreach (GameObject item in GameObject.FindGameObjectsWithTag("EnemyBullet"))
            {
                item.GetComponent<Bullet.Bullet>().RemoveFromPlayField();
            }
        }

        public void ClearEnemies()
        {
            foreach (GameObject item in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                Destroy(item);
            }
        }

        private IEnumerator WaitSeconds(Action actionMethod, float secondsToWait)
        {
            yield return new WaitForSeconds(secondsToWait);
            actionMethod();
        }

        public void GameOver()
        {
            StartCoroutine(GameOverCoroutine());
        }

        private IEnumerator GameOverCoroutine()
        {
            _uiManager.DisplayGameOverScreen();

            GameManager.UnlockCursor();
            Time.timeScale = 1;
            _uiManager.CanOpenMenu = false;
            yield return new WaitForSeconds(5f);
            _sceneManager.LoadScene(SceneIndex.MENU_SCENE);
        }
    }

}