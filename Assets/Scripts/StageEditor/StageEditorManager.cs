using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using AzurProject.Core;
using AzurProject.Data;
using AzurProject.Enemy;
using UnityEngine;

namespace AzurProject.StageEditor
{
    public class StageEditorManager : MonoBehaviour
    {
        public static StageEditorManager Instance { get; private set; }
        
        [SerializeField] private GameObject filePanel;

        private SceneManager _sceneManager;

        private void Awake()
        {
            _sceneManager = SceneManager.Instance;

            Instance = this;
        }

        private void Start()
        {
            NormalEnemyData normalEnemyData = new NormalEnemyData()
            {
                Position = new Vector2(0, 0),
                Health = 10,
                Name = "Medium Ship",
                Score = 1000000,
                Sprite = @"D:\Projects\Azur Project\azur-project\Assets\Textures\Enemies\Enemies\medium_ship\medium_ship.png",
                LifeItemDrop = 0,
                PowerItemDrop = 0,
                ScoreItemDrop = 0,
                BigPowerItemDrop = 0
            };

            EnemyFactory.BuildNormalEnemy(normalEnemyData);
        }

        public void ToggleFilePanel()
        {
            if (!filePanel.activeSelf)
                filePanel.SetActive(true);
            else
                filePanel.SetActive(false);
        }

        public void ExitToMainMenu()
        {
            _sceneManager.LoadScene(SceneIndex.MENU_SCENE);
        }
    }
}
