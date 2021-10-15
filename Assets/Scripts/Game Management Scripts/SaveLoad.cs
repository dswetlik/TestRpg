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

    public List<uint> _currentQuests;
    public List<uint> _completedQuests;

    public List<uint> _unlockedStaminaSkills;
    public List<uint> _unlockedManaSkills;
    public List<uint> _unlockedPassiveSkills;

    public uint weapon;
    public uint head, chest, legs, feet, hands;
    public uint level, exp, totalExp, expToLevel, skillPoints, currentWeight, maxWeight;
    public int health, stamina, mana, maxHealth, maxStamina, maxMana, staminaRegen, manaRegen, gold;

    public void SavePlayer(Player player)
    {
        _itemKeys = new List<uint>();
        _itemValues = new List<uint>();
        _countKeys = new List<uint>();
        _countValues = new List<uint>();
        _currentQuests = new List<uint>();
        _completedQuests = new List<uint>();
        _unlockedStaminaSkills = new List<uint>();
        _unlockedManaSkills = new List<uint>();
        _unlockedPassiveSkills = new List<uint>();

        name = player.GetName();
        currentLocation = player.GetLocation().GetID();

        player.GetInventory().OnBeforeSerialize();
        _itemKeys = player.GetInventory().GetItemKeys();
        _itemValues = player.GetInventory().GetItemValuesAsID();
        _countKeys = player.GetInventory().GetCountKeys();
        _countValues = player.GetInventory().GetCountValues();
        player.GetInventory().OnAfterDeserialize();

        player.GetQuestList().ForEach(x => _currentQuests.Add(x.GetID()));
        player.GetCompletedQuests().ForEach(x => _completedQuests.Add(x.GetID()));

        player.GetActiveStaminaSkills().ForEach(x => _unlockedStaminaSkills.Add(x.GetID()));
        player.GetActiveManaSkills().ForEach(x => _unlockedManaSkills.Add(x.GetID()));
        player.GetPassiveSkills().ForEach(x => _unlockedPassiveSkills.Add(x.GetID()));

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

        health = player.GetHealth();
        stamina = player.GetStamina();
        mana = player.GetMana();
        maxHealth = player.GetBaseMaxHealth();
        maxStamina = player.GetBaseMaxStamina();
        maxMana = player.GetBaseMaxMana();
        staminaRegen = player.GetBaseStaminaRegen();
        manaRegen = player.GetBaseManaRegen();
        gold = player.GetGold();
    }

    public Player LoadPlayer()
    {
        Inventory inventory = new Inventory();
        inventory.SetItemKeys(_itemKeys);
        inventory.SetItemValuesByID(_itemValues);
        inventory.SetCountKeys(_countKeys);
        inventory.SetCountValues(_countValues);
        inventory.OnAfterDeserialize();

        Player player = new Player(name, Engine.LocationDictionary[0], inventory);

        foreach (uint id in _currentQuests)
            player.AddQuest(Engine.QuestDictionary[id]);
        foreach(uint id in _completedQuests)
        {
            player.AddCompletedQuest(Engine.QuestDictionary[id]);
            Engine.QuestDictionary[id].SetCompletion(true);
        }
        foreach(uint id in _unlockedStaminaSkills)
        {
            player.AddActiveStaminaSkill((ActiveSkill)Engine.SkillDictionary[id]);
            Engine.SkillDictionary[id].SetUnlocked();
            Engine.SkillDictionary[id].UnlockNextSkills();
        }
        foreach (uint id in _unlockedManaSkills)
        {
            player.AddActiveManaSkill((ActiveSkill)Engine.SkillDictionary[id]);
            Engine.SkillDictionary[id].SetUnlocked();
            Engine.SkillDictionary[id].UnlockNextSkills();
        }
        foreach (uint id in _unlockedPassiveSkills)
        {
            player.AddPassiveSkill((PassiveSkill)Engine.SkillDictionary[id]);
            Engine.SkillDictionary[id].SetUnlocked();
            Engine.SkillDictionary[id].UnlockNextSkills();
        }

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

    public List<uint> _npcIDs;
    public List<bool> _npcHasGivenQuest;
    public List<uint> _npcGivenQuests;

    public void SaveNPCs()
    {
        _npcIDs = new List<uint>();
        _npcHasGivenQuest = new List<bool>();
        _npcGivenQuests = new List<uint>();

        foreach(KeyValuePair<uint, NPC> kvp in Engine.NPCDictionary)
        {
            _npcIDs.Add(kvp.Value.GetID());
            _npcHasGivenQuest.Add(kvp.Value.HasGivenQuest());
            if (kvp.Value.HasGivenQuest())
                _npcGivenQuests.Add(kvp.Value.GetGivenQuest().GetID());
            else
                _npcGivenQuests.Add(9999);
        }
    }

    public void LoadNPCs()
    {
        for(int i = 0; i < _npcIDs.Count; i++)
        {
            Engine.NPCDictionary[_npcIDs[i]].SetHasGivenQuest(_npcHasGivenQuest[i]);
            if (_npcHasGivenQuest[i])
                Engine.NPCDictionary[_npcIDs[i]].SetGivenQuest(Engine.QuestDictionary[_npcGivenQuests[i]]);
        }
    }

    public List<uint> _storeIDs;
    public List<uint> _storeItemIDs;

    public void SaveStores()
    {
        _storeIDs = new List<uint>();
        _storeItemIDs = new List<uint>();

        foreach(KeyValuePair<uint, Store> kvp in Engine.StoreDictionary)
        {           
            foreach(Item item in kvp.Value.GetItemList())
            {
                _storeIDs.Add(kvp.Value.GetID());
                _storeItemIDs.Add(item.GetID());
            }
        }
    }

    public void LoadStores()
    {
        for (int i = 0; i < _storeIDs.Count; i++)
            Engine.StoreDictionary[_storeIDs[i]].ClearInventory();

        for (int i = 0; i < _storeIDs.Count; i++)
            Engine.StoreDictionary[_storeIDs[i]].AddItem(Engine.ItemDictionary[_storeItemIDs[i]]);
    }

    public List<uint> _bossEnemyIDs;
    public List<bool> _bossEnemyHasBeenDefeated;

    public void SaveBossEnemies()
    {
        _bossEnemyIDs = new List<uint>();
        _bossEnemyHasBeenDefeated = new List<bool>();

        foreach(KeyValuePair<uint, Enemy> kvp in Engine.EnemyDictionary)
        {
            if(kvp.Value.GetEnemyType() == Enemy.EnemyType.boss)
            {
                _bossEnemyIDs.Add(kvp.Value.GetID());
                _bossEnemyHasBeenDefeated.Add(((BossEnemy)(kvp.Value)).HasBeenDefeated());
            }
        }
    }

    public void LoadBossEnemies()
    {
        for(int i = 0; i < _bossEnemyIDs.Count; i++)
        {
            ((BossEnemy)Engine.EnemyDictionary[_bossEnemyIDs[i]]).SetHasBeenDefeated(_bossEnemyHasBeenDefeated[i]);
        }
    }
}
