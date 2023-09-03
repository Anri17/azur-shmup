using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AzurShmup.Stage.Events
{
    [CreateAssetMenu(fileName = "New Dialogue Stage Event", menuName = "Stage/Event/Dialogue")]
    public class DialogueEvent : StageEvent
    {
        public DialogueConversation Dialogue;
    }
}
