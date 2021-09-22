using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue/Base Dialogue", order = 4)]
public class Dialogue : ScriptableObject
{

    public enum DialogueType
    {
        basic,
        merchant,
        quest
    }

    [TextArea(3,5)][SerializeField] string npcLine;
    [SerializeField] DialogueType dialogueType;

    [SerializeField] List<string> dialogueOptions;
    [SerializeField] List<Dialogue> dialogueAnswers;
    
    public string GetNPCLine() { return npcLine; }
    public List<string> GetDialogueOptions() { return dialogueOptions; }
    public List<Dialogue> GetDialogueAnswers() { return dialogueAnswers; }
    public DialogueType GetDialogueType() { return dialogueType; }

}
