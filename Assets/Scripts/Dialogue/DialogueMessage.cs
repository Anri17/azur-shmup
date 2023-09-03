using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AzurShmup
{
    [CreateAssetMenu(fileName = "New Dialogue Message", menuName = "Dialogue/Message")]
    public class DialogueMessage : ScriptableObject
    {
        public string speakerName;
        [TextArea(3, 10)]
        public string message;
        public bool changeMusic = false;
        public float changeMusicDelay = 1f;
        public MusicClip musicClip;
        public bool presentBoss = false;
    }
}
