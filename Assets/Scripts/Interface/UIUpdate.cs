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

        Player player;
        GameManager gameManager;

        void Awake()
        {
            gameManager = GameManager.Instance;
        }

        public void StartUIUpdate()
        {
            // player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            player = gameManager.spawnedPlayer.GetComponent<Player>();
        }

        private void Update()
        {
            if (gameManager.CurrentPlaySession != null)
            {
                scoreNumber.text = gameManager.CurrentPlaySession.Score.ToString("000,000,000,000");
            }

            if (gameManager.player != null)
            {
                powerLevelNumber.text = player.PowerLevel.ToString("0.##");
                livesNumber.text = player.Lives.ToString();   
            }
        }
    }
}
