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
        private TextArchitect architect = null;
        private bool userPrompt = false;
        public ManagerConversation(TextArchitect architect)
        {
            this.architect = architect;
            dialogueSystem.onUserPrompt_Next += OnUserPrompt_Next;
        }
        private void OnUserPrompt_Next()
        {
            userPrompt = true;
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
                    yield return Line_RunCommands(line);
            }
        }
        IEnumerator Line_RunDialogue(DIALOGUE_LINE line)
        {
            if (line.hasSpeaker)
                dialogueSystem.ShowSpeakerName(line.speaker.displayName);
            yield return BuildDialogueSegments(line.dialogue);
            yield return WaitForUserInput();
        }
        IEnumerator Line_RunCommands(DIALOGUE_LINE line)
        {
            Debug.Log(line.commands);
            yield return null;
        }
        IEnumerator BuildDialogueSegments(DIALOGUE_DATA line)
        {
            for (int i = 0; i < line.segments.Count; i++)
            {
                DIALOGUE_DATA.DIALOGUE_SEGMENT segment = line.segments[i];
                yield return WaitForDialogueSegmentSignalToBeTriggered(segment);
                yield return BuildDialogue(segment.dialogue, segment.appedText);
            }
        }
        IEnumerator WaitForDialogueSegmentSignalToBeTriggered(DIALOGUE_DATA.DIALOGUE_SEGMENT segment)
        {
            switch (segment.startSignal)
            {
                case DIALOGUE_DATA.DIALOGUE_SEGMENT.StartSignal.C:
                case DIALOGUE_DATA.DIALOGUE_SEGMENT.StartSignal.A:
                    yield return WaitForUserInput();
                    break;
                case DIALOGUE_DATA.DIALOGUE_SEGMENT.StartSignal.WC:
                case DIALOGUE_DATA.DIALOGUE_SEGMENT.StartSignal.WA:
                    yield return new WaitForSeconds(segment.signalDelay);
                    break;
                default:
                    break;
            }
        }
        IEnumerator BuildDialogue(string dialogue, bool append = false)
        {
            if (!append)
                architect.Build(dialogue);
            else
                architect.Append(dialogue);
            while (architect.isBuilding)
            {
                if (userPrompt)
                {
                    if (!architect.isInHurry)
                        architect.isInHurry = true;
                    else
                        architect.ForceComplete();
                    userPrompt = false;
                }
                yield return null;
            }
        }
        public void StopConversation()
        {
            if (!isRunning)
                return;
            dialogueSystem.StopCoroutine(process);
            process = null;
        }
        IEnumerator WaitForUserInput()
        {
            while (!userPrompt)
                yield return null;
            userPrompt = false;
        }
    }
}