using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AzurProject.Core
{
    public class SceneManager : MonoBehaviour
    {
        [SerializeField] private GameObject loadingScreen;
        [SerializeField] private Slider loadingBarSlider;
        [SerializeField] private Text loadingPercentageText;
        
        public static SceneManager Instance { get; private set; }

        public void LoadScene(int sceneIndex)
        {
            StartCoroutine(LoadSceneCoroutine(sceneIndex));
        }

        public void LoadScene(SceneIndex sceneIndex)
        {
            StartCoroutine(LoadSceneCoroutine((int)sceneIndex));
        }

        private void Awake()
        {
            MakeSingleton();
        }

        private void MakeSingleton()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private IEnumerator LoadSceneCoroutine(int sceneIndex)
        {
            AsyncOperation currentLoadingData = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneIndex);

            loadingScreen.SetActive(true);

            while (!currentLoadingData.isDone)
            {
                float progress = Mathf.Clamp((currentLoadingData.progress / 0.9f), 0, 1);
                loadingBarSlider.value = progress;
                loadingPercentageText.text = $"{Math.Round(progress * 100, 2)} %";

                yield return null;
            }
            loadingScreen.SetActive(false);
        }
    }
}
