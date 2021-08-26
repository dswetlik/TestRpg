using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Status Effect", menuName = "Status Effect")]
public class StatusEffect : ScriptableObject
{
    public enum StatusEffectType
    {
        heal,
        bleed,
        burn,
        freeze,
        poison,
        stun,
        defence,
        speed
    }

    [SerializeField] StatusEffectType statusEffectType;
    [SerializeField] new string name;
    [TextArea(3,5)][SerializeField] string description;
    [Range(0,1)][SerializeField] float hitChance;
    [SerializeField] int turnAmount;
    [SerializeField] bool isNegative;
    [SerializeField] bool isPercentage;
    [SerializeField] float statChange;
    [SerializeField] Sprite sprite;

    public StatusEffectType GetStatusEffectType() { return statusEffectType; }
    public string GetName() { return name; }
    public string GetDescription() { return description; }
    public float GetHitChance() { return hitChance; }
    public int GetTurnAmount() { return turnAmount; }
    public bool IsNegative() { return isNegative; }
    public bool IsPercentage() { return isPercentage; }
    public float GetStatChange() { return statChange; }
    public Sprite GetSprite() { return sprite; }

    public void DecrementTurnCount() { Debug.Log("Decrementing"); turnAmount -= 1; }

}
