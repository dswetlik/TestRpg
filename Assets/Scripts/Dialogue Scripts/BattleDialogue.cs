using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Battle Dialogue", menuName = "Dialogues/Battle Dialogue")]
public class BattleDialogue : Dialogue, IPlayerDialogue, INPCDialogue, IRespondable
{

    protected BattleDialogue() : base(DialogueType.battle) { }

    [SerializeField] string _playerLineObjectText;
    [TextArea(3, 5)][SerializeField] string _playerLineResponseText;
    [TextArea(3, 5)][SerializeField] string _npcLine;
    [SerializeField] List<Dialogue> _playerResponses;

    [SerializeField] Enemy _enemy;

    public string GetPlayerLineObjectText() { return _playerLineObjectText; }
    public string GetPlayerLineResponseText() { return _playerLineResponseText; }

    public string GetNPCLine() { return _npcLine; }

    public List<Dialogue> GetPlayerResponses() { return _playerResponses; }

    public Enemy GetEnemy() { return _enemy; }

}
