using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue", order = 4)]
public class Dialogue : ScriptableObject
{

    public enum DialogueType
    {
        basic,
        merchant,
        quest
    }
    
    [SerializeField] string npcLine;
    [SerializeField] List<string> dialogueOptions;
    [SerializeField] List<Dialogue> dialogueAnswers;
    [SerializeField] DialogueType dialogueType;

    public string GetNPCLine() { return npcLine; }
    public string GetDialogueOption(int optionNumber) { return dialogueOptions[optionNumber]; }
    public Dialogue GetDialogueAnswers(int dialogueNumber) { return dialogueAnswers[dialogueNumber]; }
    public DialogueType GetDialogueType(int dialogueNumber) { return dialogueAnswers[dialogueNumber].dialogueType; }
    public void SetNPCLine(string npcLine) { this.npcLine = npcLine; }
    public void SetDialogOption(string dialogueOption, int dialogueOptionNumber) { dialogueOptions[dialogueOptionNumber] = dialogueOption; }
    public void SetDialogueAnswers(Dialogue dialogue, int dialogueNumber) { dialogueAnswers[dialogueNumber] = dialogue; }

    public void ReadDialogue()
    {

    }
}
