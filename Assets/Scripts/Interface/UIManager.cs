using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using AzurShmup.Core;
using TMPro;
using SceneManager = UnityEngine.SceneManagement.SceneManager;

namespace AzurShmup.Stage
{
    public class UIManager : Singleton<UIManager>
    {
        [Header("Main Menu")]
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] public bool CanOpenMenu = true;

        [Header("UI Display")]
        [SerializeField] private Text powerLevelNumber;
        [SerializeField] private Text scoreNumber;
        [SerializeField] private Text livesNumber;
        
        [Header("Game Over")]
        [SerializeField] private GameObject gameOverScreen;
        [SerializeField] private GameObject finalScoreBoard;
        [SerializeField] private Text endScreenScore;
        [SerializeField] private Text endScreenLivesCalculation;
        [SerializeField] private Text endScreenLives;
        [SerializeField] private Text endScreenBonus;
        [SerializeField] private Text endScreenFinalScore;

        [Header("Miscellaneous")]
        [SerializeField] private TMP_Text nowPlayingBMGText;

        private GameManager _gameManager;
        private AudioPlayer _audioPlayer;

        private void Awake()
        {
            MakeSingleton();

            _gameManager = GameManager.Instance;
            _audioPlayer = AudioPlayer.Instance;

            CanOpenMenu = true;
            pauseMenu.SetActive(false);
            _gameManager.GamePaused = false;
        }

        private void Start()
        {
            gameOverScreen.SetActive(false);
            finalScoreBoard.SetActive(false);
        }

        private void Update()
        {

            if (_gameManager.CurrentPlaySession != null)
            {
                scoreNumber.text = _gameManager.CurrentPlaySession.Score.ToString("000,000,000,000");
            }

            if (_gameManager.Player != null)
            {
                powerLevelNumber.text = _gameManager.Player.PowerLevel.ToString("0.##");
                livesNumber.text = _gameManager.Player.Lives.ToString();
            }
        }


        // TODO: Get the pause functionality the fuck out of this god forsaking class!
        // Leave only the fucking UI elements for fuck sake!
        public void TogglePauseMenu()
        {
            if (pauseMenu.activeSelf)
            {
                // Audio
                _audioPlayer.ResumeMusic();
                _audioPlayer.PlaySfx(_audioPlayer.resumeSfx);
                Time.timeScale = 1;
                // Hide Pause Menu
                pauseMenu.SetActive(false);
                // Unpause Game
                GameManager.LockCursor();
                GameManager.Instance.GamePaused = false;
            }
            else
            {
                // Audio
                _audioPlayer.PauseMusic();
                _audioPlayer.PlaySfx(_audioPlayer.pauseSfx);
                Time.timeScale = 0;
                // Display Pause Menu
                pauseMenu.SetActive(true);
                // Pause Game
                GameManager.UnlockCursor();
                GameManager.Instance.GamePaused = true;
            }
        }

        public void DisplayFinalScoreInformation(ulong score, ulong clearBonus, int lives, ulong lifeBonus, ulong finalScore)
        {
            finalScoreBoard.SetActive(true);

            endScreenScore.text = score.ToString("000,000,000,000");
            endScreenLivesCalculation.text = $"{lives.ToString()} * 10000";
            endScreenLives.text = (lives * 10000).ToString("000,000,000,000");
            endScreenBonus.text = clearBonus.ToString("000,000,000,000");
            endScreenFinalScore.text = finalScore.ToString("000,000,000,000");
        }

        public void HideFinalInfo()
        {
            finalScoreBoard.SetActive(false); // Hide Final Information
        }

        public void DisplayGameOverScreen()
        {
            gameOverScreen.SetActive(true);
        }

        public void DisplayCurrentBMGText(string bgmName)
        {
            nowPlayingBMGText.GetComponent<Animator>().SetBool("Display", false);
            nowPlayingBMGText.text = "♪ Now Playing: " + bgmName;
            nowPlayingBMGText.GetComponent<Animator>().SetBool("Display", true);
        }
    }
}
