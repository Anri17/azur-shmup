using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AzurProject
{
    public class BackgroundManager : MonoBehaviour
    {
        public GameObject backgroundScene;
        public BackgroundScroll backgroundCamera;

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
