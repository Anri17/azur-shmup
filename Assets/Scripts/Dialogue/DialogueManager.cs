using System;
using System.Runtime;
using System.Collections;
using System.Collections.Generic;
using AzurShmup.Core;
using AzurShmup.Stage.Events;
using UnityEngine;
using UnityEngine.UI;

namespace AzurShmup.Stage
{
    public class DialogueManager : Singleton<DialogueManager>
    {
        public GameObject dialogueScreen;
        public Text nameText;
        public Text dialogueText;

        private Queue<DialogueMessage> _dialogueMessages;

        public bool dialogueEnded = true;
        
        public bool PlayingDialogue { get; private set; }

        [SerializeField] BossManager bossManager;

        private void Awake()
        {
            MakeSingleton();

            dialogueScreen.SetActive(false);
            _dialogueMessages = new Queue<DialogueMessage>();
        }

        private void Start()
        {
            dialogueText.text = "";
            nameText.text = "";
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return) && !dialogueEnded)
            {
                NextDialogueMessage();
            }
        }

        public void StartDialogue(DialogueConversation dialogueConversation)
        {
            PlayingDialogue = true;
            dialogueText.text = "";
            nameText.text = "";
            dialogueEnded = false;
            dialogueScreen.SetActive(true);
            _dialogueMessages.Clear();
            UnpackDialogue(dialogueConversation);
            StartCoroutine(WaitSeconds(NextDialogueMessage, 1));
        }

        private void UnpackDialogue(DialogueConversation dialogueConversation)
        {
            foreach (DialogueMessage message in dialogueConversation.DialogueMessages)
            {
                _dialogueMessages.Enqueue(message);
            }
        }
        
        private void TypeMessage(string speakerName, string message)
        {
            StopAllCoroutines();
            nameText.text = speakerName;
            StartCoroutine(TypeMessageCoroutine(message));
        }

        private void EndDialogue()
        {
            PlayingDialogue = false;
            nameText.text = "";
            dialogueText.text = "";
            dialogueScreen.SetActive(false);
            dialogueEnded = true;
        }

        private void NextDialogueMessage()
        {
            if (_dialogueMessages.Count != 0)
            {
                DialogueMessage currentMessage = _dialogueMessages.Dequeue();
                string name = currentMessage.speakerName;
                string message = currentMessage.message;
                TypeMessage(name, message);

                // TODO: Chain events with dialogue
                if (currentMessage.changeMusic)
                {
                    AudioPlayer.Instance.StopMusic();
                    StartCoroutine(WaitSeconds(() =>
                    {
                        AudioPlayer.Instance.PlayMusic(currentMessage.musicClip);
                        UIManager.Instance.DisplayCurrentBMGText(currentMessage.musicClip.musicTitle);
                    }, currentMessage.changeMusicDelay));
                }

                // TODO: Chain event with dialogue
                if (currentMessage.presentBoss)
                {
                    bossManager.spawnedBoss.GetComponent<Boss>().MoveToPosition(GameManager.DEFAULT_BOSS_POSITION);
                }
            }
            else
            {
                EndDialogue();
            }
        }

        private IEnumerator TypeMessageCoroutine(string sentence)
        {
            dialogueText.text = "";

            foreach (char letter in sentence.ToCharArray())
            {
                dialogueText.text += letter;
                yield return null;
            }
        }
        
        private IEnumerator WaitSeconds(Action methodToRun, float secondsToWait)
        {
            yield return new WaitForSeconds(secondsToWait);
            methodToRun();
        }
    }
}
