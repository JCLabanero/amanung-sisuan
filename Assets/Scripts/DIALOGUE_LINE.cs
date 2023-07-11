using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DIALOGUE
{
    public class DIALOGUE_LINE
    {
        public DIALOGUE_DATA_SPEAKER speaker;
        public DIALOGUE_DATA dialogue;
        public string commands;
        public bool hasDialogue => dialogue.hasDialogue;
        public bool hasCommands => commands != string.Empty;
        public bool hasSpeaker => speaker != null;
        public DIALOGUE_LINE(string speaker, string dialogue, string commands)
        {
            this.speaker = ((string.IsNullOrWhiteSpace(speaker)) ? null : new DIALOGUE_DATA_SPEAKER(speaker));
            this.dialogue = new DIALOGUE_DATA(dialogue);
            this.commands = commands;
        }
    }
}
