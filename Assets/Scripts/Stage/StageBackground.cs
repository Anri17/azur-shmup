using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AzurShmup.Stage
{
    [CreateAssetMenu(fileName = "New Stage Background", menuName = "Stage/Background")]
    public class StageBackground : ScriptableObject
    {
        public GameObject background;
        public Vector3 cameraEnd;
        public float cameraSpeed;
    }
}
