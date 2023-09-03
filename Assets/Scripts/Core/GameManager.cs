using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using AzurShmup.Stage;
using TMPro;

namespace AzurShmup.Core
{
    public class GameManager : SingletonPersistent<GameManager>
    {
        public static readonly Vector2 GAME_FIELD_TOP_LEFT = new Vector2(0.0f, 21.0f);
        public static readonly Vector2 GAME_FIELD_TOP_RIGHT = new Vector2(18.0f, 21.0f);
        public static readonly Vector2 GAME_FIELD_BOTTOM_LEFT = new Vector2(0.0f, 0.0f);
        public static readonly Vector2 GAME_FIELD_BOTTOM_RIGHT = new Vector2(18.0f, 0.0f);
        public static readonly Vector2 GAME_FIELD_CENTER = new Vector2(9.0f, 10.5f);
        public static readonly Vector2 DEFAULT_BOSS_POSITION = new Vector2(9.0f, 15.0f);

        public AudioPlayer AudioPlayer { get; private set; }
        public SettingsManager SettingsManager { get; private set; }
        public SceneManager SceneManager { get; private set; }


        [Header("Game Assets")]
        [Header("Fonts")]
        public Font diogenesFont;

        [Header("Stage Packs")]
        public StagePack easyStagepack;
        public StagePack normalStagepack;
        public StagePack hardStagepack;
        public StagePack insaneStagepack;
        public StagePack extraStagePack;
        public StagePack debugStagePack;

        [Header("Players")]
        public GameObject playerAPrefab;
        public GameObject playerBPrefab;

        [Header("Items")]
        public GameObject powerItemPrefab;
        public GameObject bigPowerItemPrefab;
        public GameObject scoreItemPrefab;
        public GameObject lifeItemPrefab;

        // Current play session
        public DifficultyType DifficultyType { get; set; }
        public PlayerShotType PlayerShotType { get; set; }
        public PlaySession CurrentPlaySession { get; set; }
        public StagePack CurrentStagePack { get; set; }
        public bool GamePaused { get; set; }

        public Player Player { get; set; }

        private void Awake()
        {
            MakeSingleton();
            
            AudioPlayer = AudioPlayer.Instance;
            SettingsManager = SettingsManager.Instance;
            SceneManager = SceneManager.Instance;
        }

        // Game Start
        public void Start()
        {
            SettingsManager.LoadSettings();
            AudioPlayer.SetVolumeLevels(
                SettingsManager.Data.masterVolumeLevel,
                SettingsManager.Data.musicVolumeLevel,
                SettingsManager.Data.effectsVolumeLevel);
        }

        // Game End
        public void CloseGame()
        {
            // Save Settings
            SettingsManager.SaveSettings();
            Application.Quit();
        }

        public void ResetPlaySession()
        {
            CurrentPlaySession.Score = 0;
        }

        public static void LockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public static void UnlockCursor()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        public void SetStagePack()
        {
            switch (DifficultyType)
            {
                case DifficultyType.EASY:
                    CurrentStagePack = easyStagepack;
                    break;
                case DifficultyType.NORMAL:
                    CurrentStagePack = normalStagepack;
                    break;
                case DifficultyType.HARD:
                    CurrentStagePack = hardStagepack;
                    break;
                case DifficultyType.INSANE:
                    CurrentStagePack = insaneStagepack;
                    break;
                case DifficultyType.EXTRA:
                    CurrentStagePack = extraStagePack;
                    break;
                case DifficultyType.DEBUG:
                    CurrentStagePack = debugStagePack;
                    break;
                default:
                    break;
            }
        }

        public void SetStagePack(DifficultyType difficultyType)
        {
            DifficultyType = difficultyType;

            switch (difficultyType)
            {
                case DifficultyType.EASY:
                    CurrentStagePack = easyStagepack;
                    break;
                case DifficultyType.NORMAL:
                    CurrentStagePack = normalStagepack;
                    break;
                case DifficultyType.HARD:
                    CurrentStagePack = hardStagepack;
                    break;
                case DifficultyType.INSANE:
                    CurrentStagePack = insaneStagepack;
                    break;
                case DifficultyType.EXTRA:
                    CurrentStagePack = extraStagePack;
                    break;
                case DifficultyType.DEBUG:
                    CurrentStagePack = debugStagePack;
                    break;
                default:
                    break;
            }
        }

        public void SetStagePack(StagePack stagePack)
        {
            DifficultyType = stagePack.DifficultyType;
            CurrentStagePack = stagePack;
        }

        public void CreatePlaySession()
        {
            // This is placeholder
            // TODO: Aks the player for input about the given data
            CurrentPlaySession = new PlaySession()
            {
                PlayerName = "Guest",
                Score = 0,
                DifficultyType = DifficultyType,
                PlayerType = PlayerShotType
            };
        }
        
        public void CreatePlaySession(DifficultyType difficultyType)
        {
            CurrentPlaySession = new PlaySession()
            {
                PlayerName = "Guest",
                Score = 0,
                DifficultyType = difficultyType,
            };
        }

        public void SavePlaySession(PlaySession playSession)
        {
            // TODO: Save replay. Set Current Replay to null for now.
            throw new NotImplementedException();
        }

        public void EndPlaySession()
        {
            CurrentPlaySession = null;
        }
    }
}