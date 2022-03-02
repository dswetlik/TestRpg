using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inn Dialogue", menuName = "Dialogue/Inn Dialogue")]
public class InnDialogue : Dialogue
{

    [SerializeField] int cost;
    [SerializeField] int attributeRegen;

    public int GetCost() { return cost; }
    public int GetAttributeRegen() { return attributeRegen; }

    [SerializeField] string insufficientFundsResponse;

    [SerializeField] List<string> insufficientFundsDialogueTexts;
    [SerializeField] List<Dialogue> insufficientFundsDialogueObjects;
    [SerializeField] List<Sprite> insufficientFundsDialogueSprites;

    public string GetInsufficientFundsResponse() { return insufficientFundsResponse; }

    public List<string> GetInsufficientFundsDialogueTexts() { return insufficientFundsDialogueTexts; }
    public List<Dialogue> GetInsufficientFundsDialogueObjects() { return insufficientFundsDialogueObjects; }
    public List<Sprite> GetInsufficientFundsDialogueSprites() { return insufficientFundsDialogueSprites; }

    [SerializeField] string sufficientFundsResponse;

    public string GetSufficientFundsResponse() { return sufficientFundsResponse; }

}
