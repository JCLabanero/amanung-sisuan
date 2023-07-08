using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DIALOGUE
{
    public class ManagerConversation
    {
        private DialogueSystem dialogueSystem => DialogueSystem.instance;
        private Coroutine process = null;
        public bool isRunning => process != null;
        private TextArchitech architech = null;
        public ManagerConversation(TextArchitech architech)
        {
            this.architech = architech;
        }
        public void StartConversation(List<string> conversation)
        {
            StopConversation();

            process = dialogueSystem.StartCoroutine(RunningConversation(conversation));
        }
        IEnumerator RunningConversation(List<string> conversation)
        {
            for (int i = 0; i < conversation.Count; i++)
            {
                if (string.IsNullOrWhiteSpace(conversation[i]))
                    continue;
                DIALOGUE_LINE line = DialogueParser.Parse(conversation[i]);
                if (line.hasDialogue)
                    yield return Line_RunDialogue(line);
                if (line.hasCommands)
                    yield return Line_RunDialogue(line);
                yield return new WaitForSeconds(1);
            }
        }
        IEnumerator Line_RunDialogue(DIALOGUE_LINE line)
        {
            if (line.hasSpeaker)
                dialogueSystem.ShowSpeakerName(line.speaker);
            else
                dialogueSystem.HideSpeakerName();
            architech.Build(line.dialogue);
            while (architech.isBuilding)
                yield return null;
        }
        IEnumerator Line_RunCommands(DIALOGUE_LINE line)
        {
            Debug.Log(line.commands);
            yield return null;
        }
        public void StopConversation()
        {
            if (!isRunning)
                return;
        }
    }
}