using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Edible", menuName = "Items/Consumables/Edible")]
public class Edible : Consumable
{

    public enum EdibleType
    {
        food,
        drink
    }

    [SerializeField] EdibleType edibleType;
    [SerializeField] int effectChange;

    public int GetEffectChange() { return effectChange; }

    public override void UseItem(Player player)
    {
        switch(edibleType)
        {
            case EdibleType.food:
                player.ChangeHealth(effectChange);
                break;
            case EdibleType.drink:
                player.ChangeStamina(effectChange);
                player.ChangeMana(effectChange);
                break;
        }
    }

}
