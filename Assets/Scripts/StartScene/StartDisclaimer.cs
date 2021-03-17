using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AzurProject.Core;

namespace AzurProject.StartScene
{
    public class StartDisclaimer : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(LoadMainGame(8f));
        }

        private IEnumerator LoadMainGame(float spashScreenDuration)
        {
            // if (gameObject != null)
            {
                yield return new WaitForSeconds(spashScreenDuration);
                SceneManager.Instance.LoadScene((int)SceneIndex.MENU_SCENE);
                gameObject.SetActive(false);
            }
            yield return null;
        }
    }
}
