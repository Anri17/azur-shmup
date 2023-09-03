using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using AzurShmup.Core;
using SceneManager = AzurShmup.Core.SceneManager;

namespace AzurShmup.Stage
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
            _gameManager.EndPlaySession();
        }

        public void RestartLevel()
        {
            _sceneManager.LoadScene((int)SceneIndex.STAGE_SCENE);
            Time.timeScale = 1;
            _gameManager.ResetPlaySession();
        }

        public void ResumeGame()
        {
            UIManager.Instance.TogglePauseMenu();
        }
    }
}
