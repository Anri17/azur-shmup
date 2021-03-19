using System;
using System.Collections;
using System.Collections.Generic;
using AzurProject.Core;
using UnityEngine;

namespace AzurProject.StageEditor
{
    public class StageEditorManager : MonoBehaviour
    {
        [SerializeField] private GameObject filePanel;

        private SceneManager _sceneManager;

        private void Awake()
        {
            _sceneManager = SceneManager.Instance;
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
