using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AzurProject
{
    public class PlaySession
    {
        public string PlayerName { get; set; }
        public ulong Score { get; set; }
        public DifficultyTypes DifficultyType { get; set; }
    }
}
