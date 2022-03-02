using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class Consumable : Item
{
    public enum ConsumableType
    {
        potion,
        edible,
        skillBook
    };

    [SerializeField] ConsumableType consumableType;
    public ConsumableType GetConsumableType() { return consumableType; }
    public void SetConsumableType(ConsumableType setConsumableType) { consumableType = setConsumableType; }

    public abstract void UseItem(Player player);
}