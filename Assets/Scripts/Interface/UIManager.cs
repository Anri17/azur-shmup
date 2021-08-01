using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using AzurProject.Core;
using TMPro;
using UnityEngine.Serialization;
using SceneManager = UnityEngine.SceneManagement.SceneManager;

namespace AzurProject
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }
        private AudioPlayer _audioPlayer;

        [Header("Main Menu")]
        public GameObject pauseMenu;

        public bool canOpenMenu;
        [Header("Game Over")]
        [SerializeField] private GameObject gameOverScreen;
        [SerializeField] private GameObject finalScoreBoard;
        [SerializeField] private Text endScreenScore;
        [SerializeField] private Text endScreenLivesCalculation;
        [SerializeField] private Text endScreenLives;
        [SerializeField] private Text endScreenBonus;
        [SerializeField] private Text endScreenFinalScore;
        [FormerlySerializedAs("_nowPlayingBMGText")] [SerializeField] private TMP_Text nowPlayingBMGText;

        private void Awake()
        {
            Instance = this;
            canOpenMenu = true;
            pauseMenu.SetActive(false);
            GameManager.Instance.GamePaused = false;
            _audioPlayer = AudioPlayer.Instance;
        }

        private void Start()
        {
            gameOverScreen.SetActive(false);
            finalScoreBoard.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && canOpenMenu)
            {
                TogglePauseGame();
            }
        }

        public void TogglePauseGame()
        {
            if (pauseMenu.activeSelf)
            {
                _audioPlayer.ResumeMusic();
                _audioPlayer.PlaySfx(_audioPlayer.resumeSfx);
                Time.timeScale = 1;
                pauseMenu.SetActive(false);
                GameManager.LockCursor();
                GameManager.Instance.GamePaused = false;
            }
            else
            {
                _audioPlayer.PauseMusic();
                _audioPlayer.PlaySfx(_audioPlayer.pauseSfx);
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
                GameManager.UnlockCursor();
                GameManager.Instance.GamePaused = true;
            }
        }

        public void DisplayFinalInfo()
        {
            ulong score = GameManager.Instance.CurrentPlaySession.Score;
            ulong clearBonus = 1000000;
            int lives = GameManager.Instance.spawnedPlayer.GetComponent<Player>().Lives;
            ulong lifeBonus = (ulong)lives * 10000;
            ulong finalScore = lifeBonus + score + clearBonus;

            finalScoreBoard.SetActive(true);
            endScreenScore.text = score.ToString("000,000,000,000");
            endScreenLivesCalculation.text = $"{lives.ToString()} * 10000";
            endScreenLives.text = (lives * 10000).ToString("000,000,000,000");
            endScreenBonus.text = clearBonus.ToString("000,000,000,000");
            endScreenFinalScore.text = finalScore.ToString("000,000,000,000");

            GameManager.Instance.CurrentPlaySession.Score = finalScore;
        }

        public void ResetStage()
        {
            finalScoreBoard.SetActive(false);       // Hide Final Information
            WaveManager.Instance.ReachedEnd = false;
            Time.timeScale = 1;
            GameManager.LockCursor();
        }

        public void EndStage()
        {
            DisplayFinalInfo();
            GameManager.UnlockCursor();
            Time.timeScale = 1;
        }

        public void GameOver()
        {
            DisplayGameOverScreen();
            StartCoroutine(GameOverCoroutine());
        }

        private void DisplayGameOverScreen()
        {
            gameOverScreen.SetActive(true);
        }

        public void DisplayCurrentBMGText(string bgmName)
        {
            nowPlayingBMGText.text = "♪ Now Playing: " + bgmName;
            nowPlayingBMGText.GetComponent<Animator>().SetBool("Display", true);
        }

        private IEnumerator GameOverCoroutine()
        {
            GameManager.UnlockCursor();
            Time.timeScale = 1;
            GameObject.Find("UIManager").GetComponent<UIManager>().canOpenMenu = false;
            yield return new WaitForSeconds(5f);
            SceneManager.LoadScene((int)SceneIndex.MENU_SCENE);
        }
    }
}
