using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using AzurProject.Core;

namespace AzurProject
{
    public class StageManager : MonoBehaviour
    {
        [Header("Managers")] [SerializeField] private WaveManager waveManager;
        [SerializeField] private BackgroundManager backgroundManager;
        private GameManager gameManager;
        
        [Header("Stages")] public Stage[] stages;
        public int stageIndex = 0;

        void Awake() => gameManager = GameManager.Instance;

        void Start()
        {
            if (gameManager.CurrentDifficultyPack != null)
            {
                // Load the stages of the chosen difficulty for use
                stages = new Stage[gameManager.CurrentDifficultyPack.stages.Length];
                stages = gameManager.CurrentDifficultyPack.stages;
            }

            // Start The Wave Manager
            waveManager.StartWaveManager();
        }

        public void LoadStage(Stage stage)
        {
            backgroundManager.LoadBackground(stage.background.background, stage.background.cameraEnd,
                stage.background.cameraSpeed); // Load Background
            waveManager.LoadWaves(stage.waves); // Load Waves
            waveManager.LoadBanner(stage.banner); // Load Banner
            waveManager.LoadMusic(stage.stageMusic, stage.bossMusic); // Load Music
        }

        public void UnloadStage()
        {
            backgroundManager.UnloadBackground(); // Unload Background
            waveManager.UnloadWaves(); // Unload Waves
            waveManager.UnloadStartFlag(); // Unload Start Flag
            waveManager.UnloadMusic(); // Unload Music
        }
    }
}