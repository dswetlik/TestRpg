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
    Location currentLocation;
    Inventory inventory;
    Weapon weapon;
    Armor head, chest, legs, feet, hands;
    uint level, exp, totalExp, expToLevel, skillPoints, currentWeight, maxWeight;
    int health, stamina, mana, maxHealth, maxStamina, maxMana, staminaRegen, manaRegen, defense, gold;
    float speed;
    List<Quest> questList = new List<Quest>();
    List<StatusEffect> statusEffects = new List<StatusEffect>();
    List<ActiveSkill> activeStaminaSkills = new List<ActiveSkill>();
    List<ActiveSkill> activeManaSkills = new List<ActiveSkill>();
    List<PassiveSkill> passiveSkills = new List<PassiveSkill>();

    public Player(string name, Location currentLocation, Inventory inventory, uint level = 1, int health = 20, int stamina = 10, int mana = 10,
            uint currentWeight = 0, uint maxWeight = 50)
    {
        this.name = name;
        this.currentLocation = currentLocation;
        this.level = level;
        this.health = health;
        this.stamina = stamina;
        this.mana = mana;
        this.maxWeight = maxWeight;
        this.inventory = inventory;

        maxHealth = 20;
        maxStamina = 10;
        maxMana = 10;
        staminaRegen = 2;
        manaRegen = 2;

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
        expToLevel = (level * 10);
    }

    // Get Functions
    public string GetName() { return name; }

    public Location GetLocation() { return currentLocation; }
    public Inventory GetInventory() { return inventory; }
    public uint GetCurrentWeight() { return currentWeight; }
    public uint GetMaxWeight() { return (uint)((maxWeight + GetPassiveFlat(PassiveSkill.AttributeType.carryWeight)) * GetPassivePercent(PassiveSkill.AttributeType.carryWeight)); }
    public int GetGold() { return gold; }

    public uint GetLevel() { return level; }
    public uint GetExp() { return exp; }
    public uint GetTotalExp() { return totalExp; }
    public uint GetToLevelExp() { return expToLevel; }
    public uint GetSkillPoints() { return skillPoints; }

    public int GetHealth() { return health; }
    public int GetStamina() { return stamina; }
    public int GetMana() { return mana; }
    public int GetMaxHealth() { return (int)((maxHealth + GetPassiveFlat(PassiveSkill.AttributeType.maxHealth)) * GetPassivePercent(PassiveSkill.AttributeType.maxHealth)); }
    public int GetMaxStamina() { return (int)((maxStamina + GetPassiveFlat(PassiveSkill.AttributeType.maxStamina)) * GetPassivePercent(PassiveSkill.AttributeType.maxStamina)); }
    public int GetMaxMana() { return (int)((maxMana + GetPassiveFlat(PassiveSkill.AttributeType.maxMana)) * GetPassivePercent(PassiveSkill.AttributeType.maxMana)); }
    public int GetStaminaRegen() { return (int)((staminaRegen + GetPassiveFlat(PassiveSkill.AttributeType.staminaRegen)) * GetPassivePercent(PassiveSkill.AttributeType.staminaRegen)); }
    public int GetManaRegen() { return (int)((manaRegen + GetPassiveFlat(PassiveSkill.AttributeType.manaRegen)) * GetPassivePercent(PassiveSkill.AttributeType.manaRegen)); }
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

        return (int)((defenseRating + GetPassiveFlat(PassiveSkill.AttributeType.defense)) * GetPassivePercent(PassiveSkill.AttributeType.defense));
    }
    public float GetSpeed() { return ((speed + GetPassiveFlat(PassiveSkill.AttributeType.speed)) * GetPassivePercent(PassiveSkill.AttributeType.speed)); }

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
        CheckSpeed();
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
        CheckSpeed();
    }

    public void SetSpeed(float speed) { this.speed = speed; }

    public void CheckSpeed()
    {
        speed = DEFAULT_SPEED;

        if(head != Engine.NULL_ARMOR)
        {
            if (head.GetArmorClass() == Armor.ArmorClass.heavy)
                speed -= 0.1f;
            else if (head.GetArmorClass() == Armor.ArmorClass.light)
                speed -= 0.05f;
        }
        if(chest != Engine.NULL_ARMOR)
        {
            if (chest.GetArmorClass() == Armor.ArmorClass.heavy)
                speed -= 0.1f;
            else if (chest.GetArmorClass() == Armor.ArmorClass.light)
                speed -= 0.05f;
        }
        if (legs != Engine.NULL_ARMOR)
        {
            if (legs.GetArmorClass() == Armor.ArmorClass.heavy)
                speed -= 0.1f;
            else if (legs.GetArmorClass() == Armor.ArmorClass.light)
                speed -= 0.05f;
        }
        if (feet != Engine.NULL_ARMOR)
        {
            if (feet.GetArmorClass() == Armor.ArmorClass.heavy)
                speed -= 0.1f;
            else if (feet.GetArmorClass() == Armor.ArmorClass.light)
                speed -= 0.05f;
        }
        if (hands != Engine.NULL_ARMOR)
        {
            if (hands.GetArmorClass() == Armor.ArmorClass.heavy)
                speed -= 0.1f;
            else if (hands.GetArmorClass() == Armor.ArmorClass.light)
                speed -= 0.05f;
        }
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

    // Set Functions
    public void SetName(string name) { this.name = name; }
    public void SetLocation(Location newLocation) { currentLocation = newLocation; } 

    public void SetLevel(uint level) { this.level = level; }
    public void SetExp(uint exp) { this.exp = exp; }
    public void SetTotalExp(uint totalExp) { this.totalExp = totalExp; }
    public void SetExpToLevel(uint expToLevel) { this.expToLevel = expToLevel; }
    public void SetSkillPoints(uint skillPoints) { this.skillPoints = skillPoints; }

    public void SetHealth(int health) { this.health = health; }
    public void SetStamina(int stamina) { this.stamina = stamina; }
    public void SetMana(int mana) { this.mana = mana; }
    public void SetMaxHealth(int maxHealth) { this.maxHealth = maxHealth; }
    public void SetMaxStamina(int maxStamina) { this.maxStamina = maxStamina; }
    public void SetMaxMana(int maxMana) { this.maxMana = maxMana; }
    public void SetStaminaRegen(int staminaRegen) { this.staminaRegen = staminaRegen; }
    public void SetManaRegen(int manaRegen) { this.manaRegen = manaRegen; }

    public void SetCurrentWeight(uint currentWeight) { this.currentWeight = currentWeight; }
    public void SetMaxWeight(uint maxWeight) { this.maxWeight = maxWeight; }

    public void ChangeGold(int gold) { this.gold += gold; if (gold < 0) gold = 0; }

    public void SetGold(int gold) { this.gold = gold; }

    public void AddExp(uint exp)
    {
        this.exp += exp;
        totalExp += exp;
        //("\nEarned Exp: {0}\nCurrent Exp: {1}\n", exp, this.exp);
        CheckForLevelUp();
    }

    public void CheckForLevelUp()
    {
        if (exp >= expToLevel)
        {
            exp -= expToLevel;
            level++;
            expToLevel += (level * 10);
            skillPoints++;

            SetMaxHealth((int)(level * 5) + 20);
            SetMaxStamina((int)(level * 5) + 10);
            SetMaxMana((int)(level * 5) + 10);

            GameObject.Find("GameManager").GetComponent<Engine>().OutputToText(String.Format("Level Up! New Level: {0}. Exp to Level: {1}.", level, expToLevel));
        }
    }

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

    public void AddActiveStaminaSkill(ActiveSkill skill) { activeStaminaSkills.Add(skill); }

    public void AddActiveManaSkill(ActiveSkill skill) { activeManaSkills.Add(skill); }

    public List<ActiveSkill> GetActiveStaminaSkills() { return activeStaminaSkills; }

    public List<ActiveSkill> GetActiveManaSkills() { return activeManaSkills; }

    public ActiveSkill GetRandomStaminaSkill()
    {
        if (activeStaminaSkills.Count == 0)
            return null;
        else
            return (activeStaminaSkills.Count > 1) ? activeStaminaSkills[UnityEngine.Random.Range(0, activeStaminaSkills.Count)] : activeStaminaSkills[0];
    }

    public ActiveSkill GetRandomManaSkill()
    {
        if (activeManaSkills.Count == 0)
            return null;
        else
            return (activeManaSkills.Count > 1) ? activeManaSkills[UnityEngine.Random.Range(0, activeManaSkills.Count)] : activeManaSkills[0];
    }

    public void AddPassiveSkill(PassiveSkill passiveSkill) { passiveSkills.Add(passiveSkill); }

    public List<PassiveSkill> GetPassiveSkills() { return passiveSkills; }

    public float GetPassivePercent(PassiveSkill.AttributeType attributeType)
    {
        float percentage = 1.0f;
        foreach(PassiveSkill skill in passiveSkills)
        {
            if (skill.GetAttributeType() == attributeType && skill.IsPercentage())
                percentage += skill.GetAttributeChange();
        }
        return percentage;
    }

    public int GetPassiveFlat(PassiveSkill.AttributeType attributeType)
    {
        int flat = 0;
        foreach(PassiveSkill skill in passiveSkills)
        {
            if (skill.GetAttributeType() == attributeType && !skill.IsPercentage())
                flat += (int)skill.GetAttributeChange();
        }
        return flat;
    }

    public bool CheckForQuest(uint id)
    {
        for (int i = 0; i < questList.Count; i++)
            if (questList[i].GetID() == id)
                return true;
        return false;
    }

    public void AddQuest(Quest quest)
    {
        try
        {
            questList.Add(quest);
        }
        catch (ArgumentException)
        {
            Debug.LogFormat("Quest already exists in questlist!");
        }
    }

    public void RemoveQuest(Quest quest)
    {
        questList.Remove(quest);
    }

    public Quest GetQuest(uint id)
    {
        return questList.Find(x => x.GetID() == id);
    }

    public void ChangeHealth(int healthChange)
    {
        health += healthChange;
        if (health < 0)
            health = 0;
        if (health > GetMaxHealth())
            health = GetMaxHealth();
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

}
