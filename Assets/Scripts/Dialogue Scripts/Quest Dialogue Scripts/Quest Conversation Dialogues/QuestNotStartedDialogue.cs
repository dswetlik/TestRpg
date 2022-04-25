using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest Status Dialogue", menuName = "Dialogues/Quest Dialogues/Quest Status/Not Started", order = 3)]
public class QuestNotStartedDialogue : QuestStatusDialogue, IPlayerDialogue, INPCDialogue, IRespondable
{

    protected QuestNotStartedDialogue() : base(QuestStatusType.notStarted) { }

    [SerializeField] string _playerLineObjectText;
    [TextArea(3, 5)] [SerializeField] string _playerLineResponseText;
    [TextArea(3, 5)] [SerializeField] string _npcLine;

    [SerializeField] List<Dialogue> _playerResponses;

    public string GetPlayerLineObjectText() { return _playerLineObjectText; }
    public string GetPlayerLineResponseText() { return _playerLineResponseText; }

    public string GetNPCLine() { return _npcLine; }

    public List<Dialogue> GetPlayerResponses() { return _playerResponses; }

}
