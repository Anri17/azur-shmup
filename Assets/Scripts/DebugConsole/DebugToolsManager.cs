using AzurShmup.Core;
using UnityEngine;

namespace AzurShmup.Debug
{
    public class DebugToolsManager : Singleton<DebugToolsManager>
    {
        [SerializeField] private GameObject DebugTools;

        GameManager _gameManager;
        SceneManager _sceneManager;

        private bool debugMode = false;

        private void Awake()
        {
            MakeSingleton();

            _gameManager = GameManager.Instance;
            _sceneManager = SceneManager.Instance;
        }

        private void Update()
        {
            if (InputManager.GetKeyDown(InputManager.DebugMode))
            {
                if (debugMode)
                {
                    debugMode = false;
                    DebugTools.active = false;
                }
                else
                {
                    debugMode = true;
                    DebugTools.active = true;
                }
            }
        }

        public void DEBUG_LoadStagePack()
        {
            _gameManager.SetStagePack(DifficultyType.DEBUG);
            _gameManager.CreatePlaySession();
            _sceneManager.LoadScene(SceneIndex.STAGE_SCENE);
        }
    }
}
