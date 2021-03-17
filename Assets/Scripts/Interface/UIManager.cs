using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using AzurProject.Core;
using TMPro;
using SceneManager = UnityEngine.SceneManagement.SceneManager;

namespace AzurProject
{
    public class UIManager : MonoBehaviour
    {
        private AudioPlayer _audioPlayer;

        [Header("Main Menu")]
        public GameObject pauseMenu;

        public bool canOpenMenu;
        [Header("Game Over")]
        [SerializeField] GameObject gameOverScreen;
        [SerializeField] GameObject finalScoreBoard;
        [SerializeField] Text endScreenScore;
        [SerializeField] Text endScreenLivesCalculation;
        [SerializeField] Text endScreenLives;
        [SerializeField] Text endScreenBonus;
        [SerializeField] Text endScreenFinalScore;
        [SerializeField] TMP_Text _nowPlayingBMGText;

        public bool reachedEnd = false;

        void Awake()
        {
            canOpenMenu = true;
            pauseMenu.SetActive(false);
            _audioPlayer = AudioPlayer.Instance;
        }

        private void Start()
        {
            gameOverScreen.SetActive(false);
            finalScoreBoard.SetActive(false);
            reachedEnd = false;
        }

        void Update()
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
                
            }
            else
            {
                _audioPlayer.PauseMusic();
                _audioPlayer.PlaySfx(_audioPlayer.pauseSfx);
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
                GameManager.UnlockCursor();
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
            reachedEnd = false;
            Time.timeScale = 1;
            GameManager.LockCursor();
        }

        public void EndStage()
        {
            DisplayFinalInfo();
            reachedEnd = true;
            GameManager.UnlockCursor();
            Time.timeScale = 1;
        }

        public void GameOver()
        {
            DisplayGameOverScreen();
            StartCoroutine(GameOverCoroutine());
        }

        void DisplayGameOverScreen()
        {
            gameOverScreen.SetActive(true);
        }

        public void DisplayCurrentBMGText(string bgmName)
        {
            _nowPlayingBMGText.text = "♪ Now Playing: " + bgmName;
            _nowPlayingBMGText.GetComponent<Animator>().SetBool("Display", true);
        }

        IEnumerator GameOverCoroutine()
        {
            GameManager.UnlockCursor();
            Time.timeScale = 1;
            GameObject.Find("UIManager").GetComponent<UIManager>().canOpenMenu = false;
            yield return new WaitForSeconds(5f);
            SceneManager.LoadScene((int)SceneIndex.MENU_SCENE);
        }
    }
}
