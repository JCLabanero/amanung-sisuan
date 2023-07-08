using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace DIALOGUE
{
    [System.Serializable]
    public class ContainerDialogue
    {
        public GameObject root;
        public ContainerName containerName = new ContainerName();
        public TextMeshProUGUI dialogueText;
    }
}
