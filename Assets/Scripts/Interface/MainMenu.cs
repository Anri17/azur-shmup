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

        private GameManager gameManager;
        private SceneManager _sceneManager;
        private AudioPlayer musicPlayer;

        private void Awake()
        {
            gameManager = GameManager.Instance;
            _sceneManager = SceneManager.Instance;
            musicPlayer = AudioPlayer.Instance;
        }

        private void Start()
        {
            musicPlayer.PlayMusic(menuMusicTheme, 0);
            HideAllMenus();
            foreach (GameObject menu in menus)
                if (menu.name.Equals("MainMenu"))
                    menu.SetActive(true);
        }

        private void Update()
        {
            // Load test scene
            if (Input.GetKeyDown(KeyCode.Minus))
            {
                if ((Application.CanStreamedLevelBeLoaded("_Tests")))
                    UnityEngine.SceneManagement.SceneManager.LoadScene("_Tests");
            }
        }

        // display the chosen menu
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
                    OnStartEasyDifficultyButton();
                    break;
                case DifficultyTypes.NORMAL:
                    OnStartNormalDifficultyButton();
                    break;
                case DifficultyTypes.HARD:
                    OnStartHardDifficultyButton();
                    break;
                case DifficultyTypes.INSANE:
                    OnStartInsaneDifficultyButton();
                    break;
            }
        }

        private void HideAllMenus()
        {
            foreach (GameObject menu in menus)
            {
                menu.SetActive(false);
            }
        }

        private void OnStartEasyDifficultyButton()
        {
            gameManager.SetDifficultyPack(gameManager.easyDifficultyPack);
            _sceneManager.LoadScene((int)SceneIndex.STAGE_SCENE);
            
            gameManager.CreateNewReplay();
        }

        private void OnStartNormalDifficultyButton()
        {
            gameManager.SetDifficultyPack(gameManager.normalDifficultyPack);
            _sceneManager.LoadScene((int)SceneIndex.STAGE_SCENE);
            
            gameManager.CreateNewReplay();
        }

        private void OnStartHardDifficultyButton()
        {
            gameManager.SetDifficultyPack(gameManager.hardDifficultyPack);
            _sceneManager.LoadScene((int)SceneIndex.STAGE_SCENE);
            
            gameManager.CreateNewReplay();
        }

        private void OnStartInsaneDifficultyButton()
        {
            gameManager.SetDifficultyPack(gameManager.insaneDifficultyPack);
            
            _sceneManager.LoadScene((int)SceneIndex.STAGE_SCENE);
            
            gameManager.CreateNewReplay();
        }
    }
}
