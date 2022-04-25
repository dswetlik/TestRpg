using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    public uint weapon;
    public uint head, chest, legs, feet, hands;
    public uint location;
    public uint level, exp, totalExp, expToLevel, skillPoints;
    public float currentWeight, maxWeight;
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

        Player player;

        try
        {
            player = new Player(name, Engine.LocationDictionary[location], inventory);
            player.SetLocation(Engine.LocationDictionary[location]);
            if (Engine.LocationDictionary[location].GetLocationType() == Location.LocationType.Dungeon)
                GameObject.Find("GameManager").GetComponent<DungeonManager>().EnterDungeon(true, Engine.LocationDictionary[location] as Dungeon);
        }
        catch(KeyNotFoundException)
        {
            Debug.LogErrorFormat(string.Format("Location ID {0} not found! Spawning player at ID 0.", location));
            player = new Player(name, Engine.LocationDictionary[0], inventory);
            player.SetLocation(Engine.LocationDictionary[0]);
            location = 0;
        }

        player.SetTitle(title);

        

        foreach (uint id in _currentQuests.ToList<uint>())
        {
            try
            {
                player.AddQuest(Engine.QuestDictionary[id]);
            }
            catch(KeyNotFoundException)
            {
                Debug.LogErrorFormat("Quest ID {0} not found! Removing.", id);
                _currentQuests.Remove(id);

            }
        }
        foreach(uint id in _completedQuests.ToList<uint>())
        {
            try
            {
                player.AddCompletedQuest(Engine.QuestDictionary[id]);
                Engine.QuestDictionary[id].SetCompletion(true);
            }
            catch (KeyNotFoundException)
            {
                Debug.LogErrorFormat("Quest ID {0} not found! Removing.", id);
                _completedQuests.Remove(id);
            }
        }
        foreach(uint id in _unlockedSkills)
        {
            try
            {
                player.AddSkill(Engine.SkillDictionary[id]);
                GameObject.Find("GameManager").GetComponent<Engine>().AddSkill(Engine.SkillDictionary[id]);
            }
            catch (KeyNotFoundException)
            {
                Debug.LogErrorFormat("Skill ID {0} not found! Removing.", id);
            }
        }

        if (weapon != Engine.NULL_WEAPON.GetID())
        {
            try
            {
                player.EquipWeapon((Weapon)Engine.ItemDictionary[weapon]);
            }
            catch(KeyNotFoundException)
            {
                Debug.LogErrorFormat("Item ID {0} not found! Removing.", weapon);
                weapon = Engine.NULL_WEAPON.GetID();
            }
        }
        if (head != Engine.NULL_ARMOR.GetID())
        {
            try
            {
                player.EquipArmor((Armor)Engine.ItemDictionary[head]);
            }
            catch(KeyNotFoundException)
            {
                Debug.LogErrorFormat("Item ID {0} not found! Removing.", head);
                head = Engine.NULL_ARMOR.GetID();
            }
        }
        if (chest != Engine.NULL_ARMOR.GetID())
        {
            try
            {
                player.EquipArmor((Armor)Engine.ItemDictionary[chest]);
            }
            catch (KeyNotFoundException)
            {
                Debug.LogErrorFormat("Item ID {0} not found! Removing.", chest);
                chest = Engine.NULL_ARMOR.GetID();
            }
        }
        if (legs != Engine.NULL_ARMOR.GetID())
        {
            try
            {
                player.EquipArmor((Armor)Engine.ItemDictionary[legs]);
            }
            catch (KeyNotFoundException)
            {
                Debug.LogErrorFormat("Item ID {0} not found! Removing.", legs);
                legs = Engine.NULL_ARMOR.GetID();
            }
        }
        if (feet != Engine.NULL_ARMOR.GetID())
        {
            try
            {
                player.EquipArmor((Armor)Engine.ItemDictionary[feet]);
            }
            catch (KeyNotFoundException)
            {
                Debug.LogErrorFormat("Item ID {0} not found! Removing.", feet);
                feet = Engine.NULL_ARMOR.GetID();
            }
        }
        if (hands != Engine.NULL_ARMOR.GetID())
        {
            try
            {
                player.EquipArmor((Armor)Engine.ItemDictionary[hands]);
            }
            catch (KeyNotFoundException)
            {
                Debug.LogErrorFormat("Item ID {0} not found! Removing.", hands);
                hands = Engine.NULL_ARMOR.GetID();
            }
        }

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
            if (_npcHasGivenQuest[i])
            {
                try
                {
                    Engine.NPCDictionary[_npcIDs[i]].SetHasGivenQuest(_npcHasGivenQuest[i]);
                    Engine.NPCDictionary[_npcIDs[i]].SetGivenQuest(Engine.QuestDictionary[_npcGivenQuests[i]]);
                }
                catch(KeyNotFoundException)
                {
                    Debug.LogErrorFormat("Either NPC ID {0} or Quest ID {1} not found! Removing.");
                }
            }
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
        {
            try
            {
                Engine.StoreDictionary[_storeIDs[i]].ClearInventory();
            }
            catch(KeyNotFoundException)
            {
                Debug.LogErrorFormat("Store ID {0} not found!", _storeIDs[i]);
            }
        }
        for (int i = 0; i < _storeIDs.Count; i++)
        {
            try
            {
                Engine.StoreDictionary[_storeIDs[i]].AddItem(Engine.ItemDictionary[_storeItemIDs[i]]);
            }
            catch(KeyNotFoundException)
            {
                Debug.LogErrorFormat("Either Store ID {0} or Item ID {1} not found!", _storeIDs[i], _storeItemIDs[i]);
            }
        }
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
            try
            {
                ((BossEnemy)Engine.EnemyDictionary[_bossEnemyIDs[i]]).SetHasBeenDefeated(_bossEnemyHasBeenDefeated[i]);
            }
            catch(KeyNotFoundException)
            {
                Debug.LogErrorFormat("Boss Enemy ID {0} not found; Skipping.", _bossEnemyIDs[i]);
            }
        }
    }


    // Location Variables

    public List<uint> _locationIDs;
    public List<bool> _locationIsCleared;

    public void SaveLocations()
    {
        _locationIDs = new List<uint>();
        _locationIsCleared = new List<bool>();

        foreach(KeyValuePair<uint, Location> kvp in Engine.LocationDictionary)
        {
            _locationIDs.Add(kvp.Value.GetID());
            if (kvp.Value.GetLocationType() == Location.LocationType.Dungeon)
                _locationIsCleared.Add((kvp.Value as Dungeon).IsCleared());
            else
                _locationIsCleared.Add(false);
        }
    }

    public void LoadLocations()
    {
        if (_locationIDs != null)
        {
            for (int i = 0; i < _locationIDs.Count; i++)
            {
                try
                {
                    if(Engine.LocationDictionary[_locationIDs[i]].GetLocationType() == Location.LocationType.Dungeon)
                        (Engine.LocationDictionary[_locationIDs[i]] as Dungeon).SetCleared(_locationIsCleared[i]);
                }
                catch(KeyNotFoundException)
                {
                    Debug.LogErrorFormat("Location ID {0} not found; Skipping.", _locationIDs[i]);
                }
            }
        }
    }

    // Event Variables

    public List<uint> _eventIDs;
    public List<bool> _eventIsComplete;

    public void SaveEvents()
    {
        _eventIDs = new List<uint>();
        _eventIsComplete = new List<bool>();

        foreach(KeyValuePair<uint, StartEvent> kvp in EventManager.EventDictionary)
        {
            _eventIDs.Add(kvp.Value.GetID());
            _eventIsComplete.Add(kvp.Value.IsComplete());
        }
    }

    public void LoadEvents()
    {
        if (_eventIDs != null)
        {
            for (int i = 0; i < _eventIDs.Count; i++)
            {
                try
                {
                    EventManager.EventDictionary[_eventIDs[i]].SetComplete(_eventIsComplete[i]);
                }
                catch (KeyNotFoundException)
                {
                    Debug.LogErrorFormat("Event ID {0} not found!", _eventIDs[i]);
                }
            }
        }
    }
}
