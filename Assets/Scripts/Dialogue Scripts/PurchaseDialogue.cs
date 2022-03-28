using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PurchaseDialogue : Dialogue, IPlayerDialogue
{
    public enum PurchaseDialogueType
    {
        item,
        inn
    }

    protected PurchaseDialogue(PurchaseDialogueType purchaseDialogueType) : base(DialogueType.purchase) { this.purchaseDialogueType = purchaseDialogueType; }


    [SerializeField] PurchaseDialogueType purchaseDialogueType;

    [SerializeField] string _playerLineObjectText;
    [TextArea(3, 5)][SerializeField] string _playerLineResponseText;

    public string GetPlayerLineObjectText() { return _playerLineObjectText; }
    public string GetPlayerLineResponseText() { return _playerLineResponseText; }

    
    [SerializeField] int cost;

    [TextArea(3, 5)] [SerializeField] string insufficientFundsResponse;
    [SerializeField] List<Dialogue> insufficientFundsDialogues;

    [TextArea(3, 5)] [SerializeField] string sufficientFundsResponse;
    [SerializeField] List<Dialogue> sufficientFundsDialogues;

    public PurchaseDialogueType GetPurchaseDialogueType() { return purchaseDialogueType; }
    public int GetCost() { return cost; }

    public string GetNPCLine(bool sufficientFunds) { return sufficientFunds ? sufficientFundsResponse : insufficientFundsResponse; }

    public List<Dialogue> GetPlayerResponses(bool sufficientFunds) { return sufficientFunds ? sufficientFundsDialogues : insufficientFundsDialogues; }

}
