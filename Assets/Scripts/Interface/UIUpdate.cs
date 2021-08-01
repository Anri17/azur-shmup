using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AzurProject.Core;

namespace AzurProject
{
    public class UIUpdate : MonoBehaviour
    {
        public Text powerLevelNumber;
        public Text scoreNumber;
        public Text livesNumber;

        private Player _player;
        private GameManager _gameManager;

        private void Awake()
        {
            _gameManager = GameManager.Instance;
        }

        public void StartUIUpdate()
        {
            _player = _gameManager.spawnedPlayer.GetComponent<Player>();
        }

        private void Update()
        {
            if (_gameManager.CurrentPlaySession != null)
            {
                scoreNumber.text = _gameManager.CurrentPlaySession.Score.ToString("000,000,000,000");
            }

            if (_gameManager.ryuukoA != null)
            {
                powerLevelNumber.text = _player.PowerLevel.ToString("0.##");
                livesNumber.text = _player.Lives.ToString();   
            }
        }
    }
}
