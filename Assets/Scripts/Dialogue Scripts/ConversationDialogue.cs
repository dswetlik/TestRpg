using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Conversation Dialogue", menuName = "Dialogues/Conversation")]
public class ConversationDialogue : Dialogue, IPlayerDialogue, INPCDialogue, IRespondable
{

    protected ConversationDialogue() : base(DialogueType.conversation) { }

    [SerializeField] string _playerLineObjectText;
    [TextArea(3, 5)][SerializeField] string _playerLineResponseText;
    [TextArea(3, 5)][SerializeField] string _npcLine;
    [SerializeField] List<Dialogue> _playerResponses;

    public string GetPlayerLineObjectText() { return _playerLineObjectText; }
    public string GetPlayerLineResponseText() { return _playerLineResponseText; }

    public string GetNPCLine() { return _npcLine; }

    public List<Dialogue> GetPlayerResponses() { return _playerResponses; }

}
