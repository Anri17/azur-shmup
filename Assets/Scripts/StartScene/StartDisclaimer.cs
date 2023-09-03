using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AzurShmup.Core;

namespace AzurShmup.StartScene
{
    public class StartDisclaimer : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(LoadMainGame(8f));
        }

        private IEnumerator LoadMainGame(float spashScreenDuration)
        {
            yield return new WaitForSeconds(spashScreenDuration);
            SceneManager.Instance.LoadScene((int)SceneIndex.MENU_SCENE);
            gameObject.SetActive(false);

            yield return null;
        }
    }
}
