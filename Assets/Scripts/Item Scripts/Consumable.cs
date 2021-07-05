using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Items/Consumable", order = 3)]
public class Consumable : Item
{
    public enum ConsumableType
    {
        healthPotion,
        staminaPotion,
        manaPotion,
        food,
        drink
    };

    [SerializeField] ConsumableType consumableType;
    [SerializeField] int statChange;

    public Consumable(uint id, string name, uint weight, uint value, ConsumableType consumableType, int statChange)
    {
        SetID(id);
        SetName(name);
        SetWeight(weight);
        SetValue(value);
        SetConsumableType(consumableType);
        SetStatChange(statChange);
    }

    public ConsumableType GetConsumableType() { return consumableType; }
    public int GetStatChange() { return statChange; }

    public void SetConsumableType(ConsumableType setConsumableType) { consumableType = setConsumableType; }
    public void SetStatChange(int statChange) { this.statChange = statChange; }

    public void UseItem(Player player)
    {
        switch (consumableType)
        {
            case ConsumableType.healthPotion:
                player.ChangeHealth(statChange);
                break;
            case ConsumableType.staminaPotion:
                player.ChangeStamina(statChange);
                break;
            case ConsumableType.manaPotion:
                player.ChangeMana(statChange);
                break;
            case ConsumableType.food:
                player.ChangeHealth(statChange);
                break;
            case ConsumableType.drink:
                
                break;
        }
    }
}