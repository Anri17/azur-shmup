using UnityEngine;

namespace AzurShmup.Stage
{
    // TODO: Have transitions scenes. Make is so a that Background has another background for when the previous one ends. This is usefull for smooth background transitions.
    // TODO: Also, find a way to control the camera with just data given from ScriptableObjects.
    public class BackgroundManager : Singleton<BackgroundManager>
    {
        public GameObject backgroundScene;
        public BackgroundScroll backgroundCamera;

        private void Awake()
        {
            MakeSingleton();
        }

        public void LoadBackground(GameObject background, Vector3 cameraEnd, float cameraSpeed)
        {
            // Set the camera to the start
            backgroundCamera.transform.position = transform.position;

            // Set the end of the camera
            backgroundCamera.endPosTransform = cameraEnd;
            backgroundCamera.scrollSpeed = cameraSpeed;

            // Set the background
            backgroundScene = Instantiate(background, transform);
        }

        public void LoadBackground(StageBackground background)
        {
            // Set the camera to the start
            backgroundCamera.transform.position = transform.position;

            // Set the end of the camera
            backgroundCamera.endPosTransform = background.cameraEnd;
            backgroundCamera.scrollSpeed = background.cameraSpeed;

            // Set the background
            backgroundScene = Instantiate(background.background, transform);
        }

        public void UnloadBackground()
        {
            // Unloads Background
            if (backgroundScene != null) Destroy(backgroundScene);

            // Set the camera to the start
            backgroundCamera.transform.position = transform.position;

            // Set the end of the camera
            backgroundCamera.endPosTransform = Vector3.zero;
            backgroundCamera.scrollSpeed = 0;
        }
    }
}
