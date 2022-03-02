using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[System.Serializable]
public class Player
{

    const float DEFAULT_SPEED = 1.0f;

    string name;
    string title;
    Location currentLocation;
    Inventory inventory;
    Weapon weapon;
    Armor head, chest, legs, feet, hands;
    uint level, exp, totalExp, skillPoints, currentWeight, maxWeight;
    int health, stamina, mana, defense, gold, battleCount;
    int strength, agility, intelligence, luck;
    float speed;

    List<Quest> questList = new List<Quest>();
    List<Quest> completedQuests = new List<Quest>();
    List<StatusEffect> statusEffects = new List<StatusEffect>();
    List<Skill> unlockedSkills = new List<Skill>();

    public Player(string name, Location currentLocation, Inventory inventory, uint level = 1, int strength = 5, int agility = 5, int intelligence = 5, int luck = 0, uint currentWeight = 0)
    {
        this.name = name;
        title = "Newcomer";
        this.currentLocation = currentLocation;
        this.inventory = inventory;

        this.level = level;

        this.strength = strength;
        this.agility = agility;
        this.intelligence = intelligence;
        this.luck = luck;

        maxWeight = (uint)(strength * 25);

        health = GetMaxHealth();
        stamina = GetMaxStamina();
        mana = GetMaxMana();

        speed = DEFAULT_SPEED;
        weapon = Engine.NULL_WEAPON;
        head = Engine.NULL_ARMOR;
        chest = Engine.NULL_ARMOR;
        legs = Engine.NULL_ARMOR;
        feet = Engine.NULL_ARMOR;
        hands = Engine.NULL_ARMOR;
        gold = 0;
        exp = 0;
        totalExp = 0;
        skillPoints = 0;
        battleCount = 0;
    }

    // Player Information

    public string GetName() { return name; }
    public string GetTitle() { return title; }
    public Location GetLocation() { return currentLocation; }

    public Inventory GetInventory() { return inventory; }
    public uint GetCurrentWeight() { return currentWeight; }
    public uint GetMaxWeight() { return (uint)(strength * 2); }
    public int GetGold() { return gold; }

    public uint GetLevel() { return level; }
    public uint GetExp() { return exp; }
    public uint GetTotalExp() { return totalExp; }
    public uint GetToLevelExp() { return level * 10; }
    public uint GetSkillPoints() { return skillPoints; }

    public int GetBattleCount() { return battleCount; }

    public void SetName(string name) { this.name = name; }
    public void SetTitle(string title) { this.title = title; }
    public void SetLocation(Location newLocation) { currentLocation = newLocation; }

    public void SetCurrentWeight(uint currentWeight) { this.currentWeight = currentWeight; }
    public void ChangeGold(int gold) { this.gold += gold; if (gold < 0) gold = 0; }
    public void SetGold(int gold) { this.gold = gold; }

    public void SetLevel(uint level) { this.level = level; }
    public void SetExp(uint exp) { this.exp = exp; }
    public void SetTotalExp(uint totalExp) { this.totalExp = totalExp; }
    public void SetSkillPoints(uint skillPoints) { this.skillPoints = skillPoints; }

    public void AddExp(uint exp)
    {
        this.exp += exp;
        totalExp += exp;
        CheckForLevelUp();
    }

    public void CheckForLevelUp()
    {
        if (exp >= (level * 10))
        {
            exp -= (level * 10);
            level++;
            skillPoints += 5;

            GameObject.Find("GameManager").GetComponent<Engine>().OutputToText(String.Format("You have achieved level {0}!", level));
            if (level == 3)
                GameObject.Find("GameManager").GetComponent<Engine>().StartLevelThreeEvent();
        }
    }

    public void AddBattleCount(int count) { battleCount += count; }

    //**

    // Stats and Attributes

    public int GetStrength(bool modified = true) { return modified ? strength + (int)GetTotalEffect(StatusEffect.StatusEffectType.strength) : strength; }
    public int GetAgility(bool modified = true) { return modified ? agility + (int)GetTotalEffect(StatusEffect.StatusEffectType.agility) : agility; }
    public int GetIntelligence(bool modified = true) { return modified ? intelligence + (int)GetTotalEffect(StatusEffect.StatusEffectType.intelligence) : intelligence; }
    public int GetLuck() { return luck; }

    public void SetStrength(int strength) { this.strength = strength; }
    public void SetAgility(int agility) { this.agility = agility; }
    public void SetIntelligence(int intelligence) { this.intelligence = intelligence; }
    public void SetLuck(int luck) { this.luck = luck; }

    public int GetHealth() { return health; }
    public int GetStamina() { return stamina; }
    public int GetMana() { return mana; }
    public int GetMaxHealth() { return GetStrength(); }
    public int GetMaxStamina() { return GetAgility(); }
    public int GetMaxMana() { return GetIntelligence(); }
    public int GetStaminaRegen() { return (GetAgility() / 5); }
    public int GetManaRegen() { return (GetIntelligence() / 5); }

