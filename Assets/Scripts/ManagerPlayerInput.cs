using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DIALOGUE
{
    public class ManagerPlayerInput : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetAxis("Submit") == 1)
            {
                PromptAdvance();
            }
        }

        public void PromptAdvance()
        {
            DialogueSystem.instance.OnUserPrompt_Next();
        }
    }
}

