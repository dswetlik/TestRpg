using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Loot Table", menuName = "Tables/Item Loot Table", order = 4)]
public class ItemLootTable : ScriptableObject
{
    [SerializeField] List<Item> items = new List<Item>();
    [Range(0, 1.0f)] [SerializeField] List<float> itemProbability = new List<float>();
    [SerializeField] List<Vector2Int> itemCount = new List<Vector2Int>();

    [SerializeField] Vector2Int goldRewardRange;

    public void ItemDrop(ref List<Item> items, ref List<int> counts)
    {
        items.Clear();
        counts.Clear();

        for(int i = 0; i < this.items.Count; i++)
        {
            float key = Random.Range(0, 1.0f);
            if(itemProbability[i] >= key)
            {
                int count = GetItemCount(i);
                if(count > 0)
                {
                    items.Add(this.items[i]);
                    counts.Add(count);
                }
            }
        }
    }

    public int GetRandomGold() { return Random.Range(goldRewardRange.x, goldRewardRange.y + 1); }
    public int GetMinGold() { return goldRewardRange.x; }
    public int GetMaxGold() { return goldRewardRange.y; }

    public List<Item> GetItems() { return items; }
    public Item GetItem(int itemElement) { return items[itemElement]; }
    public float GetItemProbability(int i) { return itemProbability[i]; }
    public int GetItemCount(int i) { return Random.Range(itemCount[i].x, itemCount[i].y + 1); }

}
