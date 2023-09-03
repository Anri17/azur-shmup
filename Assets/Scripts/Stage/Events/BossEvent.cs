using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AzurShmup.Stage.Events
{
    [CreateAssetMenu(fileName = "New Boss Stage Event", menuName = "Stage/Event/Boss")]
    public class BossEvent : StageEvent
    {
        public GameObject bossGameObject;
        public Vector2 spawnPosition;
    }
}