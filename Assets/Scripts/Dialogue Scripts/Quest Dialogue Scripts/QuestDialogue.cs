using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public abstract class QuestDialogue : Dialogue
{

    public enum QuestDialogueType
    {
        start,
        accept,
        deny,
        status
    }

    protected QuestDialogue(QuestDialogueType questDialogueType) : base(DialogueType.quest) { SetQuestDialogueType(questDialogueType); }

    [SerializeField] QuestDialogueType questDialogueType;

    [SerializeField] Quest _quest;

    public QuestDialogueType GetQuestDialogueType() { return questDialogueType; }
    public Quest GetQuest() { return _quest; }

    protected void SetQuestDialogueType(QuestDialogueType questDialogueType) => this.questDialogueType = questDialogueType;

}
