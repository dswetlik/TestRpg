using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveLoad
{

    // Player Variables
    public string name;
    public uint currentLocation;

    public List<uint> _itemKeys;
    public List<uint> _itemValues;
    public List<uint> _countKeys;
    public List<uint> _countValues;

    public uint weapon;
    public uint head, chest, legs, feet, hands;
    public uint level, exp, totalExp, expToLevel, skillPoints, currentWeight, maxWeight;
    public int energy, hunger, thirst, maxEnergy, maxHunger, maxThirst, health, stamina, mana, maxHealth, maxStamina, maxMana, staminaRegen, manaRegen, gold;

    public Player LoadPlayer()
    {
        Inventory inventory = new Inventory();
        inventory.SetItemKeys(_itemKeys);
        inventory.SetItemValuesByID(_itemValues);
        inventory.SetCountKeys(_countKeys);
        inventory.SetCountValues(_countValues);
        inventory.OnAfterDeserialize();

        Player player = new Player(name, Engine.LocationDictionary[currentLocation], inventory);
        if (weapon != Engine.NULL_WEAPON.GetID())
            player.EquipWeapon((Weapon)Engine.ItemDictionary[weapon]);
        if (head != Engine.NULL_ARMOR.GetID())
            player.EquipArmor((Armor)Engine.ItemDictionary[head]);
        if (chest != Engine.NULL_ARMOR.GetID())
            player.EquipArmor((Armor)Engine.ItemDictionary[chest]);
        if (legs != Engine.NULL_ARMOR.GetID())
            player.EquipArmor((Armor)Engine.ItemDictionary[legs]);
        if (feet != Engine.NULL_ARMOR.GetID())
            player.EquipArmor((Armor)Engine.ItemDictionary[feet]);
        if (hands != Engine.NULL_ARMOR.GetID())
            player.EquipArmor((Armor)Engine.ItemDictionary[hands]);

        player.SetLevel(level);
        player.SetExp(exp);
        player.SetTotalExp(totalExp);
        player.SetExpToLevel(expToLevel);
        player.SetSkillPoints(skillPoints);
        player.SetMaxWeight(maxWeight);

        player.SetEnergy(energy);
        player.SetHunger(hunger);
        player.SetThirst(thirst);
        player.SetMaxEnergy(maxEnergy);
        player.SetMaxHunger(maxHunger);
        player.SetMaxThirst(maxThirst);
        player.SetHealth(health);
        player.SetStamina(stamina);
        player.SetMana(mana);
        player.SetMaxHealth(maxHealth);
        player.SetMaxStamina(maxStamina);
        player.SetMaxMana(maxMana);
        player.SetStaminaRegen(staminaRegen);
        player.SetManaRegen(manaRegen);
        player.SetGold(gold);

        return player;
    }

    public void SavePlayer(Player player)
    {

        name = player.GetName();
        currentLocation = player.GetLocation().GetID();

        player.GetInventory().OnBeforeSerialize();
        _itemKeys = player.GetInventory().GetItemKeys();
        _itemValues = player.GetInventory().GetItemValuesAsID();
        _countKeys = player.GetInventory().GetCountKeys();
        _countValues = player.GetInventory().GetCountValues();
        player.GetInventory().OnAfterDeserialize();

        weapon = player.GetWeapon().GetID();
        head = player.GetHead().GetID();
        chest = player.GetChest().GetID();
        legs = player.GetLegs().GetID();
        feet = player.GetFeet().GetID();
        hands = player.GetHands().GetID();

        level = player.GetLevel();
        exp = player.GetExp();
        totalExp = player.GetTotalExp();
        expToLevel = player.GetToLevelExp();
        skillPoints = player.GetSkillPoints();
        maxWeight = player.GetMaxWeight();

        energy = player.GetEnergy();
        hunger = player.GetHunger();
        thirst = player.GetThirst();
        maxEnergy = player.GetMaxEnergy();
        maxHunger = player.GetMaxHunger();
        thirst = player.GetMaxThirst();
        health = player.GetHealth();
        stamina = player.GetStamina();
        mana = player.GetMana();
        maxHealth = player.GetMaxHealth();
        maxStamina = player.GetMaxStamina();
        maxMana = player.GetMaxMana();
        staminaRegen = player.GetStaminaRegen();
        manaRegen = player.GetManaRegen();
        gold = player.GetGold();

    }

 
}