    public void SetHealth(int health) { this.health = health; }
    public void SetStamina(int stamina) { this.stamina = stamina; }
    public void SetMana(int mana) { this.mana = mana; }

    public void ChangeHealth(int healthChange)
    {
        health += healthChange;
        if (health < 0)
            health = 0;
        if (health > GetMaxHealth())
            health = GetMaxHealth();

        if (health < GetMaxHealth() * 0.1f)
        {
            Debug.Log("Playing Fast Heartbeat");
            GameObject.Find("HeartbeatSlowLoopAudioSource").GetComponent<AudioSource>().Stop();
            if (!GameObject.Find("HeartbeatFastLoopAudioSource").GetComponent<AudioSource>().isPlaying)
                GameObject.Find("HeartbeatFastLoopAudioSource").GetComponent<AudioSource>().Play();
        }
        else if (health < GetMaxHealth() * 0.25f)
        {
            Debug.Log("Playing Slow Heartbeat");
            GameObject.Find("HeartbeatFastLoopAudioSource").GetComponent<AudioSource>().Stop();
            if (!GameObject.Find("HeartbeatSlowLoopAudioSource").GetComponent<AudioSource>().isPlaying)
                GameObject.Find("HeartbeatSlowLoopAudioSource").GetComponent<AudioSource>().Play();
        }
        else
        {
            Debug.Log("Not Playing Heartbeat");
            GameObject.Find("HeartbeatFastLoopAudioSource").GetComponent<AudioSource>().Stop();
            GameObject.Find("HeartbeatSlowLoopAudioSource").GetComponent<AudioSource>().Stop();
        }
    }
    public void ChangeStamina(int staminaChange)
    {
        stamina += staminaChange;
        if (stamina < 0)
            stamina = 0;
        if (stamina > GetMaxStamina())
            stamina = GetMaxStamina();
    }
    public void ChangeMana(int manaChange)
    {
        mana += manaChange;
        if (mana < 0)
            mana = 0;
        if (mana > GetMaxMana())
            mana = GetMaxMana();
    }

    public void RegenAttributes()
    {
        stamina += GetStaminaRegen();
        mana += GetManaRegen();

        if (stamina > GetMaxStamina())
            stamina = GetMaxStamina();
        if (mana > GetMaxMana())
            mana = GetMaxMana();
    }

    //**

    public int GetDefense() {
        int defenseRating = 0;
        if (GetHead() != Engine.NULL_ARMOR)
            defenseRating += (int)GetHead().GetRating(); 
        if (GetChest() != Engine.NULL_ARMOR)
            defenseRating += (int)GetChest().GetRating();
        if (GetLegs() != Engine.NULL_ARMOR)
            defenseRating += (int)GetLegs().GetRating();
        if (GetFeet() != Engine.NULL_ARMOR)
            defenseRating += (int)GetFeet().GetRating();
        if (GetHands() != Engine.NULL_ARMOR)
            defenseRating += (int)GetHands().GetRating();

        return defenseRating;
    }
    public float GetSpeed()
    {
        float baseSpeed = DEFAULT_SPEED;

        if (head != Engine.NULL_ARMOR)
        {
            if (head.GetArmorClass() == Armor.ArmorClass.heavy)
                baseSpeed -= 0.1f - (strength * 0.001f);
            else if (head.GetArmorClass() == Armor.ArmorClass.light)
                baseSpeed -= 0.05f - (agility * 0.0005f);
        }
        if (chest != Engine.NULL_ARMOR)
        {
            if (chest.GetArmorClass() == Armor.ArmorClass.heavy)
                baseSpeed -= 0.1f - (strength * 0.001f);
            else if (chest.GetArmorClass() == Armor.ArmorClass.light)
                baseSpeed -= 0.05f - (agility * 0.0005f);
        }
        if (legs != Engine.NULL_ARMOR)
        {
            if (legs.GetArmorClass() == Armor.ArmorClass.heavy)
                baseSpeed -= 0.1f - (strength * 0.001f);
            else if (legs.GetArmorClass() == Armor.ArmorClass.light)
                baseSpeed -= 0.05f - (agility * 0.0005f);
        }
        if (feet != Engine.NULL_ARMOR)
        {
            if (feet.GetArmorClass() == Armor.ArmorClass.heavy)
                baseSpeed -= 0.1f - (strength * 0.001f);
            else if (feet.GetArmorClass() == Armor.ArmorClass.light)
                baseSpeed -= 0.05f - (agility * 0.0005f);
        }
        if (hands != Engine.NULL_ARMOR)
        {
            if (hands.GetArmorClass() == Armor.ArmorClass.heavy)
                baseSpeed -= 0.1f - (strength * 0.001f);
            else if (hands.GetArmorClass() == Armor.ArmorClass.light)
                baseSpeed -= 0.05f - (agility * 0.0005f);
        }

        return baseSpeed;
    }


    // Player Equipment

    public Weapon GetWeapon() { return weapon; }

    public void EquipWeapon(Weapon weapon) { this.weapon = weapon; }
    public void UnequipWeapon() { weapon = Engine.NULL_WEAPON; }

