using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AzurShmup.Stage
{
    [CreateAssetMenu(fileName = "New Stage Banner", menuName = "Stage/Banner")]
    public class StageBanner : ScriptableObject
    {
        public GameObject bannerObject;
        public float duration;
    }
}
