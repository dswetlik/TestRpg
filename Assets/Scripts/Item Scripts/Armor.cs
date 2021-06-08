using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Armor", menuName = "Items/Armor", order = 2)]
public class Armor : Item
{

    public enum ArmorType
    {
        head,
        chest,
        legs,
        feet,
        hands
    }

    public enum ArmorClass
    {
        clothing,
        light,
        heavy
    }

    [SerializeField] ArmorType armorType;
    [SerializeField] ArmorClass armorClass;
    [SerializeField] uint rating;

    public Armor(uint id, string name, uint weight, uint value, uint rating) : base(id)
    {
        SetName(name);
        SetWeight(weight);
        SetValue(value);
        SetRating(rating);
        SetID(id);
    }

    public uint GetRating() { return rating; }
    public ArmorType GetArmorType() { return armorType; }
    public ArmorClass GetArmorClass() { return armorClass; }

    public void SetRating(uint rating) { this.rating = rating; }
    public void SetArmorType(ArmorType armorType) { this.armorType = armorType; }
    public void SetArmorClass(ArmorClass armorClass) { this.armorClass = armorClass; }

}
