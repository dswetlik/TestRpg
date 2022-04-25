﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Deny Quest Dialogue", menuName = "Dialogues/Quest Dialogues/Deny Quest")]
public class QuestDenyDialogue : QuestDialogue, IPlayerDialogue, INPCDialogue, IRespondable
{

    protected QuestDenyDialogue() : base(QuestDialogueType.deny) { }

    [SerializeField] string _playerLineObjectText;
    [TextArea(3, 5)][SerializeField] string _playerLineResponseText;
    [TextArea(3, 5)] [SerializeField] string _npcLine;
    [SerializeField] List<Dialogue> _playerResponses;

    public string GetPlayerLineObjectText() { return _playerLineObjectText; }
    public string GetPlayerLineResponseText() { return _playerLineResponseText; }

    public string GetNPCLine() { return _npcLine; }

    public List<Dialogue> GetPlayerResponses() { return _playerResponses; }

}
