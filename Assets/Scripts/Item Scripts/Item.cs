using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[CreateAssetMenu(fileName = "New Base Item", menuName = "Items/Base Item", order = 0)]
public class Item : ScriptableObject
{

    public enum ItemType
    {
        baseItem,
        weapon,
        armor,
        consumable
    };

    [SerializeField] ItemType itemType;
    [SerializeField] Sprite sprite;
    [SerializeField] uint id;
    [SerializeField] new string name;
    [TextArea(3, 5)]
    [SerializeField] string description;
    [SerializeField] float weight;
    [SerializeField] uint value;
    
    

    public Item() { }
    public Item(uint setID) { id = setID; }

    public Sprite GetSprite() { return sprite; }
    public string GetName() { return name; }
    public string GetDescription() { return description; }
    public float GetWeight() { return weight; }
    public uint GetValue() { return value; }
    public uint GetID() { return id; }

    public void SetName(string name) { this.name = name; }
    public void SetDescription(string description) { this.description = description; }
    public void SetWeight(float weight) { this.weight = weight; }
    public void SetValue(uint value) { this.value = value; }
    public void SetID(uint id) { this.id = id; }

    public bool IsBaseItem() { return (ItemType.baseItem == itemType); }
    public bool IsWeapon() { return (ItemType.weapon == itemType); }
    public bool IsArmor() { return (ItemType.armor == itemType); }
    public bool IsConsumable() { return (ItemType.consumable == itemType); }

    public override string ToString()
    {
        return GetName();
    }

    public static bool operator ==(Item itemA, Item itemB) { return (itemA.GetID() == itemB.GetID()); }
    public static bool operator !=(Item itemA, Item itemB) { return (itemA.GetID() != itemB.GetID()); }
    public static bool operator <=(Item itemA, Item itemB) { return (itemA.GetID() <= itemB.GetID()); }
    public static bool operator >=(Item itemA, Item itemB) { return (itemA.GetID() >= itemB.GetID()); }
    public static bool operator >(Item itemA, Item itemB) { return (itemA.GetID() > itemB.GetID()); }
    public static bool operator <(Item itemA, Item itemB) { return (itemA.GetID() < itemB.GetID()); }
}
