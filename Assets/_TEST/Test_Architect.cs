using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DIALOGUE;

namespace TESTING
{
    public class Test_Architect : MonoBehaviour
    {
        DialogueSystem ds;
        TextArchitect architect;

        string[] lines = new string[5]
        {
            "In a world devastated by a viral outbreak, a lone survivor must navigate through the ruins to find a cure and save humanity.",
            "A young woman defies societal norms and embarks on a dangerous journey to fulfill her dreams, challenging the status quo along the way.",
            "An ordinary man discovers a mysterious artifact that grants him extraordinary powers, but he must decide whether to use them for good or succumb to temptation.",
            "A group of unlikely allies band together to pull off an elaborate heist, facing unexpected twists and betrayals that test their loyalties.",
            "Set in a dystopian future, a courageous rebellion rises against a tyrannical regime, fighting for freedom and a chance to rebuild their world."
        };
        // Start is called before the first frame update
        void Start()
        {
            ds = DialogueSystem.instance;
            architect = new TextArchitect(ds.containerDialogue.dialogueText);
            architect.buildMethod = TextArchitect.BuildMethod.fade;
            architect.speed = 0.5f;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (architect.isBuilding)
                {
                    if (!architect.isInHurry)
                        architect.isInHurry = true;
                    else
                        architect.ForceComplete();
                }
                else
                    architect.Build(lines[Random.Range(0, lines.Length)]);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                architect.Append(lines[Random.Range(0, lines.Length)]);
            }
        }
    }
}
