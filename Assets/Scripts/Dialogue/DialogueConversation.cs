using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AzurShmup
{
    [CreateAssetMenu(fileName = "New Dialogue Conversation", menuName = "Dialogue/Conversation")]
    public class DialogueConversation : ScriptableObject
    {
        public DialogueMessage[] DialogueMessages;
    }
}
