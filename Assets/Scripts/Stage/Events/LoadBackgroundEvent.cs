using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AzurShmup.Stage.Events
{
    [CreateAssetMenu(fileName = "New Load Background Stage Event", menuName = "Stage/Event/Load Background")]
    public class LoadBackgroundEvent : StageEvent
    {
        public StageBackground Background;
    }
}