    public Armor GetHead() { return head; }
    public Armor GetChest() { return chest; }
    public Armor GetLegs() { return legs; }
    public Armor GetFeet() { return feet; }
    public Armor GetHands() { return hands; }

    public void EquipArmor(Armor armor)
    {
        switch (armor.GetArmorType())
        {
            case Armor.ArmorType.head:
                head = armor;
                break;
            case Armor.ArmorType.chest:
                chest = armor;
                break;
            case Armor.ArmorType.legs:
                legs = armor;
                break;
            case Armor.ArmorType.feet:
                feet = armor;
                break;
            case Armor.ArmorType.hands:
                hands = armor;
                break;
        }
    }
    public void UnequipArmor(Armor armor)
    {
        switch(armor.GetArmorType())
        {
            case Armor.ArmorType.head:
                head = Engine.NULL_ARMOR;
                break;
            case Armor.ArmorType.chest:
                chest = Engine.NULL_ARMOR;
                break;
            case Armor.ArmorType.legs:
                legs = Engine.NULL_ARMOR;
                break;
            case Armor.ArmorType.feet:
                feet = Engine.NULL_ARMOR;
                break;
            case Armor.ArmorType.hands:
                hands = Engine.NULL_ARMOR;
                break;
        }
    }

    //**

    // Status Effects

    public List<StatusEffect> GetStatusEffects() { return statusEffects; }
    public void AddStatusEffect(StatusEffect statusEffect) { statusEffects.Add(statusEffect); }

    public void DecrementStatusEffectTurn()
    {
        if (statusEffects.Count > 0)
            foreach (StatusEffect statusEffect in statusEffects.ToList<StatusEffect>())
            {
                statusEffect.DecrementTurnCount();
                if (statusEffect.GetTurnAmount() < 1)
                    RemoveStatusEffect(statusEffect);
            }
    }
    public void RemoveStatusEffect(StatusEffect statusEffect) { statusEffects.Remove(statusEffect); }
    public void ClearStatusEffects() { statusEffects.Clear(); }

    float GetTotalEffect(StatusEffect.StatusEffectType statusEffectType)
    {
        float effectTotal = 0;
        foreach (StatusEffect status in statusEffects)
        {
            if (statusEffectType == status.GetStatusEffectType())
                effectTotal += status.GetStatChange();
        }
        return effectTotal;
    }

    //**

    // Skills

    public List<Skill> GetSkills() { return unlockedSkills; }

    List<StaminaSkill> GetStaminaSkills()
    {
        List<StaminaSkill> sSkills = new List<StaminaSkill>();
        foreach (Skill skill in unlockedSkills)
            if (skill.GetSkillType() == Skill.SkillType.stamina)
                sSkills.Add((StaminaSkill)skill);
        return sSkills;
    }

    List<ManaSkill> GetManaSkills()
    {
        List<ManaSkill> mSkills = new List<ManaSkill>();
        foreach (Skill skill in unlockedSkills)
            if (skill.GetSkillType() == Skill.SkillType.mana)
                mSkills.Add((ManaSkill)skill);
        return mSkills;
    }

    public void AddSkill(Skill skill) { unlockedSkills.Add(skill); }

    List<StaminaSkill> GetActiveStaminaSkills() { return GetStaminaSkills().FindAll(x => x.IsActive()); }
    List<ManaSkill> GetActiveManaSkills() { return GetManaSkills().FindAll(x => x.IsActive()); }

    public StaminaSkill GetRandomStaminaSkill()
    {
        List<StaminaSkill> activeStaminaSkills = GetActiveStaminaSkills();
        if (activeStaminaSkills.Count == 0)
            return null;
        else
            return (activeStaminaSkills.Count > 1) ? activeStaminaSkills[UnityEngine.Random.Range(0, activeStaminaSkills.Count)] : activeStaminaSkills[0];
    }
    public ManaSkill GetRandomManaSkill()
    {
        List<ManaSkill> activeManaSkills = GetActiveManaSkills();
        if (activeManaSkills.Count == 0)
            return null;
        else
            return (activeManaSkills.Count > 1) ? activeManaSkills[UnityEngine.Random.Range(0, activeManaSkills.Count)] : activeManaSkills[0];
    }

    //**

    // Quests

    public bool CheckForQuest(uint id)
    {
        for (int i = 0; i < questList.Count; i++)
            if (questList[i].GetID() == id)
                return true;
        return false;
    }
    public Quest GetQuest(uint id)
    {
        return questList.Find(x => x.GetID() == id);
    }

    public List<Quest> GetQuestList() { return questList; }
    public List<Quest> GetCompletedQuests() { return completedQuests; }

    public void AddQuest(Quest quest)
    {
        try
        {
            questList.Add(quest);
        }
        catch (ArgumentException)
        {

        }
    }
    public void RemoveQuest(Quest quest)
    {
        questList.Remove(quest);
    }

    public void AddCompletedQuest(Quest quest) { completedQuests.Add(quest); }
    public void RemoveCompletedQuest(Quest quest) { completedQuests.Remove(quest); }

    //**

}
