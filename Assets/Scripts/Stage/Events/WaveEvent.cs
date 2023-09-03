using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AzurShmup.Stage.Events
{
    [CreateAssetMenu(fileName = "New Wave Stage Event", menuName = "Stage/Event/Wave")]
    public class WaveEvent : StageEvent
    {
        public GameObject Wave;
    }
}
