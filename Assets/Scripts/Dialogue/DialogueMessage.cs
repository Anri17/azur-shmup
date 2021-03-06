using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AzurProject
{
    [CreateAssetMenu(fileName = "New Dialogue Message", menuName = "Dialogue/DialogueMessage")]
    public class DialogueMessage : ScriptableObject
    {
        public string personSpeaking;
        [TextArea(3, 10)]
        public string sentence;
        public bool changeMusic = false;
        public bool presentBoss = false;
    }
}
