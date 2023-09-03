using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AzurShmup.Core
{
    public class SceneManager : SingletonPersistent<SceneManager>
    {
        [SerializeField] private GameObject loadingScreen;
        [SerializeField] private Slider loadingBarSlider;
        [SerializeField] private Text loadingPercentageText;

        private void Awake()
        {
            MakeSingleton();
        }

        public void LoadScene(int sceneIndex)
        {
            StartCoroutine(LoadSceneCoroutine(sceneIndex));
        }

        public void LoadScene(SceneIndex sceneIndex)
        {
            StartCoroutine(LoadSceneCoroutine((int)sceneIndex));
        }

        private IEnumerator LoadSceneCoroutine(int sceneIndex)
        {
            // show the loading screen
            Animator loadingScreenAnimator = loadingScreen.GetComponent<Animator>();
            loadingScreen.SetActive(true);
            loadingScreenAnimator.SetBool("Loading", true);
            while (loadingScreenAnimator.GetCurrentAnimatorStateInfo(0).IsName("LoadingScreenFadeIn") && loadingScreenAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
            {
                yield return null;
            }
            yield return new WaitForSeconds(0.5f);

            // start the loading
            AsyncOperation currentLoadingData = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneIndex);
            
            while (!currentLoadingData.isDone)
            {
                float progress = Mathf.Clamp((currentLoadingData.progress / 0.9f), 0, 1);
                loadingBarSlider.value = progress;
                loadingPercentageText.text = $"{Math.Round(progress * 100, 0)}%";

                yield return null;
            }
            
            // hide the loading screen
            loadingScreenAnimator.SetBool("Loading", false);
            while (loadingScreenAnimator.GetCurrentAnimatorStateInfo(0).IsName("LoadingScreenFadeOut") && loadingScreenAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
            {
                yield return null;
            }
            loadingScreen.SetActive(false);
        }
    }
}
