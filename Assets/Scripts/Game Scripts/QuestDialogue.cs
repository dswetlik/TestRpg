using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest Dialogue", menuName = "Dialogue/Quest Dialogue")]
public class QuestDialogue : Dialogue
{

    public enum QuestDialogueType
    {
        start,
        accept,
        reject
    }

    [SerializeField] QuestDialogueType questDialogueType;
    [SerializeField] Quest quest;

    [TextArea(3, 5)][SerializeField] string notStartedResponse;
    [TextArea(3, 5)][SerializeField] string notCompleteResponse;
    [TextArea(3, 5)][SerializeField] string completeResponse;

    public QuestDialogueType GetQuestDialogueType() { return questDialogueType; }
    public Quest GetQuest() { return quest; }
    
    public string GetNotStartedResponse() { return notStartedResponse; }
    public string GetNotCompleteResponse() { return notCompleteResponse; }
    public string GetCompleteResponse() { return completeResponse; }

}
