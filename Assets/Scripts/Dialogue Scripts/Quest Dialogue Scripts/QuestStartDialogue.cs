using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Start Quest Dialogue", menuName = "Dialogues/Quest Dialogues/Start Quest")]
public class QuestStartDialogue : QuestDialogue
{

    protected QuestStartDialogue() : base(QuestDialogueType.start) { }

    [Header("Quest Not Started Dialogue")]

    [SerializeField] Dialogue _notStartedDialogue;

    /*
    [SerializeField] string _playerLineNotStartedObjectText;
    [TextArea(3, 5)][SerializeField] string _playerLineNotStartedResponseText;

    [TextArea(3, 5)] [SerializeField] string npcNotStartedLine;

    [SerializeField] AcceptQuestDialogue playerAcceptQuestDialogue;
    [SerializeField] DenyQuestDialogue playerDenyQuestDialogue;
    */

    [Header("Quest Incomplete Dialogue")]

    [SerializeField] Dialogue _incompleteDialogue;

    /*
    [SerializeField] string _playerLineIncompletedObjectText;
    [TextArea(3, 5)] [SerializeField] string _playerLineIncompletedResponseText;

    [TextArea(3, 5)] [SerializeField] string npcIncompletedLine;

    [SerializeField] List<Dialogue> playerIncompletedDialogues;
    */
    [Header("Quest Complete Dialogue")]

    [SerializeField] Dialogue _completeDialogue;

    /*
    [SerializeField] string _playerLineCompletedObjectText;
    [TextArea(3, 5)] [SerializeField] string _playerLineCompletedResponseText;

    [TextArea(3, 5)] [SerializeField] string npcCompletedLine;
    
    [SerializeField] List<Dialogue> playerCompletedDialogues;
    */

    public Dialogue GetNotStartedDialogue() { return _notStartedDialogue; }
    public Dialogue GetIncompleteDialogue() { return _incompleteDialogue; }
    public Dialogue GetCompleteDialogue() { return _completeDialogue; }
}
