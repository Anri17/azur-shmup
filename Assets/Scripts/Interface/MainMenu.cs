using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using AzurProject.Core;
using UnityEngine.UI;
using SceneManager = AzurProject.Core.SceneManager;

namespace AzurProject
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private AudioClip menuMusicTheme;
        [SerializeField] private GameObject[] menus;

        [SerializeField] private Button[] mainMenuButtons;

        private GameManager _gameManager;
        private SceneManager _sceneManager;
        private AudioPlayer _musicPlayer;

        private void Awake()
        {
            _gameManager = GameManager.Instance;
            _sceneManager = SceneManager.Instance;
            _musicPlayer = AudioPlayer.Instance;
        }

        private void Start()
        {
            _musicPlayer.PlayMusic(menuMusicTheme, 0);
            HideAllMenus();
            foreach (GameObject menu in menus)
                if (menu.name.Equals("MainMenu"))
                    menu.SetActive(true);
        }

        public void DisplayMenu(GameObject menu)
        {
            HideAllMenus();
            menu.SetActive(true);
        }

        public void ExitApplication()
        {
            Application.Quit();
        }

        public void StartGame(DifficultyTypeComponent difficulty)
        {
            switch(difficulty.difficulty)
            {
                case DifficultyTypes.EASY:
                    _gameManager.SetDifficultyPack(_gameManager.easyDifficultyPack);
                    break;
                case DifficultyTypes.NORMAL:
                    _gameManager.SetDifficultyPack(_gameManager.normalDifficultyPack);
                    break;
                case DifficultyTypes.HARD:
                    _gameManager.SetDifficultyPack(_gameManager.hardDifficultyPack);
                    break;
                case DifficultyTypes.INSANE:
                    _gameManager.SetDifficultyPack(_gameManager.insaneDifficultyPack);
                    break;
            }
            _sceneManager.LoadScene((int)SceneIndex.STAGE_SCENE);
            _gameManager.CreateNewReplay();
        }

        private void HideAllMenus()
        {
            foreach (GameObject menu in menus)
            {
                menu.SetActive(false);
            }
        }
    }
}
