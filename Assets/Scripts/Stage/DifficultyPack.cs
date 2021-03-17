using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AzurProject
{
    [CreateAssetMenu(fileName = "New Difficulty", menuName = "Stage/Difficulty")]
    public class DifficultyPack : ScriptableObject
    {
        public DifficultyTypes DifficultyType;
        public Stage[] stages;
    }
}
