using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AzurShmup
{
    public class PlaySession
    {
        public string PlayerName { get; set; }
        public ulong Score { get; set; }
        public DifficultyType DifficultyType { get; set; }
        public PlayerShotType PlayerType { get; set; }
    }
}
