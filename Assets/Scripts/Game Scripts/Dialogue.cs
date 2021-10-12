using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue/Base Dialogue", order = 4)]
public class Dialogue : ScriptableObject
{

    public enum DialogueType
    {
        start,
        conversation,
        merchant,
        quest,
        inn,
        battle
    }

    [TextArea(3,5)][SerializeField] string npcLine;
    [SerializeField] DialogueType dialogueType;

    [SerializeField] List<string> dialogueOptions;
    [SerializeField] List<Dialogue> dialogueAnswers;
    [SerializeField] List<Sprite> dialogueSprites;
    
    public string GetNPCLine() { return npcLine; }
    public List<string> GetDialogueOptions() { return dialogueOptions; }
    public List<Dialogue> GetDialogueAnswers() { return dialogueAnswers; }
    public List<Sprite> GetDialogueSprites() { return dialogueSprites; }
    public DialogueType GetDialogueType() { return dialogueType; }

}
