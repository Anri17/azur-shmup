using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using AzurProject.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AzurProject
{
    public class PractiseSelector : MonoBehaviour
    {
        [SerializeField] private TMP_Text selectedDifficultyText;
        [SerializeField] private GameObject stageListPanel;

        private List<GameObject> _stageButtons;
        
        private GameManager _gameManager;
        private SceneManager _sceneManager;
        
        private void Awake()
        {
            _gameManager = GameManager.Instance;
            _sceneManager = SceneManager.Instance;
            selectedDifficultyText.text = "";
        }

        public void SelectDifficulty(DifficultyTypeComponent difficulty)
        {
            switch (difficulty.difficulty)
            {
                case DifficultyTypes.EASY:
                    DisplayStages(_gameManager.easyDifficultyPack);
                    // list out EASY difficulty stages on the panel
                    break;
                case DifficultyTypes.NORMAL:
                    DisplayStages(_gameManager.normalDifficultyPack);
                    // list out NORMAL difficulty stages on the panel
                    break;
                case DifficultyTypes.HARD:
                    DisplayStages(_gameManager.hardDifficultyPack);
                    // list out HARD difficulty stages on the panel
                    break;
                case DifficultyTypes.INSANE:
                    DisplayStages(_gameManager.insaneDifficultyPack);
                    // list out INSANE difficulty stages on the panel
                    break;
            }
        }

        private void DisplayStages(DifficultyPack difficultyPack)
        {
            // write text for displaying the difficulty name
            selectedDifficultyText.text = difficultyPack.DifficultyType.ToString();
            // create buttons for each stage
            if (_stageButtons != null)
            {
                foreach (GameObject gameObj in _stageButtons)
                {
                    Destroy(gameObj);
                }
                _stageButtons.Clear();
            }
            else
            {
                _stageButtons = new List<GameObject>();   
            }
            
            float yPos = 0;
            float height = 40f;
            int contentTotalSize = 0;
            for (int i = 0; i < difficultyPack.stages.Length; i++)
            {
                int x = i;
                // Set size of container to fit the new music track
                // contentTotalSize = contentTotalSize + 40;
                
                RectTransform parentObject = stageListPanel.GetComponentInParent<RectTransform>();

                parentObject.sizeDelta = new Vector2(parentObject.sizeDelta.x, contentTotalSize);

                // create buttonGameObject
                GameObject tempButtonGameObject = new GameObject();
                // create RectTransform for buttonGameObject
                RectTransform tempButtonRectTransform = tempButtonGameObject.AddComponent<RectTransform>();
                // create button for buttonGameObject
                Button tempButton = tempButtonGameObject.AddComponent<Button>();
                tempButtonGameObject.transform.SetParent(stageListPanel.transform);
                // create textGameObject for buttonGameObject
                GameObject tempButtonTextGameObject = new GameObject();
                tempButtonTextGameObject.transform.SetParent(tempButtonGameObject.transform);
                // create text for textGameObject 
                Text tempButtonText = tempButtonTextGameObject.AddComponent<Text>();
                tempButtonText.font = _gameManager.diogenesFont;
                
                // configure everything up
                // set button functionality
                tempButton.onClick.AddListener(delegate
                {
                    PlayStage(difficultyPack, x+1);
                });

                // set button
                tempButtonGameObject.name = "Stage" + (x + 1);
                tempButtonRectTransform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                tempButtonRectTransform.sizeDelta = new Vector2(parentObject.sizeDelta.x, height);
                tempButtonRectTransform.anchorMin = new Vector2(0f, 1f);
                tempButtonRectTransform.anchorMax = new Vector2(0f, 1f);
                tempButtonRectTransform.pivot = new Vector2(0f, 1f);
                tempButtonRectTransform.anchoredPosition = new Vector3(0f, yPos, 0f);
                // set button text position
                tempButtonText.text = $"Stage {i + 1}";
                tempButtonText.rectTransform.sizeDelta = tempButtonRectTransform.sizeDelta;
                tempButtonText.alignment = TextAnchor.MiddleCenter;
                tempButtonText.fontSize = 16;
                
                yPos -= height;

                parentObject.sizeDelta = new Vector2(200, 240);
                
                _stageButtons.Add(tempButtonGameObject);
            }
            // make each button start a new game with only that stage
            // make a custom difficulty pack with only that stage
        }

        private void PlayStage(DifficultyPack difficultyPack, int stageNumber)
        {
            // create difficulty pack
            // load new difficulty pack
            
            DifficultyPack customDifficultyPack = ScriptableObject.CreateInstance<DifficultyPack>();

            customDifficultyPack.DifficultyType = difficultyPack.DifficultyType;
            customDifficultyPack.stages = new Stage[1];
            customDifficultyPack.stages[0] = difficultyPack.stages[stageNumber-1];
            _gameManager.SetDifficultyPack(customDifficultyPack);
            
            _sceneManager.LoadScene((int)SceneIndex.STAGE_SCENE);
            
            _gameManager.CreateNewReplay();
        }
    }
}
