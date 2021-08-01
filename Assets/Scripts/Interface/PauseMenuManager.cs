using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using AzurProject.Core;
using SceneManager = AzurProject.Core.SceneManager;

namespace AzurProject
{
    public class PauseMenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenu;

        private GameManager _gameManager;
        private SceneManager _sceneManager;
        private AudioPlayer _audioPlayer;

        void Awake()
        {
            _gameManager = GameManager.Instance;
            _sceneManager = SceneManager.Instance;
            _audioPlayer = AudioPlayer.Instance;
        }

        public void BackToMainMenu()
        {
            _sceneManager.LoadScene((int)SceneIndex.MENU_SCENE);
            Time.timeScale = 1;
            _gameManager.DeleteReplay();
        }

        public void RestartLevel()
        {
            _sceneManager.LoadScene((int)SceneIndex.STAGE_SCENE);
            Time.timeScale = 1;
            _gameManager.ResetReplay();
        }

        public void ResumeGame()
        {
            UIManager.Instance.TogglePauseGame();
        }
    }
}
