using System.Collections;
using System.Collections.Generic;
using DIALOGUE;
using UnityEngine;
namespace TESTING
{
    public class TestConversation : MonoBehaviour
    {
        [SerializeField] private TextAsset fileToRead = null;
        // Start is called before the first frame update
        void Start()
        {
            StartConversation();
        }

        void StartConversation()
        {
            List<string> lines = FileManager.ReadTextAsset(fileToRead);
            foreach (string line in lines)
            {
                Debug.Log($"Segmenting line '{line}'");
                DIALOGUE_LINE dlline = DialogueParser.Parse(line);
                int i = 0;
                foreach (DIALOGUE_DATA.DIALOGUE_SEGMENT segment in dlline.dialogue.segments)
                {
                    Debug.Log($"Segment [{i++}] = '{segment.dialogue}' [signal={segment.startSignal.ToString()} {(segment.signalDelay > 0 ? $"{segment.signalDelay}" : $"")}]");
                }
            }
            DialogueSystem.instance.Say(lines);
        }
    }
}


