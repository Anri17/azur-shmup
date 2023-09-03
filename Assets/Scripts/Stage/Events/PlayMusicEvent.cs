using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AzurShmup.Stage.Events
{
    [CreateAssetMenu(fileName = "New Play Music Stage Event", menuName = "Stage/Event/Play Music")]
    public class PlayMusicEvent : StageEvent
    {
        public MusicClip Clip;
    }
}
