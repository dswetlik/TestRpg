using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "New Quest Conversation", menuName = "Dialogues/Quest Dialogues/Conversation", order = 3)]
public abstract class QuestStatusDialogue : QuestDialogue
{
    
    public enum QuestStatusType
    {
        notStarted,
        incomplete,
        complete
    }

    protected QuestStatusDialogue(QuestStatusType questStatusType) : base(QuestDialogueType.status) { _questStatusType = questStatusType; }

    [SerializeField] QuestStatusType _questStatusType;

    public QuestStatusType GetQuestStatusType() { return _questStatusType; }

}
