using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace AzurProject.Core
{
    public class GameManager : MonoBehaviour
    {
        public static readonly Vector2 GAME_FIELD_TOP_LEFT = new Vector2(0.0f, 21.0f);
        public static readonly Vector2 GAME_FIELD_TOP_RIGHT = new Vector2(18.0f, 21.0f);
        public static readonly Vector2 GAME_FIELD_BOTTOM_LEFT = new Vector2(0.0f, 0.0f);
        public static readonly Vector2 GAME_FIELD_BOTTOM_RIGHT = new Vector2(18.0f, 0.0f);
        public static readonly Vector2 GAME_FIELD_CENTER = new Vector2(9.0f, 10.5f);
        public static readonly Vector2 DEFAULT_BOSS_POSITION = new Vector2(9.0f, 15.0f);

        // difficulty packs
        public DifficultyPack easyDifficultyPack;
        public DifficultyPack normalDifficultyPack;
        public DifficultyPack hardDifficultyPack;
        public DifficultyPack insaneDifficultyPack;

        // Game Assets
        public GameObject player;
        public GameObject powerItem;
        public GameObject bigPowerItem;
        public GameObject scoreItem;
        public GameObject lifeItem;
        public GameObject bombItem;
        public GameObject spawnedPlayer;

        public GameObject deathAnimation;

        // replay
        public static GameManager Instance { get; private set; }
        public PlaySession CurrentPlaySession { get; set; }
        public DifficultyPack CurrentDifficultyPack { get; private set; }

        private void Awake()
        {
            MakeSingleton();
        }

        public void ResetReplay()
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

        public void SetDifficultyPack(DifficultyPack difficultyPack)
        {
            CurrentDifficultyPack = difficultyPack;
        }

        public void CreateNewReplay()
        {
            CurrentPlaySession = new PlaySession()
            {
                PlayerName = "Guest",
                Score = 0,
                difficultyType = CurrentDifficultyPack.DifficultyType
            };
        }
        
        public void CreateNewReplay(DifficultyTypes difficultyTypes)
        {
            CurrentPlaySession = new PlaySession()
            {
                PlayerName = "Guest",
                Score = 0,
                difficultyType = difficultyTypes
            };
        }

        public void SaveReplay(PlaySession playSession)
        {
            // TO DO: Save replay. Set Current Replay to null for now.
            
        }

        public void DeleteReplay()
        {
            CurrentPlaySession = null;
        }

        private void MakeSingleton()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}