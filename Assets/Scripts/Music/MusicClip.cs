using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AzurProject
{
    [CreateAssetMenu(fileName = "New Music Clip", menuName = "Music/MusicClip")]
    public class MusicClip : ScriptableObject
    {
        public string musicTitle;
        public AudioClip musicClip;
        public int loopStart;
        [TextArea(4, 8)]
        public string composerComment;
    }
}
