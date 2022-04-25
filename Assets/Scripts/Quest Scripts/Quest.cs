using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public abstract class Quest : ScriptableObject
{

    public enum QuestType
    {
        Location,
        Fetch,
        Slay,
        Talk,
        Clear
    };

    [SerializeField] QuestType questType;
    [SerializeField] uint id;
    [SerializeField] new string name;
    [TextArea(3,5)]
    [SerializeField] string description;

    [SerializeField] int levelRequirement;
    [SerializeField] Quest prerequisite;
    [SerializeField] bool isCompleted;
    [SerializeField] QuestStartDialogue dialogue;

    [SerializeField] bool hasItemReward;
    [SerializeField] List<Item> itemRewards = new List<Item>();
    [SerializeField] uint expReward, goldReward;

    
    public uint GetID() { return id; }
    public QuestType GetQuestType() { return questType; }
    public string GetName() { return name; }
    public string GetDescription() { return description; }

    public int GetLevelRequirement() { return levelRequirement; }
    public bool HasPrerequisite() { return prerequisite != null; }
    public Quest GetPrerequisite() { return prerequisite; }
    public bool IsCompleted() { return isCompleted; }
    public QuestStartDialogue GetDialogue() { return dialogue; }
    public bool HasItemReward() { return hasItemReward; }
    
    public uint GetExpReward() { return expReward; }
    public uint GetGoldReward() { return goldReward; }
    public List<Item> GetItemRewards() { return itemRewards; }

    public abstract bool CheckQuestCompletion();

    public void SetDialogue(QuestStartDialogue dialogue) { this.dialogue = dialogue; }
    public void SetQuestType(QuestType questType) { this.questType = questType; }
    public void SetName(string name) { this.name = name; }
    public void SetDescription(string description) { this.description = description; }
    public void SetCompletion(bool value) { isCompleted = value; }
    public void SetID(uint id) { this.id = id; }
    public void SetExpReward(uint expReward) { this.expReward = expReward; }
    public void SetGoldReward(uint goldReward) { this.goldReward = goldReward; }

}
