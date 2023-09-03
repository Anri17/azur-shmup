using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AzurShmup
{
    [CreateAssetMenu(fileName = "New Player Data", menuName = "Player/Player Data")]
    public class PlayerData : ScriptableObject
    {
        public GameObject Prefab;
        public PlayerType PlayerType;
    }

    [Serializable]
    public enum PlayerType
    {
        RyuukoA,
        RyuukoB,
        LaraA,
        LaraB
    }
}
