using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inn Dialogue", menuName = "Dialogues/Purchase Dialogues/Inn")]
public class InnDialogue : PurchaseDialogue
{

    protected InnDialogue() : base(PurchaseDialogueType.inn) { }

    [SerializeField] int attributeRegen;

    public int GetAttributeRegen() { return attributeRegen; }

}
