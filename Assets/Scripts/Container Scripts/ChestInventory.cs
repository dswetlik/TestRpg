using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChestInventory : MonoBehaviour
{

    [SerializeField] bool hasBeenSearched = false;
    [SerializeField] bool containsKey = false;
    [SerializeField] List<Item> items = new List<Item>();
    [SerializeField] int gold = 0;

    public void AddItem(Item item) { items.Add(item); }

    public void RemoveItem(Item item) { items.Remove(item); }

    public List<Item> GetItems() { return items; }

    public bool HasBeenSearched() { return hasBeenSearched; }

    public bool ContainsKey() { return containsKey; }

    public void SetHasBeenSearched(bool x) { hasBeenSearched = x; }

    public int GetGold() { return gold; }

    public void SetGold(int gold) { this.gold = gold; }
}
