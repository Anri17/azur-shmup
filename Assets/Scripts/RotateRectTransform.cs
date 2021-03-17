using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AzurProject
{
    public class RotateRectTransform : MonoBehaviour
    {
        public float rotationSpeed = 8f;

        RectTransform rectTransform;

        void Start()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        void Update()
        {
            // rotate object
            rectTransform.Rotate(new Vector3(0, 0, rectTransform.rotation.z + rotationSpeed));
        }
    }
}
