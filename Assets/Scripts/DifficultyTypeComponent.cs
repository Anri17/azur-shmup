using System;
using UnityEngine;

namespace AzurProject
{
    [Serializable]
    public enum DifficultyTypes
    {
        UNDEFINED,
        EASY,
        NORMAL,
        HARD,
        INSANE,
        EXTRA
    }

    public enum PlayerTypes
    {
        RYUUKO_A,
        RYUUKO_B
    }

    public class DifficultyTypeComponent : MonoBehaviour
    {
        public DifficultyTypes difficulty;
    }
}
