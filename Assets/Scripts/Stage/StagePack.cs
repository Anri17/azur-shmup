using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AzurShmup.Stage
{
    [CreateAssetMenu(fileName = "New Stage Pack", menuName = "Stage/Pack")]
    public class StagePack : ScriptableObject
    {
        public DifficultyType DifficultyType;
        public Stage[] stages;
    }
}
