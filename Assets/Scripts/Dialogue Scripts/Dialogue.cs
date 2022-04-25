using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue/Base Dialogue", order = 4)]
public abstract class Dialogue : ScriptableObject
{

    public enum DialogueType
    {
        start, //
        conversation, //
        merchant,
        quest, //
        purchase, 
        battle
    }

    [SerializeField] DialogueType dialogueType;

    public Dialogue(DialogueType dialogueType) { SetDialogueType(dialogueType); }

    public DialogueType GetDialogueType() { return dialogueType; } 
    protected void SetDialogueType(DialogueType dialogueType) => this.dialogueType = dialogueType;
    /*
    [TextArea(3,5)][SerializeField] string npcLine;
    
    [SerializeField] List<string> dialogueTexts;
    [SerializeField] List<Dialogue> dialogueObjects;
    [SerializeField] List<Sprite> dialogueSprites;
    
    public string GetNPCLine() { return npcLine; }
    public List<string> GetDialogueOptions() { return dialogueTexts; }
    public List<Dialogue> GetDialogueAnswers() { return dialogueObjects; }
    public List<Sprite> GetDialogueSprites() { return dialogueSprites; }
    public DialogueType GetDialogueType() { return dialogueType; }
    */
}
