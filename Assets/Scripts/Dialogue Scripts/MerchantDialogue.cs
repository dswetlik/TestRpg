using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Merchant Dialogue", menuName = "Dialogues/Merchant")]
public class MerchantDialogue : Dialogue, IPlayerDialogue
{

    private MerchantDialogue() : base(DialogueType.merchant) { }

    [SerializeField] string _playerLineObjectText;
    [SerializeField] string _playerLineResponseText;

    public string GetPlayerLineObjectText() { return _playerLineObjectText; }
    public string GetPlayerLineResponseText() { return _playerLineResponseText; }

}
