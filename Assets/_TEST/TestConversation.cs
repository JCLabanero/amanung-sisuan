using System.Collections;
using System.Collections.Generic;
using DIALOGUE;
using UnityEngine;
namespace TESTING
{
    public class TestConversation : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            StartConversation();
        }

        void StartConversation()
        {
            List<string> lines = FileManager.ReadTextAsset("testFile");
            DialogueSystem.instance.Say(lines);
        }
    }
}


