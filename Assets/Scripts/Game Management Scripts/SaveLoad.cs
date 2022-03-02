using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveLoad
{

    // Player Variables
    public string name;
    public string title;

    public uint currentLocation;

    public List<uint> _itemKeys;
    public List<uint> _itemValues;
    public List<uint> _countKeys;
    public List<uint> _countValues;

    public List<uint> _currentQuests;
    public List<uint> _completedQuests;

    public List<uint> _unlockedSkills;
    public List<bool> _skillActive;

    public uint weapon;
    public uint head, chest, legs, feet, hands;
    public uint location;
    public uint level, exp, totalExp, expToLevel, skillPoints, currentWeight, maxWeight;
    public int health, stamina, mana, gold;
    public int strength, agility, intelligence, luck;

    public void SavePlayer(Player player)
    {
        _itemKeys = new List<uint>();
        _itemValues = new List<uint>();
        _countKeys = new List<uint>();
        _countValues = new List<uint>();
        _currentQuests = new List<uint>();
        _completedQuests = new List<uint>();
        _unlockedSkills = new List<uint>();
        _skillActive = new List<bool>();

        name = player.GetName();
        title = player.GetTitle();

        currentLocation = player.GetLocation().GetID();

        player.GetInventory().OnBeforeSerialize();
        _itemKeys = player.GetInventory().GetItemKeys();
        _itemValues = player.GetInventory().GetItemValuesAsID();
        _countKeys = player.GetInventory().GetCountKeys();
        _countValues = player.GetInventory().GetCountValues();
        player.GetInventory().OnAfterDeserialize();

        player.GetQuestList().ForEach(x => _currentQuests.Add(x.GetID()));
        player.GetCompletedQuests().ForEach(x => _completedQuests.Add(x.GetID()));

        foreach (Skill skill in player.GetSkills())
        {
            _unlockedSkills.Add(skill.GetID());
            _skillActive.Add(skill.IsActive());
        }

        weapon = player.GetWeapon().GetID();
        head = player.GetHead().GetID();
        chest = player.GetChest().GetID();
        legs = player.GetLegs().GetID();
        feet = player.GetFeet().GetID();
        hands = player.GetHands().GetID();

        location = player.GetLocation().GetID();

        level = player.GetLevel();
        exp = player.GetExp();
        totalExp = player.GetTotalExp();
        expToLevel = player.GetToLevelExp();
        skillPoints = player.GetSkillPoints();
        maxWeight = player.GetMaxWeight();

        health = player.GetHealth();
        stamina = player.GetStamina();
        mana = player.GetMana();

        strength = player.GetStrength(false);
        agility = player.GetAgility(false);
        intelligence = player.GetIntelligence(false);
        luck = player.GetLuck();

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

        Player player = new Player(name, Engine.LocationDictionary[location], inventory);
        player.SetTitle(title);

        player.SetLocation(Engine.LocationDictionary[location]);

        foreach (uint id in _currentQuests)
            player.AddQuest(Engine.QuestDictionary[id]);
        foreach(uint id in _completedQuests)
        {
            player.AddCompletedQuest(Engine.QuestDictionary[id]);
            Engine.QuestDictionary[id].SetCompletion(true);
        }
        for(int i = 0; i < Mathf.Min(_skillActive.Count, _unlockedSkills.Count); i++)
        {
            Engine.SkillDictionary[_unlockedSkills[i]].SetActive(_skillActive[i]);
            player.AddSkill(Engine.SkillDictionary[_unlockedSkills[i]]);
            GameObject.Find("GameManager").GetComponent<Engine>().AddSkill(Engine.SkillDictionary[_unlockedSkills[i]]);
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
        player.SetSkillPoints(skillPoints);

        player.SetHealth(health);
        player.SetStamina(stamina);
        player.SetMana(mana);

        player.SetStrength(strength);
        player.SetAgility(agility);
        player.SetIntelligence(intelligence);
        player.SetLuck(luck);

        player.SetGold(gold);

        return player;
    }


    // NPC Variables

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


    // Store Variables

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


    // Boss Variables

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


    // Dungeon Variables

    public List<uint> _dungeonIDs;
    public List<int> _dungeonClearedFloorCount;
    public List<bool> _dungeonIsCleared;

    public void SaveDungeons()
    {
        _dungeonIDs = new List<uint>();
        _dungeonClearedFloorCount = new List<int>();
        _dungeonIsCleared = new List<bool>();

        foreach(KeyValuePair<uint, Dungeon> kvp in Engine.DungeonDictionary)
        {
            _dungeonIDs.Add(kvp.Value.GetID());
            _dungeonClearedFloorCount.Add(kvp.Value.GetClearedFloorCount());
            _dungeonIsCleared.Add(kvp.Value.IsCleared());
        }
    }

    public void LoadDungeons()
    {
        if (_dungeonIDs != null)
        {
            for (int i = 0; i < _dungeonIDs.Count; i++)
            {
                Engine.DungeonDictionary[_dungeonIDs[i]].SetClearedFloors(_dungeonClearedFloorCount[i]);
                Engine.DungeonDictionary[_dungeonIDs[i]].SetCleared(_dungeonIsCleared[i]);
            }
        }
    }
}
