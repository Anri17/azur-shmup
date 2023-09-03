using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using AzurShmup.Core;
using UnityEngine.UI;
using SceneManager = AzurShmup.Core.SceneManager;

namespace AzurShmup
{
    public class MainMenuManager : Singleton<MainMenuManager>
    {
        [SerializeField] private AudioClip menuMusicTheme;
        [SerializeField] private GameObject[] uiMenus;

        private GameManager _gameManager;
        private SceneManager _sceneManager;
        private AudioPlayer _audioPlayer;

        private void Awake()
        {
            MakeSingleton();

            _gameManager = GameManager.Instance;
            _sceneManager = SceneManager.Instance;
            _audioPlayer = AudioPlayer.Instance;
        }

        private void Start()
        {
            _audioPlayer.PlayMusic(menuMusicTheme, 0);
            HideAllMenus();
            foreach (GameObject menu in uiMenus)
                if (menu.name.Equals("Pannel_MainMenu"))
                    menu.SetActive(true);
        }

        public void DisplayMenu(GameObject menu)
        {
            HideAllMenus();
            menu.SetActive(true);
        }

        public void GameSetDifficultyEasy() => _gameManager.DifficultyType = DifficultyType.EASY;
        public void GameSetDifficultyNormal() => _gameManager.DifficultyType = DifficultyType.NORMAL;
        public void GameSetDifficultyHard() => _gameManager.DifficultyType = DifficultyType.HARD;
        public void GameSetDifficultyInsane() => _gameManager.DifficultyType = DifficultyType.INSANE;

        public void GameSetPlayerShotTypeHoming() => _gameManager.PlayerShotType = PlayerShotType.HOMING;
        public void GameSetPlayerShotTypeNeedle() => _gameManager.PlayerShotType = PlayerShotType.NEEDLE;

        public void GameStart()
        {
            _gameManager.SetStagePack();
            _sceneManager.LoadScene(SceneIndex.STAGE_SCENE);
            _gameManager.CreatePlaySession();
        }

        private void HideAllMenus()
        {
            foreach (GameObject menu in uiMenus)
            {
                menu.SetActive(false);
            }
        }

        public void ExitApplication()
        {
            _gameManager.CloseGame();
        }
    }
}
