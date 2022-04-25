using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest Status Dialogue", menuName = "Dialogues/Quest Dialogues/Quest Status/Incomplete", order = 3)]
public class QuestIncompleteDialogue : QuestStatusDialogue, IPlayerDialogue, INPCDialogue, IRespondable
{

    protected QuestIncompleteDialogue() : base(QuestStatusType.incomplete) { }

    [SerializeField] string _playerLineObjectText;
    [TextArea(3, 5)] [SerializeField] string _playerLineResponseText;
    [TextArea(3, 5)] [SerializeField] string _npcLine;
    [SerializeField] List<Dialogue> _playerResponses;

    public string GetPlayerLineObjectText() { return _playerLineObjectText; }
    public string GetPlayerLineResponseText() { return _playerLineResponseText; }

    public string GetNPCLine() { return _npcLine; }

    public List<Dialogue> GetPlayerResponses() { return _playerResponses; }

}
