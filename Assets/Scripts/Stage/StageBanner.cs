using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AzurProject
{
    [CreateAssetMenu(fileName = "New Stage Banner", menuName = "Stage/Stage Banner")]
    public class StageBanner : ScriptableObject
    {
        public GameObject bannerObject;
        public float duration;
    }
}
