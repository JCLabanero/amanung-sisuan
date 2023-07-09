using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace DIALOGUE
{
    public class DialogueSystem : MonoBehaviour
    {
        public ContainerDialogue containerDialogue = new ContainerDialogue();
        private ManagerConversation conversationManager;
        private TextArchitect architect;
        /// <summary>
        /// Singleton
        /// </summary>
        /// <param name="text"></param>
        /// <return></return>
        public static DialogueSystem instance;

        public bool isRunningConversation => conversationManager.isRunning;
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                Initialize();
            }
            else
                DestroyImmediate(gameObject);
        }
        bool _initialized = false;
        private void Initialize()
        {
            if (_initialized)
                return;
            architect = new TextArchitect(containerDialogue.dialogueText);
            conversationManager = new ManagerConversation(architect);
        }
        public void ShowSpeakerName(string speakerName = "") => containerDialogue.containerName.Show(speakerName);
        public void HideSpeakerName() => containerDialogue.containerName.Hide();
        public void Say(string speaker, string dialogue)
        {
            List<string> conversation = new List<string>() { $"{speaker} \"{dialogue}\"" };
            Say(conversation);
        }
        public void Say(List<string> conversation)
        {
            conversationManager.StartConversation(conversation);
        }
    }
}
