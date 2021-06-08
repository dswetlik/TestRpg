using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quests/Base Quest", order = 0)]
public class Quest : ScriptableObject
{

    public enum QuestType
    {
        Location,
        Fetch
    };

    [SerializeField] QuestType questType;
    [SerializeField] new string name;
    [TextArea(3,5)]
    [SerializeField] string description;
    [SerializeField] bool isCompleted;
    [SerializeField] Dialogue dialogue;
    [SerializeField] bool hasItemReward;
    [SerializeField] List<Item> itemRewards;
    [SerializeField] uint id, expReward, goldReward;

    public Quest(Dialogue dialogue, QuestType questType, string name = "", string description = "", bool isCompleted = false, bool hasItemReward = false, uint id = 0, uint expReward = 0, uint goldReward = 0)
    {
        this.dialogue = dialogue;
        this.questType = questType;
        this.name = name;
        this.description = description;
        this.id = id;
        this.expReward = expReward;
        this.goldReward = goldReward;
        this.hasItemReward = hasItemReward;
        this.isCompleted = isCompleted;
        itemRewards = new List<Item>();
    }

    public Dialogue GetDialogue() { return dialogue; }
    public QuestType GetQuestType() { return questType; }
    public string GetName() { return name; }
    public string GetDescription() { return description; }
    public bool IsCompleted() { return isCompleted; }
    public bool HasItemReward() { return hasItemReward; }
    public uint GetID() { return id; }
    public uint GetExpReward() { return expReward; }
    public uint GetGoldReward() { return goldReward; }
    public List<Item> GetItemRewards() { return itemRewards; }

    public virtual bool CheckQuestCompletion(Player player) { return true; }

    public void SetDialogue(Dialogue dialogue) { this.dialogue = dialogue; }
    public void SetQuestType(QuestType questType) { this.questType = questType; }
    public void SetName(string name) { this.name = name; }
    public void SetDescription(string description) { this.description = description; }
    public void SetCompletion(bool value) { isCompleted = value; }
    public void SetID(uint id) { this.id = id; }
    public void SetExpReward(uint expReward) { this.expReward = expReward; }
    public void SetGoldReward(uint goldReward) { this.goldReward = goldReward; }

}
