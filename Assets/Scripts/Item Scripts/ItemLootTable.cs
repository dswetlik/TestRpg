using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Loot Table", menuName = "Tables/Item Loot Table", order = 4)]
public class ItemLootTable : ScriptableObject
{
    [SerializeField] List<Item> items = new List<Item>();
    [Range(0, 100.0f)] [SerializeField] List<float> itemProbability = new List<float>();
    [SerializeField] List<int> itemCount = new List<int>();

    public void ItemDrop(ref List<Item> items, ref List<int> counts)
    {
        items.Clear();
        counts.Clear();

        for(int i = 0; i < this.items.Count; i++)
        {
            float key = Random.Range(0, 100.0f);
            if(key <= itemProbability[i])
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

    public List<Item> GetItems() { return items; }
    public Item GetItem(int itemElement) { return items[itemElement]; }
    public float GetItemProbability(int floatElement) { return itemProbability[floatElement]; }
    public int GetItemCount(int countElement) { return Random.Range(1, itemCount[countElement] + 1); }

}
