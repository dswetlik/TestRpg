using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Start Dialogue", menuName = "Dialogues/Start")]
public class StartDialogue : Dialogue, INPCDialogue, IRespondable
{

    protected StartDialogue() : base(DialogueType.start) { }

    [TextArea(3,5)][SerializeField] string _npcLine;
    [SerializeField] List<Dialogue> _playerResponses;

    public string GetNPCLine() { return _npcLine; }

    public List<Dialogue> GetPlayerResponses() { return _playerResponses; }

}
