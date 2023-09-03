using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AzurShmup.Stage.Events;

namespace AzurShmup.Stage
{
    [CreateAssetMenu(fileName = "New Stage", menuName = "Stage/Stage")]
    public class Stage : ScriptableObject
    {
        public string Name;
        public string Description;
        public StageEvent[] Events;
        public StageBackground Background;
    }
}
