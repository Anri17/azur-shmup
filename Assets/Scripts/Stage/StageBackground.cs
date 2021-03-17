using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AzurProject
{
    [CreateAssetMenu(fileName = "New Stage Background", menuName = "Stage/Stage Background")]
    public class StageBackground : ScriptableObject
    {
        public GameObject background;
        public Vector3 cameraEnd;
        public float cameraSpeed;
    }
}
